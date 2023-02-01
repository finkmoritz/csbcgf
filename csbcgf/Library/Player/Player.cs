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
        protected ICardCollection deck = null!;

        [JsonProperty]
        protected ICardCollection hand = null!;

        [JsonProperty]
        protected ICardCollection board = null!;

        [JsonProperty]
        protected ICardCollection graveyard = null!;

        protected Player() { }

        /// <summary>
        /// Represents a Player.
        /// </summary>
        public Player(bool _ = true)
        {
            this.deck = new CardCollection();
            this.deck.Owner = this;

            this.hand = new CardCollection();
            this.hand.Owner = this;

            this.board = new CardCollection();
            this.board.Owner = this;

            this.graveyard = new CardCollection();
            this.graveyard.Owner = this;

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
                allCards.AddRange(Deck.Cards);
                allCards.AddRange(Hand.Cards);
                allCards.AddRange(Board.Cards);
                allCards.AddRange(Graveyard.Cards);
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
        public IEnumerable<ICharacter> Characters
        {
            get
            {
                List<ICharacter> characters = new List<ICharacter> { this };
                foreach (ICard card in Board.Cards)
                {
                    characters.Add((ICharacter)card);
                }
                return characters.ToImmutableList();
            }
        }

        [JsonIgnore]
        public IEnumerable<IReaction> Reactions
        {
            get => reactions.ToImmutableList();
        }

        [JsonIgnore]
        public ICardCollection Deck
        {
            get => deck;
        }

        [JsonIgnore]
        public ICardCollection Hand
        {
            get => hand;
        }

        [JsonIgnore]
        public ICardCollection Board
        {
            get => board;
        }

        [JsonIgnore]
        public ICardCollection Graveyard
        {
            get => graveyard;
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
            game.Execute(new DrawCardAction(this));
        }

        public void SummonMonster(IGame game, IMonsterCard monsterCard)
        {
            if (!monsterCard.IsSummonable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            if (Board.IsFull)
            {
                throw new CsbcgfException("Board has reached its maximum size!");
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
        }

        public ISet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            return new HashSet<ICharacter>();
        }

        public void ReactBefore(IGame game, IAction action)
        {
            foreach (IReaction reaction in AllReactions())
            {
                reaction.ReactBefore(game, action);
            }
        }

        public void ReactAfter(IGame game, IAction action)
        {
            foreach (IReaction reaction in AllReactions())
            {
                reaction.ReactAfter(game, action);
            }
        }
    }
}
