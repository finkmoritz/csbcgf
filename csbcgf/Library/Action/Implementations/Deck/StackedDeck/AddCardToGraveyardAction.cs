using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToGraveyardAction : IAction
    {
        [JsonProperty]
        public readonly IDeck Graveyard;

        [JsonProperty]
        public ICard Card;

        [JsonConstructor]
        public AddCardToGraveyardAction(IDeck graveyard, ICard card)
        {
            Graveyard = graveyard;
            Card = card;
        }

        public void Execute(IGame game)
        {
            Graveyard.Push(Card);
        }

        public bool IsExecutable(IGameState gameState)
        {
            return Card != null && !Graveyard.Contains(Card);
        }
    }
}
