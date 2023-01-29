﻿using Newtonsoft.Json;

namespace csbcgf
{
    public class CardComponent : Reaction, ICardComponent
    {
        [JsonProperty]
        protected ManaCostStat manaCostStat = null!;

        [JsonProperty]
        protected List<IReaction> reactions = null!;

        [JsonProperty]
        protected ICompound? parentCard;

        protected CardComponent() {}

        public CardComponent(int mana)
            : this(new ManaCostStat(mana, mana), new List<IReaction>())
        {
        }

        public CardComponent(int manaValue, int manaBaseValue)
            : this(new ManaCostStat(manaValue, manaBaseValue), new List<IReaction>())
        {
        }

        protected CardComponent(ManaCostStat manaCostStat, List<IReaction> reactions)
        {
            this.manaCostStat = manaCostStat;
            this.reactions = reactions;
        }

        [JsonIgnore]
        public List<IReaction> Reactions {
            get => reactions;
        }

        [JsonIgnore]
        public ICompound? ParentCard {
            get {
                return parentCard;
            }
            set {
                parentCard = value;
            }
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

        public List<IReaction> AllReactions()
        {
            return new List<IReaction>(Reactions);
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }
    }
}
