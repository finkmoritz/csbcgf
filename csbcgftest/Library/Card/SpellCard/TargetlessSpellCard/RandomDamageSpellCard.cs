using System;
using System.Collections.Generic;
using csbcgf;
using Newtonsoft.Json;

namespace csbcgftest
{
    [Serializable]
    public class RandomDamageSpellCard : TargetlessSpellCard
    {
        public RandomDamageSpellCard(uint damage)
            : base(new RandomDamageSpellCardComponent((int)damage, damage))
        {
        }

        [JsonConstructor]
        protected RandomDamageSpellCard()
        {
        }

        [Serializable]
        public class RandomDamageSpellCardComponent : TargetlessSpellCardComponent
        {
            [JsonProperty]
            private readonly uint damage;

            public RandomDamageSpellCardComponent(int mana, uint damage) : base(mana)
            {
                this.damage = damage;
            }

            public override void Cast(IGame game)
            {
                List<ICharacter> targets = new List<ICharacter>();
                foreach (IPlayer player in game.Players)
                {
                    targets.Add(player);
                    targets.AddRange((IEnumerable<ICharacter>)player.Board.AllCards);
                }
                Random random = new Random();
                for (int i = 0; i < damage; ++i)
                {
                    int randomIndex = random.Next(targets.Count);
                    game.Execute(new ModifyLifeStatAction(targets[randomIndex], -1));
                }
            }
        }
    }
}
