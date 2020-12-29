using System;
using System.Collections.Generic;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public class Player : IPlayer
    {
        public bool IsAlive => lifeStat.Value > 0;

        public IStackedDeck Deck { get; protected set; }
        public IHand Hand { get; protected set; }
        public IBoard Board { get; protected set; }
        public IStackedDeck Graveyard { get; protected set; }

        public IPlayer Owner {
            get => this;
            set => throw new CsbcgfException("Changing the Owner of a Player " +
                "is not allowed!");
        }

        protected ManaPoolStat manaPoolStat = new ManaPoolStat(0, 0);
        protected AttackStat attackStat = new AttackStat(0);
        protected LifeStat lifeStat;

        /// <summary>
        /// Represents a Player and all his/her associated Cards.
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="life"></param>
        public Player(IStackedDeck deck, int life = 30)
        {
            Deck = deck;
            Hand = new Hand();
            Board = new Board();
            Graveyard = new StackedDeck();

            lifeStat = new LifeStat(life);
        }

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

        public int AttackValue
        {
            get => attackStat.Value;
            set => attackStat.Value = value;
        }

        public int AttackBaseValue
        {
            get => attackStat.BaseValue;
            set => attackStat.BaseValue = value;
        }

        public int LifeValue
        {
            get => lifeStat.Value;
            set => lifeStat.Value = value;
        }

        public int LifeBaseValue
        {
            get => lifeStat.BaseValue;
            set => lifeStat.BaseValue = value;
        }

        public int ManaValue
        {
            get => manaPoolStat.Value;
            set => manaPoolStat.Value = value;
        }

        public int ManaBaseValue
        {
            get => manaPoolStat.BaseValue;
            set => manaPoolStat.BaseValue = value;
        }

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
                removeAction,
                new AddCardToHandAction(Hand, () => removeAction.card)
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
                new ModifyManaStatAction(this, -monsterCard.ManaValue, 0),
                new RemoveCardFromHandAction(Hand, monsterCard),
                new AddCardToBoardAction(Board, monsterCard, boardIndex)
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
                new ModifyManaStatAction(this, -spellCard.ManaValue, 0),
                new RemoveCardFromHandAction(Hand, spellCard)
            };
            actions.AddRange(spellCard.GetActions(game));
            actions.Add(new AddCardToGraveyardAction(Graveyard, spellCard));

            game.Execute(actions);
        }

        public void PlaySpell(IGame game, ITargetfulSpellCard spellCard, ICharacter targetCharacter)
        {
            if (!spellCard.IsPlayable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            List<IAction> actions = new List<IAction>
            {
                new ModifyManaStatAction(this, -spellCard.ManaValue, 0),
                new RemoveCardFromHandAction(Hand, spellCard)
            };
            actions.AddRange(spellCard.GetActions(game, targetCharacter));
            actions.Add(new AddCardToGraveyardAction(Graveyard, spellCard));

            game.Execute(actions);
        }

        public HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            return new HashSet<ICharacter>();
        }
    }
}
