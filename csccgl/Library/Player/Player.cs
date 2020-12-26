using System;

namespace csccgl
{
    [Serializable]
    public class Player : IPlayer
    {
        public ManaStat ManaStat { get; }
        public AttackStat AttackStat { get; }
        public LifeStat LifeStat { get; }

        /// <summary>
        /// The Player's Deck of Cards.
        /// </summary>
        public readonly IStackedDeck Deck;

        /// <summary>
        /// The Player's Hand Cards.
        /// </summary>
        public readonly IHand Hand;

        /// <summary>
        /// The Player's Cards on the Board.
        /// </summary>
        public readonly IBoard Board;

        /// <summary>
        /// The Player's Cards that have been removed from the Game.
        /// </summary>
        public readonly IStackedDeck Graveyard;

        /// <summary>
        /// Represents a Player and all his/her associated Cards.
        /// </summary>
        /// <param name="deck"></param>
        public Player(IStackedDeck deck, int life = 30)
        {
            this.Deck = deck;
            this.ManaStat = new ManaStat(0, 10);
            this.AttackStat = new AttackStat(0);
            this.LifeStat = new LifeStat(life);
        }

        public void Attack(IGame game, IMonsterCard monsterCard, ICharacter targetCharacter)
        {
            if (monsterCard.IsPlayable(game))
            {
                monsterCard.Attack(targetCharacter);
            }
            else
            {
                throw new CsccglException("Tried to attack with a card that " +
                    "is not playable!");
            }
        }

        public ICard DrawCard()
        {
            if(!Deck.IsEmpty())
            {
                ICard card = Deck.PopCard();
                Hand.Add(card);
                return card;
            }
            return null;
        }

        public void PlayMonster(IGame game, IMonsterCard monsterCard, int boardIndex)
        {
            if (monsterCard.IsPlayable(game))
            {
                Hand.Remove(monsterCard);
                Board.AddAt(boardIndex, monsterCard);
            }
            else
            {
                throw new CsccglException("Tried to play a card that is " +
                    "not playable!");
            }
        }

        public void PlaySpell(IGame game, ITargetlessSpellCard spellCard)
        {
            if(spellCard.IsPlayable(game))
            {
                spellCard.Play(game);
            } else
            {
                throw new CsccglException("Tried to play a card that is " +
                    "not playable!");
            }
        }

        public void PlaySpell(IGame game, ITargetfulSpellCard spellCard, ICharacter targetCharacter)
        {
            if (spellCard.IsPlayable(game))
            {
                spellCard.Play(game, targetCharacter);
            }
            else
            {
                throw new CsccglException("Tried to play a card that is " +
                    "not playable!");
            }
        }
    }
}
