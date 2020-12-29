using System;
using System.Collections.Generic;
using csbcgf;


namespace csbcgftest
{
    public class RandomDamageSpellCard : TargetlessSpellCard
    {
        public RandomDamageSpellCard(uint damage)
            : base(new RandomDamageSpellCardComponent((int)damage, damage))
        {
        }

        public class RandomDamageSpellCardComponent : TargetlessSpellCardComponent
        {
            private readonly uint damage;

            public RandomDamageSpellCardComponent(int mana, uint damage) : base(mana)
            {
                this.damage = damage;
            }

            public override List<IAction> GetActions(IGame game)
            {
                List<ICharacter> targets = new List<ICharacter>();
                foreach (IPlayer player in game.Players)
                {
                    targets.Add(player);
                    targets.AddRange((IEnumerable<ICharacter>)player.Board.AllCards);
                }

                List<IAction> actions = new List<IAction>();
                Random random = new Random();
                for (int i = 0; i < damage; ++i)
                {
                    int randomIndex = random.Next(targets.Count);
                    actions.Add(new ModifyLifeStatAction(targets[randomIndex], -1));
                }
                return actions;
            }
        }
    }
}
