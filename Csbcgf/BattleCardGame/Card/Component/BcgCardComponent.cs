using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgCardComponent : CardComponent, ICardComponent
    {
        [JsonProperty]
        protected BcgManaCostStat manaCostStat;

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
            : base(reactions)
        {
            this.manaCostStat = manaCostStat;
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

            return new BcgCardComponent(
                (BcgManaCostStat)manaCostStat.Clone(),
                reactionsClone
            );
        }
    }
}
