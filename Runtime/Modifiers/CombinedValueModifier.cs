using System;
using System.Collections.Generic;
using Oneiromancer.Utils;

namespace Oneiromancer.Modifiers
{
    public abstract class CombinedValueModifier<T> where T : unmanaged, IEquatable<T>
    {
        protected readonly List<ValueWrapper<T>> _multiplicativeModifiers = new List<ValueWrapper<T>>();
        protected readonly List<ValueWrapper<T>> _additiveModifiers = new List<ValueWrapper<T>>();

        public abstract T Get();

        public ValueWrapper<T> AddAdditive(T value)
        {
            var wrapper = new ValueWrapper<T>(value);
            _additiveModifiers.Add(wrapper);
            return wrapper;
        }
        
        public ValueWrapper<T> AddMultiplicative(T value)
        {
            var wrapper = new ValueWrapper<T>(value);
            _multiplicativeModifiers.Add(wrapper);
            return wrapper;
        }

        public void RemoveAdditive(T value)
        {
            int idx = _additiveModifiers.FindIndex(wrapper => wrapper.Value.Equals(value));
            if (idx < 0)
                throw new ArgumentException($"Modifier of {value} not found", nameof(value));
            _additiveModifiers.RemoveAt(idx);
        }
        
        public void RemoveMultiplicative(T value)
        {
            int idx = _multiplicativeModifiers.FindIndex(wrapper => wrapper.Value.Equals(value));
            if (idx < 0)
                throw new ArgumentException($"Modifier of {value} not found", nameof(value));
            _multiplicativeModifiers.RemoveAt(idx);
        }
        
        public bool RemoveAdditive(ValueWrapper<T> wrapper) => _additiveModifiers.Remove(wrapper);

        public bool RemoveMultiplicative(ValueWrapper<T> wrapper) => _multiplicativeModifiers.Remove(wrapper);

        public void Clear()
        {
            _additiveModifiers.Clear();
            _multiplicativeModifiers.Clear();
        }
        
        public static implicit operator T(CombinedValueModifier<T> x) => x.Get();

        protected IEnumerable<T> GetMultiplicativeValues()
        {
            foreach (var modifier in _multiplicativeModifiers)
                yield return modifier.Value;
        }
        
        protected IEnumerable<T> GetAdditiveValues()
        {
            foreach (var modifier in _additiveModifiers)
                yield return modifier.Value;
        }
    }
}