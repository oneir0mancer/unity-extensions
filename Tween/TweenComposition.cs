using System.Collections;
using System.Collections.Generic;

namespace Oneiromancer.Tween
{
    public static class TweenComposition
    {
        /// Fire action callback after coroutine is finished
        public static IEnumerator SetOnComplete(this IEnumerator enumerator, System.Action action)
        {
            yield return enumerator;
            action?.Invoke();
        }
        
        /// Start second coroutine after first one is finished
        public static IEnumerator Then(this IEnumerator enumerator, IEnumerator nextEnumerator)
        {
            yield return enumerator;
            yield return nextEnumerator;
        }
        
        /// Consequently run multiple coroutines
        public static IEnumerator Combine(params IEnumerator[] coroutines)
        {
            foreach (var enumerator in coroutines)
                yield return enumerator;
        }
    }
    
    public class Sequence
    {
        private readonly List<IEnumerator> _coroutines = new List<IEnumerator>();

        public Sequence() {}

        public Sequence(params IEnumerator[] coroutines)
        {
            _coroutines.AddRange(coroutines);
        }
        
        public Sequence(IEnumerable<IEnumerator> coroutines)
        {
            _coroutines.AddRange(coroutines);
        }

        public void Add(IEnumerator coroutine)
        {
            _coroutines.Add(coroutine);
        }

        public IEnumerator GetCoroutine()    //TODO track step to pause/unpause
        {
            foreach (var routine in _coroutines)
                yield return routine;
        }
    }
}