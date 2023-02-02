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

        public List<IAction> Execute(IGame game, List<IAction> actions)
        {
            if(!ExecuteReactions)
            {
                return new List<IAction>();
            }
            
            List<IAction> executedActions = new List<IAction>(actions);

            CallActionsOrDiscard(executedActions, game, true,
                (IAction action, IGame game) => {
                    if (action is EndOfGameEvent)
                    {
                        isGameOver = true;
                    }
                }
            );

            if(!isGameOver)
            {
                CallActionsOrDiscard(executedActions, game, true,
                    (IAction action, IGame game) => {
                        foreach (IReaction reaction in game.AllReactions())
                        {
                            reaction.ReactBefore(game, action);
                        }
                    }
                );

                CallActionsOrDiscard(executedActions, game, false,
                    (IAction action, IGame game) => action.Execute(game)
                );

                CallActionsOrDiscard(executedActions, game, false,
                    (IAction action, IGame game) => {
                        foreach (IReaction reaction in game.AllReactions())
                        {
                            reaction.ReactAfter(game, action);
                        }
                    }
                );
            }
            
            return executedActions;
        }

        private void CallActionsOrDiscard(List<IAction> actions, IGame game, bool checkIfExecutable, Action<IAction, IGame> func)
        {
            List<IAction> actionsToBeRemoved = new List<IAction>();
            foreach (IAction action in actions) 
            {
                if (!action.IsAborted && (!checkIfExecutable || action.IsExecutable(game)))
                {
                    func(action, game);
                }
                else
                {
                    actionsToBeRemoved.Add(action);
                }
            }
            actionsToBeRemoved.ForEach(action => actions.Remove(action));
        }
    }
}
