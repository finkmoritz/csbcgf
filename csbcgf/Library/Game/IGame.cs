namespace csbcgf
{
    public interface IGame : IGameState, IReactive
    {
        /// <summary>
        /// Get the IGame's ActionQueue.
        /// </summary>
        IActionQueue ActionQueue { get; }
    }
}
