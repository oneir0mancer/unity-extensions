using System.Collections.Generic;

namespace Oneiromancer.Extensions
{
    public static class MathExtensions
    {
        /// Modulo operation that always returns correct positive remainder.
        public static float Modulo(this float value, float modulus)
        {
            float remainder = value % modulus;
            return remainder < 0f ?  (remainder + modulus) : remainder;
        }
    
        /// Remap value from [inMin, inMax] to [outMin, outMax]
        public static float Remap (this float value, float inMin, float inMax, float outMin, float outMax) 
        {
            return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
        }
        
        /// Returns a product of all elements in collection
        public static float Product(this IEnumerable<float> list)
        {
            float product = 1;
            foreach (var item in list)
                product *= item;
            
            return product;
        }
        
        /// Returns a product of all elements (mapped by <paramref name="getter"/>) in collection
        public static float Product<T>(this IEnumerable<T> list, System.Func<T, float> getter)
        {
            float product = 1;
            foreach (var item in list)
                product *= getter(item);
            
            return product;
        }
        
        /// Returns a sum of all elements in collection (LINQ sum casts to double)
        public static float Sum(this IEnumerable<float> list)
        {
            float sum = 0;
            foreach (var item in list)
                sum += item;
            
            return sum;
        }
        
        /// Returns a product of all elements (mapped by <paramref name="getter"/>) in collection
        public static float Sum<T>(this IEnumerable<T> list, System.Func<T, float> getter)
        {
            float sum = 0;
            foreach (var item in list)
                sum += getter(item);
            
            return sum;
        }
    }
}
