using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class TransferCardAction : Action
    {
        [JsonProperty]
        public ICard Card;

        [JsonProperty]
        public ICardCollection Source;

        [JsonProperty]
        public ICardCollection Destination;

        [JsonConstructor]
        public TransferCardAction(
                ICard card,
                ICardCollection source,
                ICardCollection destination,
                bool isAborted = false
            ) : base(isAborted)
        {
            Card = card;
            Source = source;
            Destination = destination;
        }

        public override object Clone()
        {
            return new TransferCardAction(
                (ICard)Card.Clone(),
                (ICardCollection)Source.Clone(),
                (ICardCollection)Destination.Clone()
            );
        }

        public override void Execute(IGame game)
        {
            game.Execute(new RemoveCardFromCardCollectionAction(Card, Source));
            game.Execute(new AddCardToCardCollectionAction(Card, Destination));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Card != null
                && Source != null
                && Destination != null;
        }
    }
}
