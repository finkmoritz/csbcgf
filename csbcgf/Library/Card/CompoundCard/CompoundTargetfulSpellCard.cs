using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public class CompoundTargetfulSpellCard : CompoundCard, ITargetfulSpellCard
    {
        public CompoundTargetfulSpellCard(List<ISpellCard> components)
            : base(new List<ICard>())
        {
            if(components.Find(c => c is ITargetfulSpellCard) == null)
            {
                throw new CsbcgfException("Tried to construct a CompoundTargetfulSpellCard " +
                    "without a component of type ITargetfulSpellCard.\n" +
                    "Use CompoundTargetlessSpellCard instead.");
            }
            components.ForEach(c => Components.Add(c));
        }

        public CompoundTargetfulSpellCard(ISpellCard spellCard)
            : this(new List<ISpellCard> { spellCard })
        {
        }

        public HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            //Compute the intersection of all potential targets
            HashSet<ICharacter> potentialTargets = null;
            foreach(ICard card in Components.FindAll(c => c is ITargetful)) //TODO: check if this actually works!
            {
                if (potentialTargets == null)
                {
                    potentialTargets = ((ITargetful)card).GetPotentialTargets(game);
                }
                else
                {
                    HashSet<ICharacter> potTargets = ((ITargetful)card).GetPotentialTargets(game);
                    potentialTargets.RemoveWhere(t => !potTargets.Contains(t));
                }
            }
            return potentialTargets;
        }

        public void Play(IGame game, ICharacter targetCharacter)
        {
            foreach (ICard card in Components)
            {
                if (card is ITargetfulSpellCard) //TODO: check if this actually works!
                {
                    ((ITargetfulSpellCard)card).Play(game, targetCharacter);
                } else
                {
                    ((ITargetlessSpellCard)card).Play(game);
                }
            }
        }
    }
}
