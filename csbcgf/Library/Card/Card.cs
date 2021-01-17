using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class Card : ReactiveCompound, ICard
    {
        public Card() : this(new List<ICardComponent>())
        {
        }

        [JsonConstructor]
        protected Card(List<ICardComponent> components)
            : base(components)
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
            IPlayer owner = FindOwner(gameState);
            return owner != null
                && owner == gameState.ActivePlayer
                && owner.Hand.Contains(this)
                && ManaValue <= gameState.ActivePlayer.ManaValue;
        }

        public override void AddComponent(ICardComponent cardComponent)
        {
            cardComponent.ParentCard = this;
            base.AddComponent(cardComponent);
        }

        public override void RemoveComponent(ICardComponent cardComponent)
        {
            base.RemoveComponent(cardComponent);
            cardComponent.ParentCard = null;
        }

        public IPlayer FindOwner(IGameState gameState)
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
