using Oneiromancer.Extensions;

namespace Oneiromancer.Modifiers
{
    public class CombinedFloatModifier : CombinedValueModifier<float>
    {
        public override float Get()
        {
            float mul = GetMultiplicativeValues().Product();
            float add = 1 + GetAdditiveValues().Sum();
            return mul * add;
        }
    }
}