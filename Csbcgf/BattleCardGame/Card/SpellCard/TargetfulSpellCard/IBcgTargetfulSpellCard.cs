using System.Collections.Generic;

namespace Csbcgf.BattleCardGame
{
    public interface IBcgTargetfulSpellCard : IBcgTargetful, IBcgSpellCard
    {
        /// <summary>
        /// Called when the spell card is cast.
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="target"></param>
        void Cast(IBcgGame game, IBcgCharacter target);
    }
}
