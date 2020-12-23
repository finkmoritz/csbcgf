using System;

namespace csccgl
{
    [Serializable]
    public class SpellCard : Card, ISpellCard
    {
        /// <summary>
        /// Represents a certain type of Card that has an
        /// immediate effect on the Game's state.
        /// </summary>
        /// <param name="mana"></param>
        public SpellCard(int mana) : base(mana)
        {
        }
    }
}
