using csbcgf;

namespace hearthstone
{
    /// <summary>
    /// Bananas give a minion +1/+1.
    /// </summary>
    public class Bananas : TargetfulSpellCard
    {
        protected Bananas() { }

        public Bananas(bool _ = true) : base(_)
        {
            AddComponent(new BananasComponent());
        }

        public override bool IsCastable(IGameState gameState)
        {
            return base.IsCastable(gameState)
                && gameState.Players.Any(p => !p.GetCardCollection(CardCollectionKeys.Board).IsEmpty);
        }

        public class BananasComponent : TargetfulSpellCardComponent
        {
            protected BananasComponent() { }

            public BananasComponent(bool _ = true) : base(1)
            {
            }

            public override void Cast(IGame game, ICharacter target)
            {
                game.ActionQueue.Execute(new ModifyAttackStatAction(target, 1));
                game.ActionQueue.Execute(new ModifyLifeStatAction(target, 1));
            }

            public override HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
            {
                HashSet<ICharacter> potentialTargets = new HashSet<ICharacter>();
                foreach (ICard card in gameState.Players.Aggregate(new List<ICard>(), (cards, player) => {
                    cards.AddRange(player.GetCardCollection(CardCollectionKeys.Board).Cards);
                    return cards;
                }))
                {
                    potentialTargets.Add((ICharacter)card);
                }
                return potentialTargets;
            }
        }
    }
}
