using System.Collections.Generic;
using UnityEngine;

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
        public static float Remap(this float value, float inMin, float inMax, float outMin, float outMax) 
        {
            return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
        }
        
        public static Vector2 Abs(this Vector2 value)
        {
            value.x = Mathf.Abs(value.x);
            value.y = Mathf.Abs(value.y);
            return value;
        }
        
        public static Vector2 SwapComponents(this Vector2 value)
        {
            (value.x, value.y) = (value.y, value.x);
            return value;
        }
        
        /// Snap value to nearest position on grid with cell size of <paramref name="gridSize"/>
        public static float SnapToGrid(this float value, float gridSize) 
        {
            return Mathf.Round(value / gridSize) * gridSize;
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
        
        /// Returns a framerate-independent "lerp" from `a` to `b` over `deltaTime`.
        /// `decay` is exponential decay slope, useful range approx. 1 to 25, from slow to fast.
        /// <remarks>See Freya's presentation: https://youtu.be/LSNQuFEDOyQ?t=2978</remarks>
        public static float LerpExpDecay(float a, float b, float decay, float deltaTime)
        {
            return Mathf.Lerp(a, b, 1 - Mathf.Exp(-decay * deltaTime));
        }

        /// Returns a framerate-independent "lerp" from `a` to `b` over Time.deltaTime (of Time.fixedDeltaTime if called from FixedUpdate).
        public static float LerpExpDecay(float a, float b, float decay) => LerpExpDecay(a, b, decay, Time.deltaTime);
        
        /// Returns a framerate-independent "lerp" from vector `a` to `b` over `deltaTime`.
        /// `decay` is exponential decay slope, useful range approx. 1 to 25, from slow to fast.
        public static Vector3 LerpExpDecay(Vector3 a, Vector3 b, float decay, float deltaTime)
        {
            return Vector3.Lerp(a, b, 1 - Mathf.Exp(-decay * deltaTime));
        }
        
        /// Returns a framerate-independent "lerp" from vector `a` to `b` over Time.deltaTime (of Time.fixedDeltaTime if called from FixedUpdate).
        public static Vector3 LerpExpDecay(Vector3 a, Vector3 b, float decay) => LerpExpDecay(a, b, decay, Time.deltaTime);
    }
}
