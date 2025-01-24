using hotelier_core_app.Domain.SqlGenerator;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using hotelier_core_app.Domain.Attributes;

namespace hotelier_core_app.Domain.Extensions
{
    internal static class TypeExtensions
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _reflectionPropertyCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

        private static readonly ConcurrentDictionary<Type, SqlPropertyMetadata[]> _metaDataPropertyCache = new ConcurrentDictionary<Type, SqlPropertyMetadata[]>();

        public static PropertyInfo[] FindClassProperties(this Type objectType)
        {
            if (_reflectionPropertyCache.TryGetValue(objectType, out PropertyInfo[] value))
            {
                return value;
            }

            PropertyInfo[] properties = objectType.GetProperties();
            _reflectionPropertyCache.TryAdd(objectType, properties);
            return properties;
        }

        public static SqlPropertyMetadata[] FindClassMetaDataProperties(this Type objectType)
        {
            if (_metaDataPropertyCache.TryGetValue(objectType, out SqlPropertyMetadata[] value))
            {
                return value;
            }

            SqlPropertyMetadata[] array = (from p in (from x in objectType.GetProperties()
                                                      orderby x.GetCustomAttribute<IdentityAttribute>() != null descending, x.GetCustomAttribute<KeyAttribute>() != null descending
                                                      select x).ThenBy((PropertyInfo p) => (from a in p.GetCustomAttributes<ColumnAttribute>()
                                                                                            select a.Order).DefaultIfEmpty(int.MaxValue).FirstOrDefault()).Where(ExpressionHelper.GetPrimitivePropertiesPredicate())
                                           where !p.GetCustomAttributes<NotMappedAttribute>().Any()
                                           select new SqlPropertyMetadata(p)).ToArray();
            _metaDataPropertyCache.TryAdd(objectType, array);
            return array;
        }

        public static SqlPropertyMetadata[] GetNavigationPropertyMetaDataProperties(this Type objectType)
        {
            if (_metaDataPropertyCache.TryGetValue(objectType, out SqlPropertyMetadata[] value))
            {
                return value;
            }

            SqlPropertyMetadata[] array = (from p in objectType.GetProperties()
                                           where !p.GetCustomAttributes<NotMappedAttribute>().Any() && !p.GetCustomAttributes<KeyAttribute>().Any() && p.Name.ToLower() != "id"
                                           select new SqlPropertyMetadata(p)).ToArray();
            _metaDataPropertyCache.TryAdd(objectType, array);
            return array;
        }

        public static Type UnwrapNullableType(this Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }
    }
}
