namespace csbcgf
{
    public interface IPlayer : IManaful, ICharacter, IReactive
    {
        /// <summary>
        /// The Player's Deck of Cards.
        /// </summary>
        ICardCollection Deck { get; }

        /// <summary>
        /// The Player's Hand Cards.
        /// </summary>
        ICardCollection Hand { get; }

        /// <summary>
        /// The Player's Cards on the Board.
        /// </summary>
        ICardCollection Board { get; }

        /// <summary>
        /// The Player's Cards that have been removed from the Game.
        /// </summary>
        ICardCollection Graveyard { get; }

        /// <summary>
        /// Get all Cards from the Player's Decks.
        /// </summary>
        IEnumerable<ICard> AllCards { get; }

        /// <summary>
        /// Convenience property to retrieve this player's characters, i.e.
        /// the player himself and all cards on his/her board.
        /// </summary>
        IEnumerable<ICharacter> Characters { get; }

        /// <summary>
        /// Draw a Card from the Deck and add it to the Hand.
        /// </summary>
        /// <param name="game"></param>
        void DrawCard(IGame game);

        /// <summary>
        /// Cast a MonsterCard from the Player's Hand to the Board.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="monsterCard"></param>
        void CastMonster(IGame game, IMonsterCard monsterCard);

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
