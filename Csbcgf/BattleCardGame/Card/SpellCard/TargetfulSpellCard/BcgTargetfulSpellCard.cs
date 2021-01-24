using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgTargetfulSpellCard : BcgSpellCard, IBcgTargetfulSpellCard
    {
        public BcgTargetfulSpellCard()
            : this(new List<IBcgSpellCardComponent>())
        {
        }

        public BcgTargetfulSpellCard(IBcgSpellCardComponent component)
            : this(new List<IBcgSpellCardComponent> { component })
        {
        }

        public BcgTargetfulSpellCard(List<IBcgSpellCardComponent> components)
            : this(components.ConvertAll(c => (ICardComponent)c), new List<IReaction>())
        {
        }

        [JsonConstructor]
        public BcgTargetfulSpellCard(List<ICardComponent> components, List<IReaction> reactions)
            : base(components, reactions)
        {
        }

        public HashSet<IBcgCharacter> GetPotentialTargets(IBcgGameState gameState)
        {
            //Compute the intersection of all potential targets
            HashSet<IBcgCharacter> potentialTargets = null;
            foreach (ICardComponent component in Components.FindAll(c => c is IBcgTargetful))
            {
                if (potentialTargets == null)
                {
                    potentialTargets = ((IBcgTargetful)component).GetPotentialTargets(gameState);
                }
                else
                {
                    potentialTargets.IntersectWith(((IBcgTargetful)component).GetPotentialTargets(gameState));
                }
            }
            return potentialTargets ?? new HashSet<IBcgCharacter>();
        }

        public void Cast(IBcgGame game, IBcgCharacter target)
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

            return new BcgTargetfulSpellCard(
                componentsClone,
                reactionsClone
            );
        }
    }
}
