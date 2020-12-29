using System;
namespace csbcgf
{
    [Serializable]
    public class StartAttackEvent : AttackEvent
    {
        public StartAttackEvent(Func<IMonsterCard> getAttacker, Func<ICharacter> getTarget)
            : base(getAttacker, getTarget)
        {
        }

        public StartAttackEvent(IMonsterCard attacker, ICharacter target)
            : this(() => attacker, () => target)
        {
        }

        public StartAttackEvent(Func<IMonsterCard> getAttacker, ICharacter target)
            : this(getAttacker, () => target)
        {
        }

        public StartAttackEvent(IMonsterCard attacker, Func<ICharacter> getTarget)
            : this(() => attacker, getTarget)
        {
        }
    }
}
