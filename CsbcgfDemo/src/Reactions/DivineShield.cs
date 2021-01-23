using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.Coredemo
{
    /// <summary>
    /// Divine Shield blocks first damage.
    /// </summary>
    [Serializable]
    public class DivineShield : Reaction
    {
        public override object Clone()
        {
            return new DivineShield();
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsBefore(typeof(ModifyLifeStatAction)))
            {
                ModifyLifeStatAction a = (ModifyLifeStatAction)actionEvent.Action;
                ICard parentCard = FindParentCard(game);

                if (a.Living == parentCard && a.Delta < 0)
                {
                    a.Delta = 0;
                    parentCard.Reactions.Remove(this);
                }
            }
        }
    }
}
