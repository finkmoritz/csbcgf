using csbcgf;

namespace hearthstone
{
    public class HearthstoneMonsterCardComponent : HearthstoneCardComponent
    {
        protected HearthstoneMonsterCardComponent() { }

        public HearthstoneMonsterCardComponent(int mana, int attack, int life)
            : base(mana)
        {
            AddStat(StatKeys.Attack, new Stat(attack, attack));
            AddStat(StatKeys.Life, new Stat(life, life));
        }

        public virtual ISet<IStatContainer> GetPotentialTargets(IGameState gameState)
        {
            return new HashSet<IStatContainer>();
        }
    }
}
