using System.Collections.Generic;

namespace Csbcgf.Core
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
