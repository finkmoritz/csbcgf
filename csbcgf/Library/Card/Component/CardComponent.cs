using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class CardComponent : ICardComponent
    {
        public CardComponent(int mana)
        {
            Reactions = new List<IReaction>();
            manaCostStat = new ManaCostStat(mana, mana);
        }

        public List<IReaction> Reactions { get; }

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

        public ICard ParentCard { get; set; }

        [JsonProperty]
        protected ManaCostStat manaCostStat;

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
