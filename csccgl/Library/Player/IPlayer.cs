namespace csccgl
{
    public interface IPlayer : ICharacter
    {
        /// <summary>
        /// Draw a Card from the Deck and add it to the Hand.
        /// </summary>
        /// <returns>The drawn Card or null if no Card was drawn.</returns>
        ICard DrawCard();

        /// <summary>
        /// Play a MonsterCard from the Player's Hand to the Board at
        /// position boardIndex
        /// </summary>
        /// <param name="card"></param>
        void PlayCard(IMonsterCard monsterCard, int boardIndex);

        /// <summary>
        /// Play a SpellCard that needs no target.
        /// </summary>
        /// <param name="card"></param>
        void PlayCard(ITargetlessSpellCard spellCard);

        /// <summary>
        /// Play a SpellCard onto the specified target Character.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="target"></param>
        void PlayCard(ITargetfulSpellCard spellCard, ICharacter targetCharacter);

        /// <summary>
        /// Attack the target Character with the specified MonsterCard.
        /// </summary>
        /// <param name="monsterCard"></param>
        /// <param name="targetCharacter"></param>
        void Attack(IMonsterCard monsterCard, ICharacter targetCharacter);
    }
}
