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
    }
}
