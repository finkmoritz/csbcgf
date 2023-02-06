using System.Collections.Immutable;
using Newtonsoft.Json;

namespace csbcgf
{
    public class Player : IPlayer
    {
        [JsonProperty]
        protected ManaPoolStat manaPoolStat = null!;

        [JsonProperty]
        protected AttackStat attackStat = null!;

        [JsonProperty]
        protected LifeStat lifeStat = null!;

        [JsonProperty]
        protected List<IReaction> reactions = null!;

        [JsonProperty]
        protected IDictionary<string, ICardCollection> cardCollections = null!;

        protected Player() { }

        /// <summary>
        /// Represents a Player.
        /// </summary>
        public Player(bool _ = true)
        {
            this.cardCollections = new Dictionary<string, ICardCollection>();

            this.manaPoolStat = new ManaPoolStat(0, 0);
            this.attackStat = new AttackStat(0);
            this.lifeStat = new LifeStat(0);
            this.reactions = new List<IReaction>();
        }

        [JsonIgnore]
        public bool IsAlive => lifeStat.Value > 0;

        [JsonIgnore]
        public IEnumerable<ICard> AllCards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach(ICardCollection cardCollection in cardCollections.Values)
                {
                    allCards.AddRange(cardCollection.Cards);
                }
                return allCards.ToImmutableList();
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

        [JsonIgnore]
        public IEnumerable<IReaction> Reactions
        {
            get => reactions.ToImmutableList();
        }

        public ICardCollection GetCardCollection(string key)
        {
            return cardCollections[key];
        }

        public void AddCardCollection(string key, ICardCollection cardCollection)
        {
            cardCollections.Add(key, cardCollection);
            cardCollection.Owner = this;
        }

        public bool RemoveCardCollection(string key)
        {
            if(!cardCollections.ContainsKey(key))
            {
                return false;
            }
            cardCollections[key].Owner = null;
            return cardCollections.Remove(key);
        }

        public IEnumerable<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            foreach (ICard card in AllCards)
            {
                allReactions.AddRange(card.AllReactions());
            }
            return allReactions.ToImmutableList();
        }

        public void AddReaction(IReaction reaction)
        {
            reactions.Add(reaction);
        }

        public bool RemoveReaction(IReaction reaction)
        {
            return reactions.Remove(reaction);
        }

        public void DrawCard(IGame game)
        {
            game.ActionQueue.Execute(new DrawCardAction(this));
        }

        public void SummonMonster(IGame game, IMonsterCard monsterCard)
        {
            if (!monsterCard.IsSummonable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.ActionQueue.Execute(new SummonMonsterAction(this, monsterCard));
        }

        public void CastSpell(IGame game, ITargetlessSpellCard spellCard)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.ActionQueue.Execute(new CastTargetlessSpellAction(this, spellCard));
        }

        public void CastSpell(IGame game, ITargetfulSpellCard spellCard, ICharacter target)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.ActionQueue.Execute(new CastTargetfulSpellAction(this, spellCard, target));
        }

        public ISet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            return new HashSet<ICharacter>();
        }
    }
}
