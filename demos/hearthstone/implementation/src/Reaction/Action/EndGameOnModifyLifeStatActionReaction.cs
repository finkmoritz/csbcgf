using csbcgf;

namespace hearthstone
{
    public class EndGameOnModifyLifeStatActionReaction : PlayerReaction<HearthstoneGameState, HearthstoneGame, ModifyLifeStatAction<HearthstoneGameState>>
    {
        protected EndGameOnModifyLifeStatActionReaction() { }

        public EndGameOnModifyLifeStatActionReaction(HearthstonePlayer parentPlayer)
            : base(parentPlayer) { }

        public override void ReactAfter(HearthstoneGame game, ModifyLifeStatAction<HearthstoneGameState> action)
        {
            if (ParentPlayer == action.Living && ParentPlayer.GetValue(StatKeys.Life) <= 0)
            {
                game.Execute(new GameOverEvent());
            }
        }
    }
}
