using csbcgf;

namespace hearthstone
{
    public class DieOnModifyLifeStatActionReaction : CardReaction<ModifyLifeStatAction>
    {
        protected DieOnModifyLifeStatActionReaction() { }

        public DieOnModifyLifeStatActionReaction(IHearthstoneMonsterCard parentCard)
            : base(parentCard) { }

        protected override void ReactAfterInternal(IGame game, ModifyLifeStatAction action)
        {
            if (ParentCard is IMonsterCard monsterCard && monsterCard.LifeValue <= 0)
            {
                game.ActionQueue.Execute(new DieAction(monsterCard));
            }
        }
    }
}
