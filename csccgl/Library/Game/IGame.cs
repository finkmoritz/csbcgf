using System;

namespace csccgl
{
    public interface IGame
    {
        /// <summary>
        /// Convenience method to retrieve the active Player.
        /// Equivalent to using
        /// <code>Players[ActivePlayerIndex]</code>
        /// </summary>
        Player ActivePlayer { get; }

        /// <summary>
        /// Array of Players involved in the Game.
        /// </summary>
        Player[] Players { get; }

        /// <summary>
        /// The active Player's MonsterCard attacks another Player's Character.
        /// </summary>
        /// <param name="monsterCard"></param>
        /// <param name="targetCharacter"></param>
        void Attack(IMonsterCard monsterCard, ICharacter targetCharacter);

        /// <summary>
        /// End the current turn.
        /// </summary>
        void EndTurn();

        /// <summary>
        /// Play a MonsterCard from the active Player's Hand to the Board at
        /// position boardIndex.
        /// </summary>
        /// <param name="monsterCard"></param>
        /// <param name="boardIndex"></param>
        void PlayMonster(IMonsterCard monsterCard, int boardIndex);

        /// <summary>
        /// Play a SpellCard from the active Player's Hand that needs no target.
        /// </summary>
        /// <param name="spellCard"></param>
        void PlaySpell(ITargetlessSpellCard spellCard);

        /// <summary>
        /// Play a SpellCard from the active Player's Hand onto the specified
        /// target Character.
        /// </summary>
        /// <param name="spellCard"></param>
        /// <param name="targetCharacter"></param>
        void PlaySpell(ITargetfulSpellCard spellCard, ICharacter targetCharacter);
    }
}
