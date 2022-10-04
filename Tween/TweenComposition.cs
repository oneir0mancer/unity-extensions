using System.Collections;

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
    }
}