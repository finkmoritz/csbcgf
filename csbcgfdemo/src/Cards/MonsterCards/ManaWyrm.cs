using System;
using System.Collections.Generic;
using csbcgf;

namespace csbcgfdemo
{
    public class ManaWyrm : MonsterCard
    {
        public ManaWyrm() : base(2, 1, 3)
        {
            AddReaction(new ManaWyrmReaction(this));
        }

        /// <summary>
        /// Whenever you cast a spell, gain +1 Attack.
        /// </summary>
        public class ManaWyrmReaction : IReaction
        {
            public IMonsterCard ParentCard;

            public ManaWyrmReaction(IMonsterCard parentCard)
            {
                ParentCard = parentCard;
            }

            public List<IAction> ReactTo(IGame gameState, IAction action)
            {
                List<IAction> reactions = new List<IAction>();
                if (action is EndPlaySpellCardEvent a
                    && a.Card.Owner == ParentCard.Owner
                    && ParentCard.Owner.Board.Contains(ParentCard))
                {
                    reactions.Add(new ModifyAttackStatAction(ParentCard, 1));
                }
                return reactions;
            }
        }
    }
}
