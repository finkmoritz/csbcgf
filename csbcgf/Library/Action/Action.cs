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
}
