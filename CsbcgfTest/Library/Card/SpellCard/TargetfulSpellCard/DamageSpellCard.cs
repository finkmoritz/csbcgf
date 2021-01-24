using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.Coretest
{
    [Serializable]
    public class DamageSpellCard : TargetfulSpellCard
    {
        [JsonProperty]
        protected uint damage;

        public DamageSpellCard(uint damage)
            : this(damage, new List<ICardComponent>(), new List<IReaction>())
        {
            Components.Add(new DamageSpellCardComponent((int)damage, damage));
        }

        [JsonConstructor]
        protected DamageSpellCard(
            uint damage,
            List<ICardComponent> components,
            List<IReaction> reactions
            ) : base(components, reactions)
        {
            this.damage = damage;
        }

        public override object Clone()
        {
            List<ICardComponent> componentsClone = new List<ICardComponent>();
            Components.ForEach(c => componentsClone.Add((ICardComponent)c.Clone()));

            List<IReaction> reactionsClone = new List<IReaction>();
            Reactions.ForEach(r => reactionsClone.Add((IReaction)r.Clone()));

            return new DamageSpellCard(
                damage,
                componentsClone,
                reactionsClone
            );
        }

        [Serializable]
        public class DamageSpellCardComponent : TargetfulSpellCardComponent
        {
            [JsonProperty]
            private readonly uint damage;

            public DamageSpellCardComponent(int mana, uint damage)
                : this(damage, new BcgManaCostStat(mana, mana), new List<IReaction>())
            {
            }

            [JsonConstructor]
            public DamageSpellCardComponent(
                uint damage,
                BcgManaCostStat manaCostStat,
                List<IReaction> reactions
                ) : base(manaCostStat, reactions)
            {
                this.damage = damage;
            }

            public override void Cast(IGame game, ICharacter target)
            {
                game.Execute(new BcgModifyLifeStatAction(target, -(int)damage));
            }

            public override HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
            {
                HashSet<ICharacter> targets = new HashSet<ICharacter>();
                foreach (IPlayer player in gameState.Players)
                {
                    targets.Add(player);
                    player.Board.AllCards.ForEach(c => targets.Add((ICharacter)c));
                }
                return targets;
            }

            public override object Clone()
            {
                List<IReaction> reactionsClone = new List<IReaction>();
                foreach (IReaction reaction in Reactions)
                {
                    reactionsClone.Add((IReaction)reaction.Clone());
                }

                return new DamageSpellCardComponent(
                    damage,
                    (BcgManaCostStat)manaCostStat.Clone(),
                    reactionsClone
                );
            }
        }
    }
}
