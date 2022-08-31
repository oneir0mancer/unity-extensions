namespace UnityEngine.UI.Extensions
{
    [ExecuteAlways]
    [RequireComponent(typeof(Slider))]
    public abstract class BaseSliderExtension : MonoBehaviour
    {
        [SerializeField] protected Slider _slider;
        [SerializeField] protected RectTransform _fillRect;
        protected Image m_FillImage;
        
        protected bool reverseValue => _slider.direction == Slider.Direction.RightToLeft || 
                                      _slider.direction == Slider.Direction.TopToBottom;

        protected enum Axis { Horizontal = 0, Vertical = 1 }

        protected Axis axis => (_slider.direction == Slider.Direction.LeftToRight ||
                                _slider.direction == Slider.Direction.RightToLeft)
            ? Axis.Horizontal
            : Axis.Vertical;
        
        protected virtual void OnEnable()
        {
            SetValue(_slider.value);
            _slider.onValueChanged.AddListener(SetValue);
            if (_fillRect != null)
                m_FillImage = _fillRect.GetComponent<Image>();
        }
        
        protected virtual void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(SetValue);
        }
        
        protected virtual void OnValidate()
        {
            if (_fillRect != null)
                m_FillImage = _fillRect.GetComponent<Image>();
        }

        protected abstract void SetValue(float value);
    }
}
