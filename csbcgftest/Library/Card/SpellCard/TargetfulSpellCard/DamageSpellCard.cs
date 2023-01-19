using csbcgf;

namespace csbcgftest
{
    
    public class DamageSpellCard : TargetfulSpellCard
    {
        protected uint damage;

        public DamageSpellCard(uint damage)
            : this(damage, new List<ICardComponent>(), new List<IReaction>())
        {
            Components.Add(new DamageSpellCardComponent(this, (int)damage, damage));
        }

        protected DamageSpellCard(
            uint damage,
            List<ICardComponent> components,
            List<IReaction> reactions
            ) : base(components, reactions)
        {
            this.damage = damage;
        }

        public class DamageSpellCardComponent : TargetfulSpellCardComponent
        {
            private readonly uint damage;

            public DamageSpellCardComponent(ICard card, int mana, uint damage)
                : this(card, damage, new ManaCostStat(mana, mana), new List<IReaction>())
            {
            }

            public DamageSpellCardComponent(
                ICard card,
                uint damage,
                ManaCostStat manaCostStat,
                List<IReaction> reactions
                ) : base(card, manaCostStat, reactions)
            {
                this.damage = damage;
            }

            public override void Cast(IGame game, ICharacter target)
            {
                game.Execute(new ModifyLifeStatAction(target, -(int)damage));
            }

            public override HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
            {
                HashSet<ICharacter> targets = new HashSet<ICharacter>();
                foreach (IPlayer player in gameState.Players)
                {
                    targets.Add(player);
                    player.Board.AllCards.ForEach(c => targets.Add((ICharacter)c));
                }
                return targets;
            }
        }
    }
}
