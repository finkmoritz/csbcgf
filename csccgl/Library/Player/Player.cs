using System;

namespace csccgl
{
    [Serializable]
    public class Player : IPlayer
    {
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
            this.AttackStat.Value = 0;
            this.LifeStat.Value = life;
        }

        public void Attack(IMonsterCard monsterCard, ICharacter targetCharacter)
        {
            monsterCard.Attack(targetCharacter);
        }

        public ICard DrawCard()
        {
            ICard card = Deck.PopCard();
            Hand.Add(card);
            return card;
        }

        public void PlayCard(IMonsterCard monsterCard, int boardIndex)
        {
            Hand.Remove(monsterCard);
            Board.AddAt(boardIndex, monsterCard);
        }

        public void PlayCard(ITargetlessSpellCard spellCard)
        {
            throw new NotImplementedException(); //TODO
        }

        public void PlayCard(ITargetfulSpellCard spellCard, ICharacter targetCharacter)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
