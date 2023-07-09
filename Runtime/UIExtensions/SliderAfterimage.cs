using System.Collections;

namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Extensions/Slider Afterimage")]
    public class SliderAfterimage : BaseSliderExtension
    {
        [SerializeField, Min(0)] private float _speed = 0.1f;
        [SerializeField, Min(0)] private float _pauseTime = 0.1f;
        private float _currentValue;
        private float _targetValue;

        private Coroutine _afterimageCoroutine;

        public void Set(float value) => SetValue(value);
        
        protected override void SetValue(float value)
        {
            _targetValue = value;
            StopFillCoroutine();
            if (value >= _currentValue)
            {
                _currentValue = value;
                return;
            }

            _afterimageCoroutine = StartCoroutine(AfterimageCoroutine());
        }

        private IEnumerator AfterimageCoroutine()
        {
            UpdateFillImage(_currentValue);
            yield return new WaitForSeconds(_pauseTime);
            
            float time = (_currentValue - _targetValue) / _speed;
            for (float t = 0; t < time; t += Time.deltaTime)
            {
                yield return null;
                _currentValue = Mathf.MoveTowards(_currentValue, _targetValue, _speed);
                UpdateFillImage(_currentValue);
            }

            _currentValue = _targetValue;
            _afterimageCoroutine = null;
        }
        
        private void UpdateFillImage(float value)
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

        private void StopFillCoroutine()
        {
            if (_afterimageCoroutine != null)
            {
                StopCoroutine(_afterimageCoroutine);
                _afterimageCoroutine = null;
            }
        }
    }
}
