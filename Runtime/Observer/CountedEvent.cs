using System;

namespace Oneiromancer.Observer
{
    public class CountedEvent : CountedObserver<Action>
    {
        public void Invoke()
        {
            for (int i = _subscribers.Count - 1; i >= 0; i--)
            {
                _subscribers[i].Item?.Invoke();
                _subscribers[i].Count -= 1;
                
                if (_subscribers[i].Count <= 0) _subscribers.RemoveAt(i);
            }
        }
    }
    
    public class CountedEvent<T> : CountedObserver<Action<T>>
    {
        public void Invoke(T arg)
        {
            for (int i = _subscribers.Count - 1; i >= 0; i--)
            {
                _subscribers[i].Item?.Invoke(arg);
                _subscribers[i].Count -= 1;
                
                if (_subscribers[i].Count <= 0) _subscribers.RemoveAt(i);
            }
        }
    }
    
    public class CountedEvent<T1, T2> : CountedObserver<Action<T1, T2>>
    {
        public void Invoke(T1 arg1, T2 arg2)
        {
            for (int i = _subscribers.Count - 1; i >= 0; i--)
            {
                _subscribers[i].Item?.Invoke(arg1, arg2);
                _subscribers[i].Count -= 1;
                
                if (_subscribers[i].Count <= 0) _subscribers.RemoveAt(i);
            }
        }
    }
}