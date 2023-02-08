using csbcgf;

namespace hearthstone
{
    public class SetReadyToAttackOnStartOfTurnEventReaction : CardReaction<NextTurnAction>
    {
        protected SetReadyToAttackOnStartOfTurnEventReaction() { }

        public SetReadyToAttackOnStartOfTurnEventReaction(IMonsterCard monsterCard) : base(monsterCard) { }

        public override void ReactAfter(IGame game, NextTurnAction action)
        {
            HearthstoneGameState state = (HearthstoneGameState)game.State;
            HearthstoneMonsterCard monsterCard = (HearthstoneMonsterCard)parentCard;
            IPlayer? owner = monsterCard.Owner;
            bool isReadyToAttack = owner == state.ActivePlayer
                && state.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).Contains(monsterCard);

            game.Execute(new ModifyReadyToAttackAction(monsterCard, isReadyToAttack));
        }
    }
}
