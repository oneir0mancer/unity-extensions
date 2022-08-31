namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Extensions/Slider Window Extension")]
    public class SliderMinMaxWindow : ISliderExtension
    {
        [SerializeField] private float _windowDelta;
        [SerializeField] private bool _floorToInt;
        
        protected override void SetValue(float value)
        {
            UpdateWindowImage(_floorToInt ? Mathf.Floor(value) : value);
        }

        private void UpdateWindowImage(float value)
        {
            if (_fillRect == null) return;
            
            Vector2 anchorMin = Vector2.zero;
            Vector2 anchorMax = Vector2.one;
            float normalizedValue = Mathf.InverseLerp(_slider.minValue, _slider.maxValue, value);
            float normalizedMinValue = Mathf.InverseLerp(_slider.minValue, _slider.maxValue, value - _windowDelta);

            if (reverseValue)
            {
                anchorMin[(int) axis] = 1 - normalizedValue;
                anchorMax[(int) axis] = 1 - normalizedMinValue;
            }
            else
            {
                anchorMin[(int) axis] = normalizedMinValue;
                anchorMax[(int) axis] = normalizedValue;
            }
            
            _fillRect.anchorMin = anchorMin;
            _fillRect.anchorMax = anchorMax;
        }
    }
}