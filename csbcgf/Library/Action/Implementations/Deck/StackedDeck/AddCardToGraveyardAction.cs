using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToGraveyardAction : IAction
    {
        [JsonProperty]
        protected readonly IDeck graveyard;

        [JsonProperty]
        protected readonly ICard card;

        [JsonConstructor]
        public AddCardToGraveyardAction(IDeck graveyard, ICard card)
        {
            this.graveyard = graveyard;
            this.card = card;
        }

        public void Execute(IGame game)
        {
            graveyard.Push(card);
        }

        public bool IsExecutable(IGame game)
        {
            return card != null && !graveyard.Contains(card);
        }
    }
}
