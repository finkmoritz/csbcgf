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

        public void DrawCard(IGame game)
        {
            RemoveCardFromDeckAction removeAction = new RemoveCardFromDeckAction(Deck);
            game.Queue(removeAction);
            game.Queue(new AddCardToHandAction(Hand, () => removeAction.card));

            game.Process();
        }

        public void PlayMonster(IGame game, IMonsterCard monsterCard, int boardIndex)
        {
            if (!Hand.Contains(monsterCard))
            {
                throw new CsbcgfException("Failed to play a MonsterCard that is not " +
                    "on the Player's Hand!");
            }
            if (!monsterCard.IsPlayable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            PayCosts(game, monsterCard.ManaValue);
            game.Queue(new RemoveCardFromHandAction(Hand, monsterCard));
            game.Queue(new AddCardToBoardAction(Board, monsterCard, boardIndex));
            game.Process();
        }

        public void PlaySpell(IGame game, ITargetlessSpellCard spellCard)
        {
            if (!Hand.Contains(spellCard))
            {
                throw new CsbcgfException("Failed to play a SpellCard that is not " +
                    "on the Player's Hand!");
            }
            if (!spellCard.IsPlayable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            PayCosts(game, spellCard.ManaValue);
            game.Queue(new RemoveCardFromHandAction(Hand, spellCard));
            spellCard.Play(game);
            game.Queue(new AddCardToGraveyardAction(Graveyard, spellCard));
            game.Process();
        }

        public void PlaySpell(IGame game, ITargetfulSpellCard spellCard, ICharacter targetCharacter)
        {
            if (!Hand.Contains(spellCard))
            {
                throw new CsbcgfException("Failed to play a SpellCard that is not " +
                    "on the Player's Hand!");
            }
            if (targetCharacter is ICard
                && !game.ActivePlayer.Board.Contains((ICard)targetCharacter)
                && !game.NonActivePlayer.Board.Contains((ICard)targetCharacter))
            {
                throw new CsbcgfException("Failed to attack a target Character that is neither " +
                    "a Player nor a Card on a Player's Board!");
            }
            if (!spellCard.IsPlayable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            PayCosts(game, spellCard.ManaValue);
            game.Queue(new RemoveCardFromHandAction(Hand, spellCard));
            spellCard.Play(game, targetCharacter);
            game.Queue(new AddCardToGraveyardAction(Graveyard, spellCard));
            game.Process();
        }

        private void PayCosts(IGame game, int mana)
        {
            if(ManaValue < mana)
            {
                throw new CsbcgfException("Cannot pay costs of " + mana + " mana as this player has only " +
                    ManaValue + " mana left!");
            } else
            {
                game.Queue(new ModifyManaStatAction(this, -mana, 0));
            }
        }

        public HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            return new HashSet<ICharacter>();
        }
    }
}
