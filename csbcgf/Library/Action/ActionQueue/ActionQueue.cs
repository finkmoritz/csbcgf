using Newtonsoft.Json;

namespace csbcgf
{
    public class ActionQueue : IActionQueue
    {
        [JsonProperty]
        protected bool isGameOver = false;

        public bool ExecuteReactions { get; set; }

        protected ActionQueue() { }

        public ActionQueue(bool executeReactions = true)
            : this(executeReactions, false)
        {
        }

        protected ActionQueue(bool executeReactions, bool isGameOver)
        {
            ExecuteReactions = executeReactions;
            this.isGameOver = isGameOver;
        }

        public void Execute(IGame game, IAction action)
        {
            if (ExecuteReactions && !isGameOver && !action.IsAborted && action.IsExecutable(game))
            {
                foreach(IReaction reaction in game.AllReactions())
                {
                    reaction.ReactBefore(game, action);
                }

                if (!action.IsAborted)
                {
                    action.Execute(game);

                    foreach(IReaction reaction in game.AllReactions())
                    {
                        reaction.ReactAfter(game, action);
                    }
                }
            }

            if (action is EndOfGameEvent)
            {
                isGameOver = true;
            }
        }
    }
}
