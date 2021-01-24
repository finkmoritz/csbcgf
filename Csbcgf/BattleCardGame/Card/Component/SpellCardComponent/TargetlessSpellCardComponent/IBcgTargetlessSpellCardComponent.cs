using System.Collections.Generic;

namespace Csbcgf.BattleCardGame
{
    public interface IBcgTargetlessSpellCardComponent : IBcgSpellCardComponent, ITargetless
    {
        /// <summary>
        /// Called when spell card is cast. Execute Actions here.
        /// </summary>
        /// <param name="gameState"></param>
        void Cast(IGame game);
    }
}
