using System.Collections.Generic;
using System.Text;
using Random = UnityEngine.Random;

namespace Oneiromancer.Extensions
{
    public static class CollectionsExtension
    {
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
        
        /// Returns a json-like string of all elements of list mapped to string
        public static string ElementsToString<T>(this IReadOnlyList<T> list, System.Func<T, string> map)
        {
            StringBuilder s = new StringBuilder("[ ");
            foreach (var item in list) 
                s.Append($"{map(item)}, ");
            s.Append("]");
            return s.ToString();
        }
        
        /// Returns a json-like string with all elements of dict
        public static string ElementsToString<TK, TV>(this Dictionary<TK, TV> dict)
        {
            StringBuilder s = new StringBuilder("{\n");
            foreach (var item in dict) 
                s.Append($"{{ {item.Key.ToString()}, {item.Value.ToString()} }}\n ");
            s.Append("}");
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
        
        /// Get value from dictionary by key, or default value if such key doesnt exist
        public static TV GetValueOrDefault<TK, TV>(this IDictionary<TK, TV> dictionary, TK key,
            TV defaultValue = default)
        {
            if (dictionary == null) return defaultValue;
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }
}