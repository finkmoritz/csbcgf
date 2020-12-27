using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public class Player : IPlayer
    {
        public ManaStat ManaStat { get; }
        public AttackStat AttackStat { get; }
        public LifeStat LifeStat { get; }

        public IStackedDeck Deck { get; protected set; }
        public IHand Hand { get; protected set; }
        public IBoard Board { get; protected set; }
        public IStackedDeck Graveyard { get; protected set; }

        public IPlayer Owner {
            get => this;
            set => throw new CsbcgfException("Changing the Owner of a Player " +
                "is not allowed!");
        }

        /// <summary>
        /// Represents a Player and all his/her associated Cards.
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="life"></param>
        public Player(IStackedDeck deck, int life = 30)
        {
            this.Deck = deck;
            this.Hand = new Hand();
            this.Board = new Board();
            this.Graveyard = new StackedDeck();

            this.ManaStat = new ManaStat(0, 10);
            this.AttackStat = new AttackStat(0);
            this.LifeStat = new LifeStat(life);
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

        public void Attack(IGame game, IMonsterCard monsterCard, ICharacter targetCharacter)
        {
            if (!Board.Contains(monsterCard))
            {
                throw new CsbcgfException("Failed to attack with a MonsterCard that is not " +
                    "on the Player's Board!");
            }
            if (targetCharacter is IPlayer && targetCharacter != game.NonActivePlayer
                || targetCharacter is ICard card && !game.NonActivePlayer.Board.Contains(card))
            {
                throw new CsbcgfException("Failed to attack a target Character that is neither " +
                    "the opposing Player nor a Card on the non-active Player's Board!");
            }
            if (!monsterCard.IsReadyToAttack)
            {
                throw new CsbcgfException("Tried to attack with a monster card that " +
                    "is not ready to attack!");
            }

            monsterCard.Attack(game, targetCharacter);
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

            PayCosts(game, monsterCard.ManaStat.Value);
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

            PayCosts(game, spellCard.ManaStat.Value);
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

            PayCosts(game, spellCard.ManaStat.Value);
            game.Queue(new RemoveCardFromHandAction(Hand, spellCard));
            spellCard.Play(game, targetCharacter);
            game.Queue(new AddCardToGraveyardAction(Graveyard, spellCard));
            game.Process();
        }

        private void PayCosts(IGame game, int mana)
        {
            if(ManaStat.Value < mana)
            {
                throw new CsbcgfException("Cannot pay costs of " + mana + " mana as this player has only " +
                    ManaStat.Value + " mana left!");
            } else
            {
                game.Queue(new ModifyManaStatAction(ManaStat, -mana));
            }
        }

        public bool IsAlive() => LifeStat.Value > 0;
    }
}
