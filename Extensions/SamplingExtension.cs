using System.Collections.Generic;
using System.Linq;

namespace Oneiromancer.Extensions
{
    public static class SamplingExtension
    {
        /// Sample <paramref name="count"/> random elements from a collection
        public static IEnumerable<T> Sample<T>(this IReadOnlyList<T> collection, int count)
        {
            if (collection.Count < count)
                throw new System.ArgumentOutOfRangeException(nameof(count), "Not enough elements in collection");
            
            var rnd = new System.Random();
            return collection.OrderBy(x => rnd.Next()).Take(count);
        }

        /// Sample <paramref name="count"/> random elements from a collection to array
        public static T[] SampleArray<T>(this IReadOnlyList<T> collection, int count) =>
            Sample(collection, count).ToArray();
        
        /// Sample <paramref name="count"/> random elements from a collection to list
        public static List<T> SampleList<T>(this IReadOnlyList<T> collection, int count) =>
            Sample(collection, count).ToList();
        
        //https://stackoverflow.com/questions/48087/select-n-random-elements-from-a-listt-in-c-sharp
        public static IEnumerable<T> SelectionSampling<T>(this IReadOnlyList<T> collection, int count)
        {
            if (collection.Count < count)
                throw new System.ArgumentOutOfRangeException(nameof(count), "Not enough elements in collection");
            
            var rnd = new System.Random();
            int itemsToSelect = count;
            int itemsLeft = collection.Count;
            foreach (var item in collection)
            {
                if (rnd.Next() < (float) itemsToSelect / itemsLeft)
                {
                    yield return item;
                    itemsToSelect -= 1;
                    if (itemsToSelect <= 0) break;
                }
                itemsLeft -= 1;
            }
        }
    }
}