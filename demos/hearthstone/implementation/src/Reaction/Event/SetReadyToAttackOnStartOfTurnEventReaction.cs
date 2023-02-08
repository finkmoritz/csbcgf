using csbcgf;

namespace hearthstone
{
    public class SetReadyToAttackOnStartOfTurnEventReaction : CardReaction<NextTurnAction>
    {
        protected SetReadyToAttackOnStartOfTurnEventReaction() { }

        public SetReadyToAttackOnStartOfTurnEventReaction(IMonsterCard monsterCard) : base(monsterCard) { }

        protected override void ReactAfterInternal(IGame game, NextTurnAction action)
        {
            HearthstoneMonsterCard monsterCard = (HearthstoneMonsterCard)parentCard;
            IPlayer? owner = monsterCard.Owner;
            bool isReadyToAttack = owner == game.GameState.ActivePlayer
                && game.GameState.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).Contains(monsterCard);

            game.Execute(new ModifyReadyToAttackAction(monsterCard, isReadyToAttack));
        }
    }
}
