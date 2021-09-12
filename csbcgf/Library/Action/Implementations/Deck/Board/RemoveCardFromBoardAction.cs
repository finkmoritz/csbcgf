using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromBoardAction : Action
    {
        [JsonProperty]
        public readonly IBoard Board;

        [JsonProperty]
        public ICard Card;

        [JsonConstructor]
        public RemoveCardFromBoardAction(IBoard board, ICard card, bool isAborted = false)
            : base(isAborted)
        {
            Board = board;
            Card = card;
        }

        public override object Clone()
        {
            return new RemoveCardFromBoardAction(
                (IBoard)Board.Clone(),
                (ICard)Card.Clone(),
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            Board.Remove(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Board.Contains(Card);
        }
    }
}
