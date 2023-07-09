using System.Collections.Generic;

namespace Oneiromancer.Modifiers
{
    public abstract class BaseValueModifier<T>
    {
        protected readonly List<T> _modifiers = new List<T>();

        public abstract T Get();
        
        public void Add(T item) => _modifiers.Add(item);
        public bool Remove(T item) => _modifiers.Remove(item);
        public void Clear() => _modifiers.Clear();
        public bool IsEmpty => _modifiers.Count == 0;
    
        public static implicit operator T(BaseValueModifier<T> x) => x.Get();
    }
}