using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class CardComponent : Reaction, ICardComponent
    {
        public List<IReaction> Reactions { get; }

        public CardComponent()
            : this(new List<IReaction>())
        {
        }

        [JsonConstructor]
        protected CardComponent(List<IReaction> reactions)
        {
            Reactions = reactions;
        }

        public List<IReaction> AllReactions()
        {
            return new List<IReaction>(Reactions);
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }

        public override object Clone()
        {
            List<IReaction> reactionsClone = new List<IReaction>();
            foreach (IReaction reaction in Reactions)
            {
                reactionsClone.Add((IReaction)reaction.Clone());
            }

            return new CardComponent(reactionsClone);
        }

        public ICard FindCard(IGameState gameState)
        {
            foreach (ICard card in gameState.AllCards)
            {
                foreach (ICardComponent cardComponent in card.Components)
                {
                    if (cardComponent == this)
                    {
                        return card;
                    }
                }
            }
            return null;
        }
    }
}
