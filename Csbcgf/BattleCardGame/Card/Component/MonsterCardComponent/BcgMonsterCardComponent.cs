using System;
using System.Collections.Generic;
using Csbcgf.BattleCardGame.SimpleBattleCardGame;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgMonsterCardComponent : BcgCardComponent, IBcgMonsterCardComponent
    {
        [JsonProperty]
        protected BcgAttackStat attackStat;

        [JsonProperty]
        protected BcgLifeStat lifeStat;

        public BcgMonsterCardComponent(int mana, int attack, int life)
            : this(mana, new BcgAttackStat(attack), new BcgLifeStat(life))
        {
        }

        public BcgMonsterCardComponent(int manaValue, int manaBaseValue,
            int attackValue, int attackBaseValue, int lifeValue, int lifeBaseValue)
            : base(manaValue, manaBaseValue)
        {
            attackStat = new BcgAttackStat(attackValue, attackBaseValue);
            lifeStat = new BcgLifeStat(lifeValue, lifeBaseValue);
        }

        public BcgMonsterCardComponent(int mana, BcgAttackStat attackStat, BcgLifeStat lifeStat)
            : base(mana)
        {
            this.attackStat = attackStat;
            this.lifeStat = lifeStat;
        }

        [JsonConstructor]
        protected BcgMonsterCardComponent(
            BcgManaCostStat manaCostStat,
            BcgAttackStat attackStat,
            BcgLifeStat lifeStat,
            List<IReaction> reactions
            ) : base(manaCostStat, reactions)
        {
            this.attackStat = attackStat;
            this.lifeStat = lifeStat;
        }

        [JsonIgnore]
        public int AttackValue
        {
            get => attackStat.Value;
            set => attackStat.Value = value;
        }

        [JsonIgnore]
        public int AttackBaseValue
        {
            get => attackStat.BaseValue;
            set => attackStat.BaseValue = value;
        }

        [JsonIgnore]
        public int LifeValue
        {
            get => lifeStat.Value;
            set => lifeStat.Value = value;
        }

        [JsonIgnore]
        public int LifeBaseValue
        {
            get => lifeStat.BaseValue;
            set => lifeStat.BaseValue = value;
        }

        public override object Clone()
        {
            List<IReaction> reactionsClone = new List<IReaction>();
            foreach (IReaction reaction in Reactions)
            {
                reactionsClone.Add((IReaction)reaction.Clone());
            }

            return new BcgMonsterCardComponent(
                (BcgManaCostStat)manaCostStat.Clone(),
                (BcgAttackStat)attackStat.Clone(),
                (BcgLifeStat)lifeStat.Clone(),
                reactionsClone
            );
        }

        public virtual HashSet<IBcgCharacter> GetPotentialTargets(IBcgGameState gameState)
        {
            SimpleBcgGame game = (SimpleBcgGame)gameState;
            HashSet<IBcgCharacter> potentialTargets = new HashSet<IBcgCharacter>();
            foreach (IBcgPlayer player in game.NonActivePlayers)
            {
                potentialTargets.Add(player);
                foreach (ICard card in player.CardCollections[SimpleBcgPlayer.CardCollectionKeyBoard])
                {
                    potentialTargets.Add((IBcgCharacter)card);
                }
            }
            return potentialTargets;
        }
    }
}
