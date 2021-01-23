using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.Coredemo
{
    [Serializable]
    public class FarSight : TargetlessSpellCard
    {
        [JsonConstructor]
        public FarSight() : base(new FarSightComponent())
        {
        }

        [Serializable]
        public class FarSightComponent : TargetlessSpellCardComponent
        {
            [JsonConstructor]
            public FarSightComponent() : base(3)
            {
            }

            public override void Cast(IGame game)
            {
                game.Execute(new FarSightAction());
            }

            [Serializable]
            public class FarSightAction : csbcgf.Action
            {
                public override object Clone()
                {
                    return new FarSightAction();
                }

                public override void Execute(IGame game)
                {
                    DrawCardAction drawCardAction = new DrawCardAction(game.ActivePlayer);
                    game.Execute(drawCardAction);
                    game.Execute(new ModifyManaStatAction(drawCardAction.DrawnCard, -3, 0));
                }

                public override bool IsExecutable(IGameState gameState)
                {
                    return true;
                }
            }
        }
    }
}
