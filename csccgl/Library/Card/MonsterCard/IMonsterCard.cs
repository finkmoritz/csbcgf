using System;
namespace csccgl
{
    public interface IMonsterCard : ICard, ICharacter, ITargetful
    {
        /// <summary>
        /// Attack the given target Character.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="targetCharacter"></param>
        void Attack(IGame game, ICharacter targetCharacter);
    }
}
