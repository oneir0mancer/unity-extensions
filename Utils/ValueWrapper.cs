namespace Oneiromancer.Utils
{
    /// A wrapper to ref r-value types
    public class ValueWrapper<T> where T : unmanaged
    {
        public T Value;
        
        public ValueWrapper() { }

        public ValueWrapper(T value)
        {
            Value = value;
        }

        public static implicit operator T(ValueWrapper<T> item) => item.Value;
        
        public static explicit operator ValueWrapper<T>(T value) => new ValueWrapper<T>(value);
    }
}