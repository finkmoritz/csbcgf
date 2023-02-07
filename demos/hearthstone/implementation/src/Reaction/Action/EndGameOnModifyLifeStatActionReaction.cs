using csbcgf;

namespace hearthstone
{
    public class EndGameOnModifyLifeStatActionReaction : PlayerReaction<ModifyLifeStatAction>
    {
        protected EndGameOnModifyLifeStatActionReaction() { }

        public EndGameOnModifyLifeStatActionReaction(IPlayer parentPlayer)
            : base(parentPlayer) { }

        protected override void ReactAfterInternal(IGame game, ModifyLifeStatAction action)
        {
            if (ParentPlayer is IPlayer && ParentPlayer.LifeValue <= 0)
            {
                game.ActionQueue.Execute(new EndOfGameEvent());
            }
        }
    }
}
