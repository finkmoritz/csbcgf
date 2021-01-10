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

            public void ReactTo(IGame game, IActionEvent actionEvent)
            {
                /*if (actionEvent.IsAfter(typeof(EndPlaySpellCardEvent)))
                {
                    EndPlaySpellCardEvent a = (EndPlaySpellCardEvent)actionEvent.Action;
                    if (a.Card.Owner == ParentCard.Owner
                        && ParentCard.Owner.Board.Contains(ParentCard))
                    {
                        game.Execute(new ModifyAttackStatAction(ParentCard, 1));
                    }
                }*/
            }
        }
    }
}
