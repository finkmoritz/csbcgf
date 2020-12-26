namespace csccgl
{
    public interface IPlayer : ICharacter
    {
        ManaStat ManaStat { get; }

        /// <summary>
        /// Draw a Card from the Deck and add it to the Hand.
        /// </summary>
        /// <returns>The drawn Card or null if no Card was drawn.</returns>
        ICard DrawCard();

        /// <summary>
        /// Play a MonsterCard from the Player's Hand to the Board at
        /// position boardIndex.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="monsterCard"></param>
        /// <param name="boardIndex"></param>
        void PlayMonster(IGame game, IMonsterCard monsterCard, int boardIndex);

        /// <summary>
        /// Play a SpellCard from the Player's Hand that needs no target.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spellCard"></param>
        void PlaySpell(IGame game, ITargetlessSpellCard spellCard);

        /// <summary>
        /// Play a SpellCard from the Player's Hand onto the specified
        /// target Character.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spellCard"></param>
        /// <param name="targetCharacter"></param>
        void PlaySpell(IGame game, ITargetfulSpellCard spellCard, ICharacter targetCharacter);

        /// <summary>
        /// Attack the target Character with the specified MonsterCard.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="monsterCard"></param>
        /// <param name="targetCharacter"></param>
        void Attack(IGame game, IMonsterCard monsterCard, ICharacter targetCharacter);
    }
}
