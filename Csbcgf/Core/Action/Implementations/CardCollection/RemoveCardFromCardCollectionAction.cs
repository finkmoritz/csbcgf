using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class RemoveCardFromCardCollectionAction : Action
    {
        [JsonProperty]
        public ICard Card;

        [JsonProperty]
        public ICardCollection CardCollection;

        [JsonConstructor]
        public RemoveCardFromCardCollectionAction(
            ICard card,
            ICardCollection cardCollection,
            bool isAborted = false
            ) : base(isAborted)
        {
            Card = card;
            CardCollection = cardCollection;
        }

        public override object Clone()
        {
            return new RemoveCardFromCardCollectionAction(
                (ICard)Card.Clone(),
                (ICardCollection)CardCollection.Clone(),
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            CardCollection.Remove(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Card != null
                && CardCollection != null;
        }
    }
}
