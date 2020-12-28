using System;
using System.Collections.Generic;
using System.Linq;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public abstract class Card : ReactiveCompound, ICard
    {
        public Card(List<ICardComponent> components)
            : base(components)
        {
        }

        public Card() : this(new List<ICardComponent>())
        {
        }

        public IPlayer Owner { get; set; }

        public int ManaValue {
            get => manaCostStat.Value + Components.Sum(c => c.ManaValue);
            set => manaCostStat.Value = value - Components.Sum(c => c.ManaValue);
        }

        public int ManaBaseValue {
            get => manaCostStat.BaseValue + Components.Sum(c => c.ManaBaseValue);
            set => manaCostStat.BaseValue = value - Components.Sum(c => c.ManaBaseValue);
        }

        protected ManaCostStat manaCostStat = new ManaCostStat(0, 0);

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
