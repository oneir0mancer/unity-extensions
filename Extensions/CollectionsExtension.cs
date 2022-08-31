using System.Collections.Generic;
using System.Text;
using Random = UnityEngine.Random;

namespace Oneiromancer.Extensions
{
    public static class CollectionsExtension
    {
        /// Returns a product of all elements in collection
        public static float Product(this IEnumerable<float> list)
        {
            float product = 1;
            foreach (var item in list)
            {
                product *= item;
            }
            return product;
        }
    
        /// Returns max element of array or null if array is empty
        public static T GetMaxElement<T>(this IReadOnlyList<T> list, System.Func<T, float> sorter)
        {
            T maxItem = default;
            float maxValue = float.MinValue;
            foreach (var element in list)
            {
                float value = sorter(element);
                if (value > maxValue)
                {
                    maxValue = value;
                    maxItem = element;
                }
            }

            return maxItem;
        }
        
        /// Returns min element of array or null if array is empty
        public static T GetMinElement<T>(this IReadOnlyList<T> list, System.Func<T, float> sorter) => list.GetMaxElement(x => -sorter(x));
    
        /// Returns random element from list, will throw if collection is empty
        public static T GetRandomElement<T>(this IReadOnlyList<T> list) => list[Random.Range(0, list.Count)];

        /// Returns a json-like string with all elements of list
        public static string ElementsToString<T>(this IReadOnlyList<T> list)
        {
            StringBuilder s = new StringBuilder("[ ");
            foreach (var item in list) 
                s.Append($"{item.ToString()}, ");
            s.Append("]");
            return s.ToString();
        }
        
        /// Shuffles a list inplace
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int n = list.Count; n > 0; n--)
            {
                int k = Random.Range(0, n);  
                (list[k], list[n - 1]) = (list[n - 1], list[k]);
            }
        }
        
        /// System.Array.IndexOf wrapper
        public static int IndexOf<T>(this T[] array, T value) => System.Array.IndexOf(array, value);
        
        /// System.Array.Find wrapper
        public static T Find<T>(this T[] array, System.Predicate<T> match) => System.Array.Find(array, match);
    }
}