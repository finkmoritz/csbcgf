namespace csbcgf
{
    public class ModifyManaOnStartOfTurnEventReaction : Reaction<StartOfTurnEvent>
    {
        protected override void ReactAfterInternal(IGame game, StartOfTurnEvent action)
        {
            int manaDelta = game.ActivePlayer.ManaBaseValue + 1 - game.ActivePlayer.ManaValue;
            game.Execute(new ModifyManaStatAction(game.ActivePlayer, manaDelta, 1));
        }
    }
}
