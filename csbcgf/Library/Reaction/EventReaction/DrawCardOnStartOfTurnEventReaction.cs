using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class DrawCardOnStartOfTurnEventReaction : IReaction
    {
        [JsonConstructor]
        public DrawCardOnStartOfTurnEventReaction()
        {
        }

        public object Clone()
        {
            return new DrawCardOnStartOfTurnEventReaction();
        }

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                game.Execute(new DrawCardAction(game.ActivePlayer));
            }
        }
    }
}
