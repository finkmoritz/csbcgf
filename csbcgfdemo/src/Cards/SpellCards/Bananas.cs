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

        public override bool IsCastable(IGameState gameState)
        {
            return base.IsCastable(gameState)
                && gameState.AllCardsOnTheBoard.Count > 0;
        }

        public class BananasComponent : TargetfulSpellCardComponent
        {
            public BananasComponent() : base(1)
            {
            }

            public override void Cast(IGame game, ICharacter target)
            {
                game.Execute(new ModifyAttackStatAction(target, 1));
                game.Execute(new ModifyLifeStatAction(target, 1));
            }

            public override HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
            {
                HashSet<ICharacter> potentialTargets = new HashSet<ICharacter>();
                gameState.AllCardsOnTheBoard.ForEach(c => potentialTargets.Add((ICharacter)c));
                return potentialTargets;
            }
        }
    }
}
