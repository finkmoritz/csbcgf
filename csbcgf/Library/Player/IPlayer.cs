using System;
using System.Collections.Generic;


namespace csbcgf
{
    public interface IPlayer : IManaful, ICharacter, IReactive, ICloneable
    {
        /// <summary>
        /// Get CardCollections of this Player.
        /// </summary>
        Dictionary<string, ICardCollection> CardCollections { get; }

        /// <summary>
        /// Get all Cards from the Player's CardCollections.
        /// </summary>
        List<ICard> AllCards { get; }

        /// <summary>
        /// Draw a Card from the Deck and add it to the Hand.
        /// </summary>
        /// <param name="game"></param>
        //void DrawCard(IGame game); //TODO

        /// <summary>
        /// Cast a MonsterCard from the Player's Hand to the Board.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="monsterCard"></param>
        //void CastMonster(IGame game, IMonsterCard monsterCard); //TODO

        /// <summary>
        /// Cast a SpellCard from the Player's Hand that needs no target.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spellCard"></param>
        //void CastSpell(IGame game, ITargetlessSpellCard spellCard); //TODO

        /// <summary>
        /// Cast a SpellCard from the Player's Hand onto the specified
        /// target Character.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spellCard"></param>
        /// <param name="target"></param>
        //void CastSpell(IGame game, ITargetfulSpellCard spellCard, ICharacter target); //TODO
    }
}
