using System;
using Csbcgf.Core;

namespace Csbcgf.BattleCardGame
{
    public interface IBcgPlayer : IPlayer, IBcgManaful, IBcgCharacter
    {
        /// <summary>
        /// Draw a Card from the Deck and add it to the Hand.
        /// </summary>
        /// <param name="game"></param>
        void DrawCard(IBcgGame game);

        /// <summary>
        /// Summon a MonsterCard from the Player's Hand to the Board.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="monsterCard"></param>
        void SummonMonster(IBcgGame game, IBcgMonsterCard monsterCard);

        /// <summary>
        /// Cast a SpellCard from the Player's Hand that needs no target.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spellCard"></param>
        void CastSpell(IBcgGame game, IBcgTargetlessSpellCard spellCard);

        /// <summary>
        /// Cast a SpellCard from the Player's Hand onto the specified
        /// target Character.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spellCard"></param>
        /// <param name="target"></param>
        void CastSpell(IBcgGame game, IBcgTargetfulSpellCard spellCard, IBcgCharacter target);
    }
}
