using System.Collections.Generic;
using csbcgf;

namespace csccgl
{
    public interface ITargetlessSpellCardComponent : ISpellCardComponent, ITargetless
    {
        /// <summary>
        /// Get all Actions to be performed by this component when the
        /// associated SpellCard is played.
        /// </summary>
        /// <param name="game"></param>
        /// <returns>All Actions to be performed by this component when the
        /// associated SpellCard is played.</returns>
        List<IAction> GetActions(IGame game);
    }
}
