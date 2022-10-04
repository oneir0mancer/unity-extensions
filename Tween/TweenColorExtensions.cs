using System.Collections;
using Oneiromancer.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Oneiromancer.Tween
{
    public static class TweenColorExtensions
    {
        /// Tween color, works for both Image and TMP_Text
        public static IEnumerator TweenColor(this MaskableGraphic graphic, Color startColor, Color targetColor, float duration)
        {
            yield return CoroutineExtensions.Progress(duration, t =>
                graphic.color = Color.Lerp(startColor, targetColor, t)
            );
        }

        /// Tween color, starting from current value
        public static IEnumerator TweenColor(this MaskableGraphic graphic, Color targetColor, float duration)
            => TweenColor(graphic, graphic.color, targetColor, duration);

        /// Tween color alpha, works for both Image and TMP_Text
        public static IEnumerator TweenAlpha(this MaskableGraphic graphic, float fromAlpha, float toAlpha, float duration)
        {
            Color color = graphic.color;
            float startAlpha = fromAlpha;
            yield return CoroutineExtensions.Progress(duration, t =>
            {
                color.a = Mathf.Lerp(startAlpha, toAlpha, t);
                graphic.color = color;
            });
        }
        
        /// Tween color alpha, starting from current value
        public static IEnumerator TweenAlpha(this MaskableGraphic graphic, float targetAlpha, float duration)
            => TweenAlpha(graphic, graphic.color.a, targetAlpha, duration);
    }
}
