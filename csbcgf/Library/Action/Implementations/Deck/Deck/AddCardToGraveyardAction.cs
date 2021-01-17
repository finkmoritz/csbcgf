using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToGraveyardAction : Action
    {
        [JsonProperty]
        public readonly IDeck Graveyard;

        [JsonProperty]
        public ICard Card;

        [JsonConstructor]
        public AddCardToGraveyardAction(IDeck graveyard, ICard card, bool isAborted = false)
            : base(isAborted)
        {
            Graveyard = graveyard;
            Card = card;
        }

        public override object Clone()
        {
            return new AddCardToGraveyardAction(
                (IDeck)Graveyard.Clone(),
                (ICard)Card.Clone(),
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            Graveyard.Push(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Card != null && !Graveyard.Contains(Card);
        }
    }
}
