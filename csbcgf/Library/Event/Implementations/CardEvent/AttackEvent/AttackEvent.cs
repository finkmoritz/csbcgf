using System;
namespace csbcgf
{
    [Serializable]
    public abstract class AttackEvent : Event
    {
        public IMonsterCard Attacker { get => GetAttacker(); }
        public ICharacter Target { get => GetTarget(); }

        protected Func<IMonsterCard> GetAttacker;
        protected Func<ICharacter> GetTarget;

        public AttackEvent(Func<IMonsterCard> getAttacker, Func<ICharacter> getTarget)
        {
            GetAttacker = getAttacker;
            GetTarget = getTarget;
        }
    }
}
