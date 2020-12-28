using System;
using System.Collections.Generic;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public abstract class SpellCard : Card, ISpellCard
    {
        /// <summary>
        /// Represents a certain type of Card that has an
        /// immediate effect on the Game's state.
        /// </summary>
        public SpellCard() : base()
        {
        }

        /// <summary>
        /// Represents a certain type of Card that has an
        /// immediate effect on the Game's state.
        /// </summary>
        /// <param name="components"></param>
        public SpellCard(List<ISpellCardComponent> components)
            : base(components.ConvertAll(c => (ICardComponent)c))
        {
        }
    }
}
