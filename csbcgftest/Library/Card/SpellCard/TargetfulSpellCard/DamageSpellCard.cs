using csbcgf;
using Newtonsoft.Json;

namespace csbcgftest
{

    public class DamageSpellCard : TargetfulSpellCard
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

        public class DamageSpellCardComponent : TargetfulSpellCardComponent
        {
            [JsonProperty]
            protected uint damage;

            protected DamageSpellCardComponent() { }

            public DamageSpellCardComponent(int mana, uint damage)
                : base(mana)
            {
                this.damage = damage;
            }

            public override void Cast(IGame game, ICharacter target)
            {
                game.ActionQueue.Execute(new ModifyLifeStatAction(target, -(int)damage));
            }

            public override HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
            {
                HashSet<ICharacter> targets = new HashSet<ICharacter>();
                foreach (IPlayer player in gameState.Players)
                {
                    targets.Add(player);
                    foreach (ICard card in player.Board.Cards)
                    {
                        targets.Add((ICharacter)card);
                    }
                }
                return targets;
            }
        }
    }
}
