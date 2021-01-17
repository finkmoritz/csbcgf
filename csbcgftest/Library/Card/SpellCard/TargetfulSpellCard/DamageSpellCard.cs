using System;
using System.Collections.Generic;
using csbcgf;
using Newtonsoft.Json;

namespace csbcgftest
{
    [Serializable]
    public class DamageSpellCard : TargetfulSpellCard
    {
        [JsonProperty]
        protected uint damage;

        public DamageSpellCard(uint damage)
            : this(damage, new List<ISpellCardComponent>(), null)
        {
            AddComponent(new DamageSpellCardComponent((int)damage, damage));
        }

        [JsonConstructor]
        protected DamageSpellCard(uint damage, List<ISpellCardComponent> components, IPlayer owner)
            : base(components, owner)
        {
            this.damage = damage;
        }

        public override object Clone()
        {
            DamageSpellCard clone = new DamageSpellCard(
                damage,
                new List<ISpellCardComponent>(),
                null // otherwise circular dependency
            );
            foreach (ISpellCardComponent c in Components)
            {
                ISpellCardComponent cc = (ISpellCardComponent)c.Clone();
                cc.ParentCard = clone;
                clone.AddComponent(cc);
            }
            return clone;
        }

        [Serializable]
        public class DamageSpellCardComponent : TargetfulSpellCardComponent
        {
            [JsonProperty]
            private readonly uint damage;

            public DamageSpellCardComponent(int mana, uint damage)
                : this(damage, new ManaCostStat(mana, mana), new List<IReaction>(), null)
            {
            }

            [JsonConstructor]
            public DamageSpellCardComponent(
                uint damage,
                ManaCostStat manaCostStat,
                List<IReaction> reactions,
                ICard parentCard
                ) : base(manaCostStat, reactions, parentCard)
            {
                this.damage = damage;
            }

            public override void Cast(IGame game, ICharacter target)
            {
                game.Execute(new ModifyLifeStatAction(target, -(int)damage));
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
                    (ManaCostStat)manaCostStat.Clone(),
                    reactionsClone,
                    null // otherwise circular dependency
                );
            }
        }
    }
}
