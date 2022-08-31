public static class MathExtensions
{
    public static float Modulo(float value, float modulus)
    {
        float remainder = value % modulus;
        return remainder < 0f?  (remainder + modulus) : remainder;
    }
    
    public static float Remap (this float value, float inMin, float inMax, float outMin, float outMax) 
    {
        return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
    }
}
