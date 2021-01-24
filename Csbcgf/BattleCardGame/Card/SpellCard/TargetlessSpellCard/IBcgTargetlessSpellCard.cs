using System.Collections.Generic;

namespace Csbcgf.BattleCardGame
{
    public interface ITargetlessSpellCard : ITargetless, ISpellCard
    {
        /// <summary>
        /// Called when the spell card is cast.
        /// </summary>
        /// <param name="gameState"></param>
        void Cast(IGame game);
    }
}
