using csbcgf;

namespace hearthstone
{
    public class FarSight : HearthstoneTargetlessSpellCard
    {
        protected FarSight() { }

        public FarSight(bool _ = true) : base(_)
        {
            AddComponent(new FarSightComponent());
        }

        public class FarSightComponent : TargetlessSpellCardComponent<HearthstoneGameState>
        {
            protected FarSightComponent() { }

            public FarSightComponent(bool _ = true) : base(3)
            {
            }

            public override void Cast(IGame<HearthstoneGameState> game)
            {
                game.Execute(new FarSightAction());
            }

            public class FarSightAction : csbcgf.Action
            {
                public override void Execute(IGame game)
                {
                    HearthstoneGameState state = (HearthstoneGameState)game.State;
                    DrawCardAction drawCardAction = new DrawCardAction(state.ActivePlayer);
                    game.Execute(drawCardAction);
                    game.Execute(new ModifyManaStatAction(drawCardAction.DrawnCard!, -3, 0));
                }

                public override bool IsExecutable(IGameState gameState)
                {
                    return true;
                }
            }
        }
    }
}
