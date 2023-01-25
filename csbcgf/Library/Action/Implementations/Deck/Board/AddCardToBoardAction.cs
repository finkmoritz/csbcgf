using Newtonsoft.Json;

namespace csbcgf
{
    public class AddCardToBoardAction : Action
    {
        [JsonProperty]
        protected IBoard board = null!;

        [JsonProperty]
        protected ICard card = null!;

        [JsonProperty]
        protected int boardIndex;

        protected AddCardToBoardAction() {}

        public AddCardToBoardAction(IBoard board, ICard card, int boardIndex, bool isAborted = false)
            : base(isAborted)
        {
            this.board = board;
            this.card = card;
            this.boardIndex = boardIndex;
        }

        [JsonIgnore]
        public IBoard Board {
            get => board;
        }

        [JsonIgnore]
        public ICard Card {
            get => card;
        }

        [JsonIgnore]
        public int BoardIndex {
            get => boardIndex;
        }

        public override void Execute(IGame game)
        {
            Board.AddAt(BoardIndex, Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Card != null
                && Board.IsFreeSlot(BoardIndex);
        }
    }
}
