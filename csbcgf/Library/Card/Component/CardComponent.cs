using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class CardComponent : ICardComponent
    {
        [JsonProperty]
        protected ManaCostStat manaCostStat;

        public List<IReaction> Reactions { get; }

        public ICard ParentCard { get; set; }

        public CardComponent(int mana)
            : this(new ManaCostStat(mana, mana), new List<IReaction>(), null)
        {
        }

        [JsonConstructor]
        protected CardComponent(ManaCostStat manaCostStat, List<IReaction> reactions, ICard parentCard)
        {
            this.manaCostStat = manaCostStat;
            Reactions = reactions;
            ParentCard = parentCard;
        }

        [JsonIgnore]
        public int ManaValue {
            get => manaCostStat.Value;
            set => manaCostStat.Value = value;
        }

        [JsonIgnore]
        public int ManaBaseValue {
            get => manaCostStat.BaseValue;
            set => manaCostStat.BaseValue = value;
        }

        public void AddReaction(IReaction reaction)
        {
            Reactions.Add(reaction);
        }

        public List<IAction> ReactTo(IGame game, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            Reactions.ForEach(r => reactions.AddRange(r.ReactTo(game, action)));
            return reactions;
        }

        public void RemoveReaction(IReaction reaction)
        {
            Reactions.Remove(reaction);
        }
    }
}
