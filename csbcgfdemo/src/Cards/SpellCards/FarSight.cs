using csbcgf;

namespace csbcgfdemo
{
    public class FarSight : TargetlessSpellCard
    {
        protected FarSight() {}
        
        public FarSight(bool initialize = true) : base(new FarSightComponent())
        {
        }

        public class FarSightComponent : TargetlessSpellCardComponent
        {
            protected FarSightComponent() {}

            public FarSightComponent(bool initialize = true) : base(3)
            {
            }

            public override void Cast(IGame game)
            {
                game.Execute(new FarSightAction());
            }

            public class FarSightAction : csbcgf.Action
            {
                public override void Execute(IGame game)
                {
                    DrawCardAction drawCardAction = new DrawCardAction(game.ActivePlayer);
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
