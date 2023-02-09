using csbcgf;

namespace hearthstone
{
    public class EndGameOnModifyLifeStatActionReaction : PlayerReaction<HearthstoneGameState, HearthstoneGame, ModifyLifeStatAction<HearthstoneGameState>>
    {
        protected EndGameOnModifyLifeStatActionReaction() { }

        public EndGameOnModifyLifeStatActionReaction(IPlayer parentPlayer)
            : base(parentPlayer) { }

        public override void ReactAfter(HearthstoneGame game, ModifyLifeStatAction<HearthstoneGameState> action)
        {
            if (ParentPlayer is IPlayer && ParentPlayer.LifeValue <= 0)
            {
                game.Execute(new GameOverEvent());
            }
        }
    }
}
