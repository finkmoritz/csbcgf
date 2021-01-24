using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class TargetfulSpellCard : SpellCard, ITargetfulSpellCard
    {
        public TargetfulSpellCard()
            : this(new List<IBcgSpellCardComponent>())
        {
        }

        public TargetfulSpellCard(IBcgSpellCardComponent component)
            : this(new List<IBcgSpellCardComponent> { component })
        {
        }

        public TargetfulSpellCard(List<IBcgSpellCardComponent> components)
            : this(components.ConvertAll(c => (ICardComponent)c), new List<IReaction>())
        {
        }

        [JsonConstructor]
        public TargetfulSpellCard(List<ICardComponent> components, List<IReaction> reactions)
            : base(components, reactions)
        {
        }

        public HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            //Compute the intersection of all potential targets
            HashSet<ICharacter> potentialTargets = null;
            foreach (ICardComponent component in Components.FindAll(c => c is ITargetful))
            {
                if (potentialTargets == null)
                {
                    potentialTargets = ((ITargetful)component).GetPotentialTargets(gameState);
                }
                else
                {
                    potentialTargets.IntersectWith(((ITargetful)component).GetPotentialTargets(gameState));
                }
            }
            return potentialTargets ?? new HashSet<ICharacter>();
        }

        public void Cast(IGame game, ICharacter target)
        {
            if (!GetPotentialTargets(game).Contains(target))
            {
                throw new CsbcgfException("Tried to play a TargetfulSpellCard " +
                    "on an invalid target character!");
            }

            foreach (ICardComponent component in Components)
            {
                if (component is IBcgTargetlessSpellCardComponent targetlessComponent)
                {
                    targetlessComponent.Cast(game);
                }
                else if (component is IBcgTargetfulSpellCardComponent targetfulComponent)
                {
                    targetfulComponent.Cast(game, target);
                }
            }
        }

        public override object Clone()
        {
            List<ICardComponent> componentsClone = new List<ICardComponent>();
            Components.ForEach(c => componentsClone.Add((ICardComponent)c.Clone()));

            List<IReaction> reactionsClone = new List<IReaction>();
            Reactions.ForEach(r => reactionsClone.Add((IReaction)r.Clone()));

            return new TargetfulSpellCard(
                componentsClone,
                reactionsClone
            );
        }
    }
}
