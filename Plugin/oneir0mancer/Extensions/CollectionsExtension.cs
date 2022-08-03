using System.Collections.Generic;
using System.Text;
using Random = UnityEngine.Random;

public static class CollectionsExtension
{
    public static float Product(this IEnumerable<float> list)
    {
        float product = 1;
        foreach (var item in list)
        {
            product *= item;
        }
        return product;
    }
    
    //Returns max element of array or null if array is empty
    public static T GetMaxElement<T>(this T[] array, System.Func<T, float> sorter)
    {
        T maxItem = default;
        float maxValue = float.MinValue;
        foreach (var element in array)
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
    //Returns min element of array or null if array is empty
    public static T GetMinElement<T>(this T[] array, System.Func<T, float> sorter) => array.GetMaxElement(x => -sorter(x));
    
    public static T GetRandomElement<T>(this List<T> list) => list[Random.Range(0, list.Count)];
    public static T GetRandomElement<T>(this T[] array) => array[Random.Range(0, array.Length)];
    
    public static int IndexOf<T>(this T[] array, T value) => System.Array.IndexOf(array, value);
    public static T Find<T>(this T[] array, System.Predicate<T> match) => System.Array.Find(array, match);
    
    public static string ElementsToString<T>(this IEnumerable<T> list)
    {
        StringBuilder s = new StringBuilder("[ ");
        foreach (var item in list) 
            s.Append($"{item.ToString()}, ");
        s.Append("]");
        return s.ToString();
    }
	
	public static string ElementsToString<T>(this T[] list)
    {
        string s = "( ";
        foreach (var item in list)
        {
            s += $"{item.ToString()}, ";
        }
        s += ")";
        return s;
    }
}