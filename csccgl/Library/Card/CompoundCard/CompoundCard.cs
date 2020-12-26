using System;
using System.Collections.Generic;
using System.Linq;

namespace csccgl
{
    public abstract class CompoundCard : ICard
    {
        protected List<ICard> Components;
        public ManaStat ManaStat { get; }

        public CompoundCard(List<ICard> components)
        {
            if(components.Count == 0)
            {
                throw new CsccglException("Parameter components cannot be empty!");
            }
            this.Components = components;
            this.ManaStat = new ManaStat(Components.Sum(c => c.ManaStat.Value), 99);
        }

        public bool IsPlayable(IGame game)
        {
            return Components.TrueForAll(c => c.IsPlayable(game));
        }
    }
}
