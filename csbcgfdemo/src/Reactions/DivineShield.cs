using System;
using csbcgf;
using Newtonsoft.Json;

namespace csbcgfdemo
{
    /// <summary>
    /// Divine Shield blocks first damage.
    /// </summary>
    [Serializable]
    public class DivineShield : IReaction
    {
        public object Clone()
        {
            return new DivineShield();
        }

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsBefore(typeof(ModifyLifeStatAction)))
            {
                ModifyLifeStatAction a = (ModifyLifeStatAction)actionEvent.Action;
                ICard parentCard = FindParentCard(game);

                if (a.Living == parentCard && a.Delta < 0)
                {
                    a.Delta = 0;
                    parentCard.RemoveReaction(this);
                }
            }
        }

        private ICard FindParentCard(IGameState gameState)
        {
            foreach (ICard card in gameState.AllCards)
            {
                if (card.Reactions.Contains(this))
                {
                    return card;
                }
            }
            return null;
        }
    }
}
