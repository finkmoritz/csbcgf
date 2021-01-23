using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class CardComponent : Reaction, ICardComponent
    {
        [JsonProperty]
        protected ManaCostStat manaCostStat;

        public List<IReaction> Reactions { get; }

        public CardComponent(int mana)
            : this(new ManaCostStat(mana, mana), new List<IReaction>())
        {
        }

        public CardComponent(int manaValue, int manaBaseValue)
            : this(new ManaCostStat(manaValue, manaBaseValue), new List<IReaction>())
        {
        }

        [JsonConstructor]
        protected CardComponent(ManaCostStat manaCostStat, List<IReaction> reactions)
        {
            this.manaCostStat = manaCostStat;
            Reactions = reactions;
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

        public override object Clone()
        {
            List<IReaction> reactionsClone = new List<IReaction>();
            foreach (IReaction reaction in Reactions)
            {
                reactionsClone.Add((IReaction)reaction.Clone());
            }

            return new CardComponent(
                (ManaCostStat)manaCostStat.Clone(),
                reactionsClone
            );
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
