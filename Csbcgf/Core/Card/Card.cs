using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public abstract class Card : ReactiveCompound, ICard
    {
        public Card() : this(new List<ICardComponent>(), new List<IReaction>())
        {
        }

        [JsonConstructor]
        protected Card(List<ICardComponent> components, List<IReaction> reactions)
            : base(components, reactions)
        {
        }

        public override ICard FindParentCard(IGameState gameState)
        {
            return this;
        }

        public override IPlayer FindParentPlayer(IGameState gameState)
        {
            foreach (IPlayer player in gameState.Players)
            {
                if (player.AllCards.Contains(this))
                {
                    return player;
                }
            }
            return null;
        }
    }
}
