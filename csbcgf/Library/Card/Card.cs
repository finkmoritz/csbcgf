using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class Card : ReactiveCompound, ICard
    {
        [JsonProperty]
        protected ManaCostStat manaCostOffsetStat;

        public IPlayer Owner { get; set; }

        public Card(List<ICardComponent> components)
            : this(components, new ManaCostStat(0, 0), null)
        {
        }

        public Card() : this(new List<ICardComponent>())
        {
        }

        [JsonConstructor]
        protected Card(List<ICardComponent> components, ManaCostStat manaCostOffsetStat, IPlayer owner)
            : base(components)
        {
            this.manaCostOffsetStat = manaCostOffsetStat;
            Owner = owner;
        }

        [JsonIgnore]
        public int ManaValue {
            get => manaCostOffsetStat.Value + Components.Sum(c => c.ManaValue);
            set => manaCostOffsetStat.Value = value - Components.Sum(c => c.ManaValue);
        }

        [JsonIgnore]
        public int ManaBaseValue {
            get => manaCostOffsetStat.BaseValue + Components.Sum(c => c.ManaBaseValue);
            set => manaCostOffsetStat.BaseValue = value - Components.Sum(c => c.ManaBaseValue);
        }

        public virtual bool IsPlayable(IGame game)
        {
            return Owner == game.ActivePlayer
                && Owner.Hand.Contains(this)
                && ManaValue <= game.ActivePlayer.ManaValue;
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
