using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class Card : ReactiveCompound, ICard
    {
        public IPlayer Owner { get; set; }

        public Card(List<ICardComponent> components)
            : this(components, null)
        {
        }

        public Card() : this(new List<ICardComponent>())
        {
        }

        [JsonConstructor]
        protected Card(List<ICardComponent> components, IPlayer owner)
            : base(components)
        {
            Owner = owner;
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
            return Owner == gameState.ActivePlayer
                && Owner.Hand.Contains(this)
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
    }
}
