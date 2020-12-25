using System;
namespace csccgl
{
    public interface ITargetlessSpellCard : ITargetless, ISpellCard
    {
        /// <summary>
        /// Play this Card to alter the Game's state.
        /// </summary>
        /// <param name="game"></param>
        void Play(Game game);
    }
}
