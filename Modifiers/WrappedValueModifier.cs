using System;
using System.Collections.Generic;
using Oneiromancer.Utils;

namespace Oneiromancer.Modifiers
{
    public abstract class WrappedValueModifier<T> where T : unmanaged, IEquatable<T>
    {
        protected readonly List<ValueWrapper<T>> _modifiers = new List<ValueWrapper<T>>();

        public abstract T Get();
        
        public ValueWrapper<T> Add(T value)
        {
            var item = new ValueWrapper<T>(value);
            _modifiers.Add(item);
            return item;
        }
        
        public void Remove(T value)
        {
            int idx = _modifiers.FindIndex(wrapper => wrapper.Value.Equals(value));
            if (idx < 0) return;
            _modifiers.RemoveAt(idx);
        }

        public bool Remove(ValueWrapper<T> item) => _modifiers.Remove(item);
        
        public void Clear() => _modifiers.Clear();

        public bool IsEmpty => _modifiers.Count == 0;
    
        public static implicit operator T(WrappedValueModifier<T> x) => x.Get();
    }
}