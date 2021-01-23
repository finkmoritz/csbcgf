using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class Player : IPlayer
    {
        [JsonProperty]
        protected ManaPoolStat manaPoolStat;

        [JsonProperty]
        protected AttackStat attackStat;

        [JsonProperty]
        protected LifeStat lifeStat;

        public List<IReaction> Reactions { get; }

        public Dictionary<string, ICardCollection> CardCollections { get; }

        /// <summary>
        /// Represents a Player and all his/her associated Cards.
        /// </summary>
        public Player()
            : this(
                  new ManaPoolStat(0, 0),
                  new AttackStat(0),
                  new LifeStat(0),
                  new List<IReaction>(),
                  new Dictionary<string, ICardCollection>())
        {
        }

        public Player(int mana, int attack, int life)
            : this(
                  new ManaPoolStat(mana, mana),
                  new AttackStat(attack),
                  new LifeStat(life),
                  new List<IReaction>(),
                  new Dictionary<string, ICardCollection>())
        {
        }

        [JsonConstructor]
        protected Player(
            ManaPoolStat manaPoolStat,
            AttackStat attackStat,
            LifeStat lifeStat,
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

        /*public void DrawCard(IGame game)
        {
            game.Execute(new DrawCardAction(this));
        }

        public void CastMonster(IGame game, IMonsterCard monsterCard)
        {
            if (!monsterCard.IsSummonable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new CastMonsterAction(this, monsterCard));
        }

        public void CastSpell(IGame game, ITargetlessSpellCard spellCard)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new CastTargetlessSpellAction(this, spellCard));
        }

        public void CastSpell(IGame game, ITargetfulSpellCard spellCard, ICharacter target)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new CastTargetfulSpellAction(this, spellCard, target));
        }*/ //TODO

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
                (ManaPoolStat)manaPoolStat.Clone(),
                (AttackStat)attackStat.Clone(),
                (LifeStat)lifeStat.Clone(),
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
