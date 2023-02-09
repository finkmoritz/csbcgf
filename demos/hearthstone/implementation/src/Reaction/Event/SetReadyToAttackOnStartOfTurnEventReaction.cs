using csbcgf;

namespace hearthstone
{
    public class SetReadyToAttackOnStartOfTurnEventReaction : CardReaction<HearthstoneGameState, HearthstoneGame, NextTurnAction>
    {
        protected SetReadyToAttackOnStartOfTurnEventReaction() { }

        public SetReadyToAttackOnStartOfTurnEventReaction(HearthstoneMonsterCard monsterCard) : base(monsterCard) { }

        public override void ReactAfter(HearthstoneGame game, NextTurnAction action)
        {
            HearthstoneMonsterCard monsterCard = (HearthstoneMonsterCard)parentCard;
            IPlayer? owner = monsterCard.Owner;
            bool isReadyToAttack = owner == game.State.ActivePlayer
                && game.State.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).Contains(monsterCard);

            game.Execute(new ModifyReadyToAttackAction(monsterCard, isReadyToAttack));
        }
    }
}
