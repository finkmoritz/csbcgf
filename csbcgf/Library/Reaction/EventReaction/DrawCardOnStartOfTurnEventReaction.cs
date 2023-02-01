namespace csbcgf
{
    public class DrawCardOnStartOfTurnEventReaction : Reaction<StartOfTurnEvent>
    {
        protected override void ReactAfterInternal(IGame game, StartOfTurnEvent action)
        {
            game.Execute(new DrawCardAction(game.ActivePlayer));
        }
    }
}
