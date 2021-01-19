using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class SpellCard : Card, ISpellCard
    {
        public SpellCard()
            : this(new List<ISpellCardComponent>())
        {
        }

        /// <summary>
        /// Represents a certain type of Card that has an
        /// immediate effect on the Game's state.
        /// </summary>
        /// <param name="components"></param>
        public SpellCard(List<ISpellCardComponent> components)
            : this(components.ConvertAll(c => (ICardComponent)c), new List<IReaction>())
        {
        }

        [JsonConstructor]
        public SpellCard(List<ICardComponent> components, List<IReaction> reactions)
            : base(components, reactions)
        {
        }
    }
}
