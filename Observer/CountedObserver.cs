using System.Collections.Generic;
using Oneiromancer.Utils;

namespace Oneiromancer.Observer
{
    public abstract class CountedObserver<T>
    {
        protected readonly List<Counted<T>> _subscribers = new List<Counted<T>>();
        
        public Counted<T> Subscribe(T item, int count)
        {
            var countedEvent = new Counted<T>(item, count);
            _subscribers.Add(countedEvent);
            return countedEvent;
        }
        
        public bool Unsubscribe(T item)
        {
            var idx = _subscribers.FindIndex(x => x.Item.Equals(item));
            if (idx <= 0) return false;
            _subscribers.RemoveAt(idx);
            return true;
        }
        
        public bool Unsubscribe(Counted<T> countedEvent)
        {
            return _subscribers.Remove(countedEvent);
        }
    }
}