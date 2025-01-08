using System.Dynamic;
using System.Reflection;

namespace hotelier_core_app.Domain.Helpers
{
    public static class DictionaryToObjectConverter
    {
        public static T GetClassInstance<T>(this Dictionary<string, object> dict, Type type) where T : class
        {
            object obj = Activator.CreateInstance(type);
            foreach (KeyValuePair<string, object> item in dict)
            {
                PropertyInfo property = type.GetProperty(item.Key);
                if (!(property == null))
                {
                    property.SetValue(obj, item.Value, null);
                }
            }

            return obj as T;
        }

        public static object ConvertToAnonymousObject(this Dictionary<string, object> dict)
        {
            ExpandoObject expandoObject = new ExpandoObject();
            ICollection<KeyValuePair<string, object>> collection = expandoObject;
            foreach (KeyValuePair<string, object> item in dict)
            {
                collection.Add(item);
            }

            return expandoObject;
        }
    }
}
