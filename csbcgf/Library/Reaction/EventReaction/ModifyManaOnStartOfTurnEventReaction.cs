using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyManaOnStartOfTurnEventReaction : IReaction
    {
        [JsonConstructor]
        public ModifyManaOnStartOfTurnEventReaction()
        {
        }

        public object Clone()
        {
            return new ModifyManaOnStartOfTurnEventReaction();
        }

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                int manaDelta = game.ActivePlayer.ManaBaseValue + 1 - game.ActivePlayer.ManaValue;
                game.Execute(new ModifyManaStatAction(game.ActivePlayer, manaDelta, 1));
            }
        }
    }
}
