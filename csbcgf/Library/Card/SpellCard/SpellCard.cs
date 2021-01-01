using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class SpellCard : Card, ISpellCard
    {
        /// <summary>
        /// Represents a certain type of Card that has an
        /// immediate effect on the Game's state.
        /// </summary>
        /// <param name="components"></param>
        [JsonConstructor]
        public SpellCard(List<ISpellCardComponent> components)
            : base(components.ConvertAll(c => (ICardComponent)c))
        {
        }

        public SpellCard() : this(new List<ISpellCardComponent>())
        {
        }
    }
}
