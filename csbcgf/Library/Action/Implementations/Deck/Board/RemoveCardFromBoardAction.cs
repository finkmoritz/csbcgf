using Newtonsoft.Json;

namespace csbcgf
{
    public class RemoveCardFromBoardAction : Action
    {
        [JsonProperty]
        protected IBoard board = null!;

        [JsonProperty]
        protected ICard card = null!;

        protected RemoveCardFromBoardAction() {}

        public RemoveCardFromBoardAction(IBoard board, ICard card, bool isAborted = false)
            : base(isAborted)
        {
            this.board = board;
            this.card = card;
        }

        [JsonIgnore]
        public IBoard Board {
            get => board;
        }

        [JsonIgnore]
        public ICard Card {
            get => card;
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
