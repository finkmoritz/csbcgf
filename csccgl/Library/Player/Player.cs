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
        /// <param name="life"></param>
        public Player(IStackedDeck deck, int life = 30)
        {
            this.Deck = deck;
            this.ManaStat = new ManaStat(0, 10);
            this.AttackStat = new AttackStat(0);
            this.LifeStat = new LifeStat(life);
        }

        public void Attack(IGame game, IMonsterCard monsterCard, ICharacter targetCharacter)
        {
            if (!Board.Contains(monsterCard))
            {
                throw new CsccglException("Failed to attack with a MonsterCard that is not " +
                    "on the Player's Board!");
            }
            if (targetCharacter is IPlayer && targetCharacter != game.NonActivePlayer
                || targetCharacter is ICard && !game.NonActivePlayer.Board.Contains((ICard)targetCharacter))
            {
                throw new CsccglException("Failed to attack a target Character that is neither " +
                    "the opposing Player nor a Card on the non-active Player's Board!");
            }
            if (!monsterCard.IsPlayable(game))
            {
                throw new CsccglException("Tried to attack with a card that " +
                    "is not playable!");
            }

            monsterCard.Attack(targetCharacter);

            if(!monsterCard.IsAlive())
            {
                Board.Remove(monsterCard);
                Graveyard.Push(monsterCard);
            }
            if(targetCharacter is ICard && !targetCharacter.IsAlive())
            {
                game.NonActivePlayer.Board.Remove((ICard)targetCharacter);
                game.NonActivePlayer.Graveyard.Push((ICard)targetCharacter);
            }
        }

        public ICard DrawCard()
        {
            if(!Deck.IsEmpty())
            {
                ICard card = Deck.Pop();
                Hand.Add(card);
                return card;
            }
            return null;
        }

        public void PlayMonster(IGame game, IMonsterCard monsterCard, int boardIndex)
        {
            if (!Hand.Contains(monsterCard))
            {
                throw new CsccglException("Failed to play a MonsterCard that is not " +
                    "on the Player's Hand!");
            }
            if (!monsterCard.IsPlayable(game))
            {
                throw new CsccglException("Tried to play a card that is " +
                    "not playable!");
            }

            payCosts(monsterCard.ManaStat.Value);
            Hand.Remove(monsterCard);
            Board.AddAt(boardIndex, monsterCard);
        }

        public void PlaySpell(IGame game, ITargetlessSpellCard spellCard)
        {
            if (!Hand.Contains(spellCard))
            {
                throw new CsccglException("Failed to play a SpellCard that is not " +
                    "on the Player's Hand!");
            }
            if (!spellCard.IsPlayable(game))
            {
                throw new CsccglException("Tried to play a card that is " +
                    "not playable!");
            }

            payCosts(spellCard.ManaStat.Value);
            Hand.Remove(spellCard);
            spellCard.Play(game);
            Graveyard.Push(spellCard);
        }

        public void PlaySpell(IGame game, ITargetfulSpellCard spellCard, ICharacter targetCharacter)
        {
            if (!Hand.Contains(spellCard))
            {
                throw new CsccglException("Failed to play a SpellCard that is not " +
                    "on the Player's Hand!");
            }
            if (targetCharacter is ICard
                && !game.ActivePlayer.Board.Contains((ICard)targetCharacter)
                && !game.NonActivePlayer.Board.Contains((ICard)targetCharacter))
            {
                throw new CsccglException("Failed to attack a target Character that is neither " +
                    "a Player nor a Card on a Player's Board!");
            }
            if (!spellCard.IsPlayable(game))
            {
                throw new CsccglException("Tried to play a card that is " +
                    "not playable!");
            }

            payCosts(spellCard.ManaStat.Value);
            Hand.Remove(spellCard);
            spellCard.Play(game, targetCharacter);
            Graveyard.Push(spellCard);
        }

        private void payCosts(int mana)
        {
            if(ManaStat.Value < mana)
            {
                throw new CsccglException("Cannot pay costs of " + mana + " mana as this player has only " +
                    ManaStat.Value + " mana left!");
            }
            ManaStat.Value -= mana;
        }

        public bool IsAlive() => LifeStat.Value > 0;
    }
}
