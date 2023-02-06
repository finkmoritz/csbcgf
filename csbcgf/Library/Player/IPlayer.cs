namespace csbcgf
{
    public interface IPlayer : IManaful, ICharacter, IReactive
    {
        /// <summary>
        /// Get all Cards from the Player's Decks.
        /// </summary>
        IEnumerable<ICard> AllCards { get; }

        ICardCollection GetCardCollection(string key);

        void AddCardCollection(string key, ICardCollection cardCollection);

        bool RemoveCardCollection(string key);

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
        void SummonMonster(IGame game, IMonsterCard monsterCard);

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
