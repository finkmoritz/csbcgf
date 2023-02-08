using csbcgf;

namespace hearthstone
{
    public class EndGameOnModifyLifeStatActionReaction : PlayerReaction<ModifyLifeStatAction>
    {
        protected EndGameOnModifyLifeStatActionReaction() { }

        public EndGameOnModifyLifeStatActionReaction(IPlayer parentPlayer)
            : base(parentPlayer) { }

        public override void ReactAfter(IGame game, ModifyLifeStatAction action)
        {
            if (ParentPlayer is IPlayer && ParentPlayer.LifeValue <= 0)
            {
                game.Execute(new GameOverEvent());
            }
        }
    }
}
