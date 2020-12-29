using System;
using System.Collections.Generic;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public class TargetlessSpellCard : SpellCard, ITargetlessSpellCard
    {
        public TargetlessSpellCard(List<ITargetlessSpellCardComponent> components)
            : base(components.ConvertAll(c => (ISpellCardComponent)c))
        {
        }

        public TargetlessSpellCard(ITargetlessSpellCardComponent component)
            : this(new List<ITargetlessSpellCardComponent> { component })
        {
        }

        public TargetlessSpellCard() : base()
        {
        }

        public void Play(IGame game)
        {
            List<IAction> actions = new List<IAction>();
            Components.ForEach(
                c => ((ITargetlessSpellCardComponent)c).GetActions(game).ForEach(
                    a => actions.Add(a)
                )
            );
            game.Execute(actions);
        }
    }
}
