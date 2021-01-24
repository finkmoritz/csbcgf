using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class Player : IPlayer
    {
        [JsonProperty]
        protected BcgManaPoolStat manaPoolStat;

        [JsonProperty]
        protected BcgAttackStat attackStat;

        [JsonProperty]
        protected BcgLifeStat lifeStat;

        public List<IReaction> Reactions { get; }

        public Dictionary<string, ICardCollection> CardCollections { get; }

        /// <summary>
        /// Represents a Player and all his/her associated Cards.
        /// </summary>
        public Player()
            : this(
                  new BcgManaPoolStat(0, 0),
                  new BcgAttackStat(0),
                  new BcgLifeStat(0),
                  new List<IReaction>(),
                  new Dictionary<string, ICardCollection>())
        {
        }

        public Player(int mana, int attack, int life)
            : this(
                  new BcgManaPoolStat(mana, mana),
                  new BcgAttackStat(attack),
                  new BcgLifeStat(life),
                  new List<IReaction>(),
                  new Dictionary<string, ICardCollection>())
        {
        }

        [JsonConstructor]
        protected Player(
            BcgManaPoolStat manaPoolStat,
            BcgAttackStat attackStat,
            BcgLifeStat lifeStat,
            List<IReaction> reactions,
            Dictionary<string, ICardCollection> cardCollections)
        {
            this.manaPoolStat = manaPoolStat;
            this.attackStat = attackStat;
            this.lifeStat = lifeStat;
            Reactions = reactions;
            CardCollections = cardCollections;
        }

        [JsonIgnore]
        public bool IsAlive => lifeStat.Value > 0;

        [JsonIgnore]
        public List<ICard> AllCards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach (ICardCollection cc in CardCollections.Values)
                {
                    allCards.AddRange(cc.AllCards);
                }
                return allCards;
            }
        }

        [JsonIgnore]
        public int AttackValue
        {
            get => attackStat.Value;
            set => attackStat.Value = Math.Max(0, value);
        }

        [JsonIgnore]
        public int AttackBaseValue
        {
            get => attackStat.BaseValue;
            set => attackStat.BaseValue = Math.Max(0, value);
        }

        [JsonIgnore]
        public int LifeValue
        {
            get => lifeStat.Value;
            set => lifeStat.Value = Math.Max(0, value);
        }

        [JsonIgnore]
        public int LifeBaseValue
        {
            get => lifeStat.BaseValue;
            set => lifeStat.BaseValue = Math.Max(0, value);
        }

        [JsonIgnore]
        public int ManaValue
        {
            get => manaPoolStat.Value;
            set => manaPoolStat.Value = Math.Max(0, value);
        }

        [JsonIgnore]
        public int ManaBaseValue
        {
            get => manaPoolStat.BaseValue;
            set => manaPoolStat.BaseValue = Math.Max(0, value);
        }

        public List<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            AllCards.ForEach(c => allReactions.AddRange(c.AllReactions()));
            return allReactions;
        }

        public HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            return new HashSet<ICharacter>();
        }

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }

        public virtual object Clone()
        {
            List<IReaction> reactionsClone = new List<IReaction>();
            foreach (IReaction reaction in Reactions)
            {
                reactionsClone.Add((IReaction)reaction.Clone());
            }

            Dictionary<string, ICardCollection> cardCollectionsClone = new Dictionary<string, ICardCollection>();
            foreach (KeyValuePair<string, ICardCollection> kv in CardCollections)
            {
                cardCollectionsClone.Add(kv.Key, (ICardCollection)kv.Value.Clone());
            }

            return new Player(
                (BcgManaPoolStat)manaPoolStat.Clone(),
                (BcgAttackStat)attackStat.Clone(),
                (BcgLifeStat)lifeStat.Clone(),
                reactionsClone,
                cardCollectionsClone
            );
        }

        public ICard FindParentCard(IGameState gameState)
        {
            throw new CsbcgfException("Cannot use method 'FindParentCard' on " +
                "instance of type 'Player'");
        }

        public IPlayer FindParentPlayer(IGameState gameState)
        {
            return this;
        }
    }
}
