using System.Collections.Generic;

namespace csbcgf
{
    public interface IReaction
    {
        /// <summary>
        /// React on a given Action with zero or more Actions.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="action"></param>
        /// <returns>Actions triggered by the given Action.</returns>
        List<IAction> ReactTo(IGame game, IAction action);
    }
}
