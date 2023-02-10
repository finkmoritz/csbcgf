using csbcgf;

namespace hearthstone
{
    public class DieOnModifyLifeStatActionReaction : CardReaction<HearthstoneGameState, HearthstoneGame, ModifyLifeStatAction<HearthstoneGameState>>
    {
        protected DieOnModifyLifeStatActionReaction() { }

        public DieOnModifyLifeStatActionReaction(HearthstoneMonsterCard parentCard)
            : base(parentCard) { }

        public override void ReactAfter(HearthstoneGame game, ModifyLifeStatAction<HearthstoneGameState> action)
        {
            if (ParentCard is HearthstoneMonsterCard monsterCard && monsterCard.GetValue(StatKeys.Life) <= 0)
            {
                game.Execute(new DieAction(monsterCard));
            }
        }
    }
}
