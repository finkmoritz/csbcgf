using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgCardComponent : CardComponent, IBcgCardComponent
    {
        [JsonProperty]
        protected BcgManaCostStat manaCostStat;

        public List<IReaction> Reactions { get; }

        public BcgCardComponent(int mana)
            : this(new BcgManaCostStat(mana, mana), new List<IReaction>())
        {
        }

        public BcgCardComponent(int manaValue, int manaBaseValue)
            : this(new BcgManaCostStat(manaValue, manaBaseValue), new List<IReaction>())
        {
        }

        [JsonConstructor]
        protected BcgCardComponent(BcgManaCostStat manaCostStat, List<IReaction> reactions)
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
                (BcgManaCostStat)manaCostStat.Clone(),
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
