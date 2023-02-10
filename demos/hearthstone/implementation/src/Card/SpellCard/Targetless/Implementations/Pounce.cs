using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class Pounce : HearthstoneTargetlessSpellCard
    {
        protected Pounce() { }

        public Pounce(bool _ = true) : base(_)
        {
            AddComponent(new PounceComponent());
        }

        public class PounceComponent : HearthstoneTargetlessSpellCardComponent
        {
            protected PounceComponent() { }

            public PounceComponent(bool _ = true) : base(3)
            {
            }

            public override void Cast(HearthstoneGame game)
            {
                game.Execute(new PounceAction((HearthstonePlayer)this.ParentCard!.Owner!));
            }

            /// Give your hero +2 Attack this turn
            public class PounceAction : csbcgf.Action<HearthstoneGameState>
            {
                [JsonProperty]
                protected HearthstonePlayer player = null!;

                [JsonProperty]
                protected IStat? extraAttackStat;

                protected PounceAction() {}

                public PounceAction(HearthstonePlayer player)
                {
                    this.player = player;
                }

                public override void Execute(IGame<HearthstoneGameState> game)
                {
                    extraAttackStat = new Stat(2, 0);
                    player.AddStat(StatKeys.Attack, extraAttackStat);
                    player.AddReaction(new PounceReaction(player, extraAttackStat));
                }

                public override bool IsExecutable(HearthstoneGameState gameState)
                {
                    return true;
                }
            }

            /// Remove +2 Attack again at the end of the turn
            public class PounceReaction : Reaction<HearthstoneGameState, HearthstoneGame, NextTurnAction>
            {
                [JsonProperty]
                protected HearthstonePlayer player = null!;

                [JsonProperty]
                protected IStat extraAttackStat = null!;

                protected PounceReaction() {}

                public PounceReaction(HearthstonePlayer player, IStat extraAttackStat)
                {
                    this.player = player;
                    this.extraAttackStat = extraAttackStat;
                }

                public override void ReactBefore(HearthstoneGame game, NextTurnAction action)
                {
                    player.RemoveStat(StatKeys.Attack, extraAttackStat);
                    player.RemoveReaction(this);
                }
            }
        }
    }
}
