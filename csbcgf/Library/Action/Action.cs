namespace csbcgf
{
    public abstract class Action : IAction
    {
        public bool IsAborted { get; set; }

        public Action(bool isAborted = false)
        {
            IsAborted = isAborted;
        }

        public abstract void Execute(IGame game);
        public abstract bool IsExecutable(IGameState gameState);
    }
}
