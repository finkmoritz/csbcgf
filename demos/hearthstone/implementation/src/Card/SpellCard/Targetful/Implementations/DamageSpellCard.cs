using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class DamageSpellCard : HearthstoneTargetfulSpellCard
    {
        [JsonProperty]
        protected uint damage;

        protected DamageSpellCard() { }

        public DamageSpellCard(uint damage)
            : base(true)
        {
            this.damage = damage;
            AddComponent(new DamageSpellCardComponent((int)damage, damage));
        }

        public class DamageSpellCardComponent : HearthstoneTargetfulSpellCardComponent
        {
            [JsonProperty]
            protected uint damage;

            protected DamageSpellCardComponent() { }

            public DamageSpellCardComponent(int mana, uint damage)
                : base(mana)
            {
                this.damage = damage;
            }

            public override void Cast(HearthstoneGame game, IStatContainer target)
            {
                game.Execute(new ModifyLifeStatAction<HearthstoneGameState>(target, -(int)damage));
            }

            public override HashSet<IStatContainer> GetPotentialTargets(HearthstoneGameState gameState)
            {
                HashSet<IStatContainer> targets = new HashSet<IStatContainer>();
                foreach (HearthstonePlayer player in gameState.Players)
                {
                    targets.Add(player);
                    foreach (ICard card in player.GetCardCollection(CardCollectionKeys.Board).Cards)
                    {
                        targets.Add((IStatContainer)card);
                    }
                }
                return targets;
            }
        }
    }
}
