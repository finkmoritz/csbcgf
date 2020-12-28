using System;
using System.Collections.Generic;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public abstract class TargetfulSpellCard : SpellCard, ITargetfulSpellCard
    {
        public TargetfulSpellCard(List<ISpellCardComponent> components)
            : base(components)
        {
        }

        public TargetfulSpellCard() : base()
        {
        }

        public HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            //Compute the intersection of all potential targets
            HashSet<ICharacter> potentialTargets = null;
            foreach (ICardComponent component in Components.FindAll(c => c is ITargetful))
            {
                if (potentialTargets == null)
                {
                    potentialTargets = ((ITargetful)component).GetPotentialTargets(game);
                }
                else
                {
                    HashSet<ICharacter> potTargets = ((ITargetful)component).GetPotentialTargets(game);
                    potentialTargets.RemoveWhere(t => !potTargets.Contains(t));
                }
            }
            return potentialTargets;
        }

        public abstract void Play(IGame game, ICharacter targetCharacter);
    }
}
