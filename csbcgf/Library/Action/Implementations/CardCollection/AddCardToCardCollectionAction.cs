using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToCardCollectionAction : Action
    {
        [JsonProperty]
        public ICard Card;

        [JsonProperty]
        public ICardCollection CardCollection;

        [JsonConstructor]
        public AddCardToCardCollectionAction(
            ICard card,
            ICardCollection cardCollection,
            bool isAborted = false)
            : base(isAborted)
        {
            Card = card;
            CardCollection = cardCollection;
        }

        public override object Clone()
        {
            return new AddCardToCardCollectionAction(
                (ICard)Card.Clone(),
                (ICardCollection)CardCollection.Clone(),
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            CardCollection.Add(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Card != null
                && CardCollection != null;
        }
    }
}
