namespace csbcgf
{
    public class SetReadyToAttackOnStartOfTurnEventReaction : CardReaction
    {
        protected SetReadyToAttackOnStartOfTurnEventReaction() {}

        public SetReadyToAttackOnStartOfTurnEventReaction(IMonsterCard monsterCard) : base(monsterCard) {}

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if(actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                IMonsterCard monsterCard = (IMonsterCard)parentCard;
                IPlayer? owner = monsterCard.Owner;
                bool isReadyToAttack = owner == game.ActivePlayer
                    && game.ActivePlayer.Board.Contains(monsterCard);

                game.Execute(new ModifyReadyToAttackAction(monsterCard, isReadyToAttack));
            }
        }
    }
}
