using System.Collections.Generic;

namespace csbcgf
{
    public interface ITargetfulSpellCard : ITargetful, ISpellCard
    {
        /// <summary>
        /// Retrieve all Actions to be performed when this spell card is played.
        /// Targetful spell card components will make use of the specified
        /// target character.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="targetCharacter"></param>
        /// <returns>All Actions to be performed when this spell card is played.</returns>
        List<IAction> GetActions(IGame game, ICharacter targetCharacter);
    }
}
