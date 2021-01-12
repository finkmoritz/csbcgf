using System.Collections.Generic;


namespace csbcgf
{
    public interface IPlayer : IManaful, ICharacter, IReactive
    {
        /// <summary>
        /// The Player's Deck of Cards.
        /// </summary>
        IDeck Deck { get; }

        /// <summary>
        /// The Player's Hand Cards.
        /// </summary>
        IHand Hand { get; }

        /// <summary>
        /// The Player's Cards on the Board.
        /// </summary>
        IBoard Board { get; }

        /// <summary>
        /// The Player's Cards that have been removed from the Game.
        /// </summary>
        IDeck Graveyard { get; }

        /// <summary>
        /// Get all Cards from the Player's Decks.
        /// </summary>
        List<ICard> AllCards { get; }

        /// <summary>
        /// Convenience property to retrieve this player's characters, i.e.
        /// the player himself and all cards on his/her board.
        /// </summary>
        List<ICharacter> Characters { get; }

        /// <summary>
        /// Draw a Card from the Deck and add it to the Hand.
        /// </summary>
        /// <param name="game"></param>
        void DrawCard(IGame game);

        /// <summary>
        /// Cast a MonsterCard from the Player's Hand to the Board at
        /// position boardIndex.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="monsterCard"></param>
        /// <param name="boardIndex"></param>
        void CastMonster(IGame game, IMonsterCard monsterCard, int boardIndex);

        /// <summary>
        /// Cast a SpellCard from the Player's Hand that needs no target.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spellCard"></param>
        void CastSpell(IGame game, ITargetlessSpellCard spellCard);

        /// <summary>
        /// Cast a SpellCard from the Player's Hand onto the specified
        /// target Character.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spellCard"></param>
        /// <param name="target"></param>
        void CastSpell(IGame game, ITargetfulSpellCard spellCard, ICharacter target);
    }
}
