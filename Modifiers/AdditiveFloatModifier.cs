using Oneiromancer.Extensions;
using UnityEngine;

namespace Oneiromancer.Modifiers
{
    public class AdditiveFloatModifier: BaseValueModifier<float>
    {
        private readonly float _minValue;
        private readonly float _maxValue;
        
        public AdditiveFloatModifier(float maxValue = 5)
        {
            _minValue = 0;
            _maxValue = maxValue;
        }
        
        public AdditiveFloatModifier(float minValue, float maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public override float Get() => Mathf.Clamp(1 + _modifiers.Sum(), _minValue, _maxValue);
    }
}