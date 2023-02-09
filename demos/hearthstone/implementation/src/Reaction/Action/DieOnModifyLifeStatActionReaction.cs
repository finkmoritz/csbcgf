using csbcgf;

namespace hearthstone
{
    public class DieOnModifyLifeStatActionReaction : CardReaction<ModifyLifeStatAction>
    {
        protected DieOnModifyLifeStatActionReaction() { }

        public DieOnModifyLifeStatActionReaction(HearthstoneMonsterCard parentCard)
            : base(parentCard) { }

        public override void ReactAfter(IGame game, ModifyLifeStatAction action)
        {
            if (ParentCard is HearthstoneMonsterCard monsterCard && monsterCard.LifeValue <= 0)
            {
                game.Execute(new DieAction(monsterCard));
            }
        }
    }
}
