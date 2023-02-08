using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class Action : IAction
    {
        [JsonProperty]
        protected bool isAborted;

        protected Action() { }

        public Action(bool isAborted = false)
        {
            this.isAborted = isAborted;
        }

        [JsonIgnore]
        public bool IsAborted
        {
            get => isAborted;
            set => isAborted = value;
        }

        public abstract void Execute(IGame game);
        public abstract bool IsExecutable(IGameState gameState);
    }

    public abstract class Action<T> : Action, IAction<T> where T : IGameState
    {
        protected Action() { }

        public Action(bool isAborted = false) : base(isAborted)
        {
        }

        public abstract bool IsExecutable(T gameState);

        public override bool IsExecutable(IGameState gameState)
        {
            if (gameState is T s)
            {
                return IsExecutable(s);
            }
            else
            {
                return false;
            }
        }

        public abstract void Execute(IGame<T> game);

        public override void Execute(IGame game)
        {
            if (game is IGame<T> g)
            {
                Execute(g);
            }
        }
    }
}
