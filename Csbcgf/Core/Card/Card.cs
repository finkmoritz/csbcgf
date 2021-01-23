using System;
using System.Collections.Generic;
using System.Linq;
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

        [JsonIgnore]
        public int ManaValue {
            get => Math.Max(0, Components.Sum(c => c.ManaValue));
            set
            {
                Components.Add(new CardComponent(value - Components.Sum(c => c.ManaValue), 0));
            }
        }

        [JsonIgnore]
        public int ManaBaseValue {
            get => Math.Max(0, Components.Sum(c => c.ManaBaseValue));
            set
            {
                Components.Add(new CardComponent(0, value - Components.Sum(c => c.ManaBaseValue)));
            }
        }

        public virtual bool IsCastable(IGameState gameState)
        {
            IPlayer owner = FindParentPlayer(gameState);
            return owner != null
                && owner == gameState.ActivePlayer
                && ManaValue <= gameState.ActivePlayer.ManaValue;
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
