using Oneiromancer.Extensions;

namespace Oneiromancer.Modifiers
{
    public class MultiplicativeFloatModifier : BaseValueModifier<float>
    {
        public override float Get() => _modifiers.Product();
    }
}