namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Extensions/Back Slider Extension")]
    public class SliderIntBar : ISliderExtension
    {
        protected override void SetValue(float value)
        {
            UpdateBackImage(Mathf.FloorToInt(value));
        }

        private void UpdateBackImage(int value)
        {
            if (_fillRect == null) return;
            
            Vector2 anchorMin = Vector2.zero;
            Vector2 anchorMax = Vector2.one;
            float normalizedValue = Mathf.InverseLerp(_slider.minValue, _slider.maxValue, value);
            
            if (m_FillImage != null && m_FillImage.type == Image.Type.Filled)
            {
                m_FillImage.fillAmount = normalizedValue;
            }
            else
            {
                if (reverseValue)
                    anchorMin[(int) axis] = 1 - normalizedValue;
                else
                    anchorMax[(int) axis] = normalizedValue;
            }
            
            _fillRect.anchorMin = anchorMin;
            _fillRect.anchorMax = anchorMax;
        }
    }
}
