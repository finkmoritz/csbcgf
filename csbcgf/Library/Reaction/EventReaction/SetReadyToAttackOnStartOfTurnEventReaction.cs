namespace csbcgf
{
    public class SetReadyToAttackOnStartOfTurnEventReaction : CardReaction<StartOfTurnEvent>
    {
        protected SetReadyToAttackOnStartOfTurnEventReaction() { }

        public SetReadyToAttackOnStartOfTurnEventReaction(IMonsterCard monsterCard) : base(monsterCard) { }

        protected override void ReactAfterInternal(IGame game, StartOfTurnEvent action)
        {
            IMonsterCard monsterCard = (IMonsterCard)parentCard;
            IPlayer? owner = monsterCard.Owner;
            bool isReadyToAttack = owner == game.ActivePlayer
                && game.ActivePlayer.Board.Contains(monsterCard);

            game.Execute(new ModifyReadyToAttackAction(monsterCard, isReadyToAttack));
        }
    }
}
