using csbcgf;

namespace csbcgfdemo
{
    public class FarSight : TargetlessSpellCard
    {
        public FarSight() : base(new FarSightComponent())
        {
        }

        public class FarSightComponent : TargetlessSpellCardComponent
        {
            public FarSightComponent() : base(3)
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
