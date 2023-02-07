using csbcgf;

namespace hearthstone
{
    public class SetReadyToAttackOnStartOfTurnEventReaction : CardReaction<NextTurnAction>
    {
        protected SetReadyToAttackOnStartOfTurnEventReaction() { }

        public SetReadyToAttackOnStartOfTurnEventReaction(IMonsterCard monsterCard) : base(monsterCard) { }

        protected override void ReactAfterInternal(IGame game, NextTurnAction action)
        {
            IHearthstoneMonsterCard monsterCard = (IHearthstoneMonsterCard)parentCard;
            IPlayer? owner = monsterCard.Owner;
            bool isReadyToAttack = owner == game.ActivePlayer
                && game.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).Contains(monsterCard);

            game.ActionQueue.Execute(new ModifyReadyToAttackAction(monsterCard, isReadyToAttack));
        }
    }
}
