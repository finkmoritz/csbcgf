using System;
namespace csccgl
{
    public interface IMonsterCard : ICard, ICharacter, ITargetful
    {
        /// <summary>
        /// Attack the given target Character.
        /// </summary>
        /// <param name="targetCharacter"></param>
        void Attack(ICharacter targetCharacter);
    }
}
