using System;
using System.Collections.Generic;
using csbcgf;

namespace csbcgfdemo
{
    /// <summary>
    /// Bananas give a minion +1/+1.
    /// </summary>
    public class Bananas : TargetfulSpellCard
    {
        public Bananas() : base(new BananasComponent())
        {
        }

        public override bool IsPlayable(IGameState gameState)
        {
            return base.IsPlayable(gameState)
                && gameState.AllCardsOnTheBoard.Count > 0;
        }

        public class BananasComponent : TargetfulSpellCardComponent
        {
            public BananasComponent() : base(1)
            {
            }

            public override List<IAction> GetActions(IGame gameState, ICharacter target)
            {
                return new List<IAction>
                {
                    new ModifyAttackStatAction(target, 1),
                    new ModifyLifeStatAction(target, 1)
                };
            }

            public override HashSet<ICharacter> GetPotentialTargets(IGame gameState)
            {
                HashSet<ICharacter> potentialTargets = new HashSet<ICharacter>();
                gameState.AllCardsOnTheBoard.ForEach(c => potentialTargets.Add((ICharacter)c));
                return potentialTargets;
            }
        }
    }
}
