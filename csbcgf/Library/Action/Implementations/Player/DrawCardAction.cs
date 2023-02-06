﻿using Newtonsoft.Json;

namespace csbcgf
{
    public class DrawCardAction : Action
    {
        [JsonProperty]
        protected IPlayer player = null!;

        [JsonProperty]
        protected ICard? drawnCard;

        protected DrawCardAction() { }

        public DrawCardAction(IPlayer player, bool isAborted = false)
            : base(isAborted)
        {
            this.player = player;
        }

        [JsonIgnore]
        public IPlayer Player
        {
            get => player;
        }

        [JsonIgnore]
        public ICard? DrawnCard
        {
            get => drawnCard;
        }

        public override void Execute(IGame game)
        {
            drawnCard = player.GetCardCollection(CardCollectionKeys.Deck).Last;
            game.ActionQueue.ExecuteSequentially(new List<IAction> {
                new RemoveCardFromCardCollectionAction(player.GetCardCollection(CardCollectionKeys.Deck), drawnCard),
                new AddCardToCardCollectionAction(player.GetCardCollection(CardCollectionKeys.Hand), drawnCard)
            });
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !player.GetCardCollection(CardCollectionKeys.Deck).IsEmpty;
        }
    }
}
