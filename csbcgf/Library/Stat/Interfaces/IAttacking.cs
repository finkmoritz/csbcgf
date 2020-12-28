using csbcgf;

namespace csccgl
{
    public interface IAttacking : ITargetful
    {
        int AttackValue { get; set; }
        int AttackBaseValue { get; set; }
    }
}
