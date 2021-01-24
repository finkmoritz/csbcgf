using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public abstract class SpellCard : Card, ISpellCard
    {
        public SpellCard()
            : this(new List<IBcgSpellCardComponent>())
        {
        }

        /// <summary>
        /// Represents a certain type of Card that has an
        /// immediate effect on the Game's state.
        /// </summary>
        /// <param name="components"></param>
        public SpellCard(List<IBcgSpellCardComponent> components)
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
