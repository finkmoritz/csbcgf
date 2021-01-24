using System.Collections.Generic;

namespace Csbcgf.BattleCardGame
{
    public interface IBcgTargetfulSpellCardComponent : IBcgSpellCardComponent, IBcgTargetful
    {
        /// <summary>
        /// Called when the spell card is cast. Execute Actions here.
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="target"></param>
        void Cast(IBcgGame game, IBcgCharacter target);
    }
}
