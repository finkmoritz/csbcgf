using System;
using System.Collections.Generic;
using csbcgf;


namespace csbcgftest
{
    public class DamageSpellCard : TargetfulSpellCard
    {
        public DamageSpellCard(uint damage)
            : base(new DamageSpellCardComponent((int)damage, damage))
        {
        }

        public class DamageSpellCardComponent : TargetfulSpellCardComponent
        {
            private readonly uint damage;

            public DamageSpellCardComponent(int mana, uint damage) : base(mana)
            {
                this.damage = damage;
            }

            public override List<IAction> GetActions(IGame game, ICharacter target)
            {
                return new List<IAction>
                {
                    new ModifyLifeStatAction(target, -(int)damage)
                };
            }

            public override HashSet<ICharacter> GetPotentialTargets(IGame game)
            {
                HashSet<ICharacter> targets = new HashSet<ICharacter>();
                foreach (IPlayer player in game.Players)
                {
                    targets.Add(player);
                    player.Board.AllCards.ForEach(c => targets.Add((ICharacter)c));
                }
                return targets;
            }
        }
    }
}
