namespace hotelier_core_app.Domain.Extensions
{
    internal static class CollectionExtensions
    {
        public static void AddRange<TInput>(this ICollection<TInput> collection, IEnumerable<TInput> addCollection)
        {
            if (collection == null || addCollection == null)
            {
                return;
            }

            foreach (TInput item in addCollection)
            {
                collection.Add(item);
            }
        }
    }
}
