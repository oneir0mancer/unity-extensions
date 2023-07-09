using System.Collections;
using Oneiromancer.Extensions;
using UnityEngine;

namespace Oneiromancer.Tween
{
    public static class TweenTransformExtensions
    {
        /// Tween Transform or RectTransform position
        public static IEnumerator TweenPosition(this Transform transform, Vector3 startPosition, Vector3 targetPosition, float duration)
        {
            yield return CoroutineExtensions.Progress(duration, t =>
                transform.position = Vector3.Lerp(startPosition, targetPosition, t)
            );
        }

        /// Tween Transform or RectTransform position, starting from current value
        public static IEnumerator TweenPosition(this Transform transform, Vector3 targetPosition, float duration)
            => TweenPosition(transform, transform.position, targetPosition, duration);
        
        public static IEnumerator TweenScale(this Transform transform, Vector3 startScale, Vector3 targetScale, float duration)
        {
            yield return CoroutineExtensions.Progress(duration, t =>
                transform.localScale = Vector3.Lerp(startScale, targetScale, t)
            );
        }

        public static IEnumerator TweenScale(this Transform transform, Vector3 targetScale, float duration)
            => TweenScale(transform, transform.localScale, targetScale, duration);
        
        public static IEnumerator TweenScale(this Transform transform, float scaleMlt, float duration)
            => TweenScale(transform, scaleMlt * transform.localScale, duration);
        
        public static IEnumerator TweenSizeDelta(this RectTransform rectTransform, Vector2 startSizeDelta, 
            Vector2 targetSizeDelta, float duration)
        {
            yield return CoroutineExtensions.Progress(duration, t =>
                rectTransform.sizeDelta = Vector2.Lerp(startSizeDelta, targetSizeDelta, t)
            );
        }

        public static IEnumerator TweenSizeDelta(this RectTransform rectTransform, Vector2 targetSizeDelta,
            float duration)
            => TweenSizeDelta(rectTransform, rectTransform.sizeDelta, targetSizeDelta, duration);
        
    }

    public enum XYZComponent  {X, Y, Z, XY, XZ, YZ }
}