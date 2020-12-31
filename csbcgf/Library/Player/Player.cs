using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class Player : IPlayer
    {
        [JsonIgnore]
        public bool IsAlive => lifeStat.Value > 0;

        public IStackedDeck Deck { get; protected set; }
        public IHand Hand { get; protected set; }
        public IBoard Board { get; protected set; }
        public IStackedDeck Graveyard { get; protected set; }

        [JsonIgnore]
        public IPlayer Owner {
            get => this;
            set => throw new CsbcgfException("Changing the Owner of a Player " +
                "is not allowed!");
        }

        [JsonProperty]
        protected ManaPoolStat manaPoolStat = new ManaPoolStat(0, 0);

        [JsonProperty]
        protected AttackStat attackStat = new AttackStat(0);

        [JsonProperty]
        protected LifeStat lifeStat = new LifeStat(0);

        /// <summary>
        /// Represents a Player and all his/her associated Cards.
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="maxHandSize"></param>
        /// <param name="maxBoardSize"></param>
        public Player(IStackedDeck deck, int maxHandSize = 10, int maxBoardSize = 6)
        {
            Deck = deck;
            Hand = new Hand(maxHandSize);
            Board = new Board(maxBoardSize);
            Graveyard = new StackedDeck();
        }

        [JsonIgnore]
        public List<ICard> AllCards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                allCards.AddRange(Deck.AllCards);
                allCards.AddRange(Hand.AllCards);
                allCards.AddRange(Board.AllCards);
                allCards.AddRange(Graveyard.AllCards);
                return allCards;
            }
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

        [JsonIgnore]
        public int ManaValue
        {
            get => manaPoolStat.Value;
            set => manaPoolStat.Value = value;
        }

        [JsonIgnore]
        public int ManaBaseValue
        {
            get => manaPoolStat.BaseValue;
            set => manaPoolStat.BaseValue = value;
        }

        [JsonIgnore]
        public List<ICharacter> Characters
        {
            get
            {
                List<ICharacter> characters = new List<ICharacter>
                {
                    this
                };
                Board.AllCards.ForEach(c => characters.Add((ICharacter)c));
                return characters;
            }
        }

        public void DrawCard(IGame game)
        {
            RemoveCardFromDeckAction removeAction = new RemoveCardFromDeckAction(Deck);
            game.Execute(new List<IAction>
            {
                new StartDrawCardEvent(),
                removeAction,
                new AddCardToHandAction(Hand, () => removeAction.Card),
                new EndDrawCardEvent(() => removeAction.Card)
            });
        }

        public void PlayMonster(IGame game, IMonsterCard monsterCard, int boardIndex)
        {
            if (!monsterCard.IsPlayable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new List<IAction>
            {
                new StartPlayMonsterCardEvent(monsterCard, boardIndex),
                new ModifyManaStatAction(this, -monsterCard.ManaValue, 0),
                new RemoveCardFromHandAction(Hand, monsterCard),
                new AddCardToBoardAction(Board, monsterCard, boardIndex),
                new EndPlayMonsterCardEvent(monsterCard, boardIndex)
            });
        }

        public void PlaySpell(IGame game, ITargetlessSpellCard spellCard)
        {
            if (!spellCard.IsPlayable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            List<IAction> actions = new List<IAction>
            {
                new StartPlayTargetlessSpellCardEvent(spellCard),
                new ModifyManaStatAction(this, -spellCard.ManaValue, 0),
                new RemoveCardFromHandAction(Hand, spellCard)
            };
            actions.AddRange(spellCard.GetActions(game));
            actions.Add(new AddCardToGraveyardAction(Graveyard, spellCard));
            actions.Add(new EndPlayTargetlessSpellCardEvent(spellCard));

            game.Execute(actions);
        }

        public void PlaySpell(IGame game, ITargetfulSpellCard spellCard, ICharacter target)
        {
            if (!spellCard.IsPlayable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            List<IAction> actions = new List<IAction>
            {
                new StartPlayTargetfulSpellCardEvent(spellCard, target),
                new ModifyManaStatAction(this, -spellCard.ManaValue, 0),
                new RemoveCardFromHandAction(Hand, spellCard)
            };
            actions.AddRange(spellCard.GetActions(game, target));
            actions.Add(new AddCardToGraveyardAction(Graveyard, spellCard));
            actions.Add(new EndPlayTargetfulSpellCardEvent(spellCard, target));

            game.Execute(actions);
        }

        public HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            return new HashSet<ICharacter>();
        }
    }
}
