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
        public SpellCard(List<ISpellCardComponent> components)
            : this(components, null)
        {
        }

        public SpellCard() : this(new List<ISpellCardComponent>())
        {
        }

        public SpellCard(
            List<ISpellCardComponent> components,
            IPlayer owner
            ) : this(components.ConvertAll(c => (ICardComponent)c), owner)
        {
        }

        [JsonConstructor]
        public SpellCard(
            List<ICardComponent> components,
            IPlayer owner
            ) : base(components, owner)
        {
        }
    }
}
