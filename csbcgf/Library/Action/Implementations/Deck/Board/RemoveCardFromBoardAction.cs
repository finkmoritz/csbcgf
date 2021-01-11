using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromBoardAction : IAction
    {
        [JsonProperty]
        public readonly IBoard Board;

        [JsonProperty]
        public ICard Card;

        [JsonConstructor]
        public RemoveCardFromBoardAction(IBoard board, ICard card)
        {
            Board = board;
            Card = card;
        }

        public void Execute(IGame game)
        {
            Board.Remove(Card);
        }

        public bool IsExecutable(IGame game)
        {
            return Board.Contains(Card);
        }
    }
}
