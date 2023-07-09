using Oneiromancer.Extensions;

namespace Oneiromancer.Modifiers
{
    public class FloatModifier : ValueModifier<float>
    {
        public override float Get()
        {
            float mul = GetMultiplicativeValues().Product();
            float add = 1 + GetAdditiveValues().Sum();
            return mul * add;
        }
    }
}