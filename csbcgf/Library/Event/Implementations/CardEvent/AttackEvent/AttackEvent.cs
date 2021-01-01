using System;
namespace csbcgf
{
    [Serializable]
    public abstract class AttackEvent : Event
    {
        protected Func<IMonsterCard> GetAttacker;
        protected Func<ICharacter> GetTarget;

        public AttackEvent(Func<IMonsterCard> getAttacker, Func<ICharacter> getTarget)
        {
            GetAttacker = getAttacker;
            GetTarget = getTarget;
        }

        public IMonsterCard Attacker { get => GetAttacker(); }
        public ICharacter Target { get => GetTarget(); }
    }
}
