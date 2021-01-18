using System;
using csbcgf;
using Newtonsoft.Json;

namespace csbcgfdemo
{
    [Serializable]
    public class ManaWyrm : MonsterCard
    {
        [JsonConstructor]
        public ManaWyrm() : base(2, 1, 3)
        {
            AddReaction(new ManaWyrmReaction());
        }

        /// <summary>
        /// Whenever you cast a spell, gain +1 Attack.
        /// </summary>
        [Serializable]
        public class ManaWyrmReaction : IReaction
        {
            public object Clone()
            {
                return new ManaWyrmReaction();
            }

            public void ReactTo(IGame game, IActionEvent actionEvent)
            {
                if (actionEvent.IsAfter(typeof(CastSpellAction)))
                {
                    CastSpellAction a = (CastSpellAction)actionEvent.Action;
                    ICard parentCard = FindParentCard(game);
                    IPlayer spellCardOwner = a.SpellCard.FindOwner(game);
                    IPlayer manaWyrmOwner = parentCard.FindOwner(game);

                    if (spellCardOwner == manaWyrmOwner
                        && manaWyrmOwner.Board.Contains(parentCard))
                    {
                        game.Execute(new ModifyAttackStatAction((IAttacking)parentCard, 1));
                    }
                }
            }

            private ICard FindParentCard(IGameState gameState)
            {
                foreach (ICard card in gameState.AllCards)
                {
                    if (card.Reactions.Contains(this))
                    {
                        return card;
                    }
                }
                return null;
            }
        }
    }
}
