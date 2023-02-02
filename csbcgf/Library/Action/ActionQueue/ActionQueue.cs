using Newtonsoft.Json;

namespace csbcgf
{
    public class ActionQueue : IActionQueue
    {
        [JsonProperty]
        protected IGame game = null!;

        [JsonProperty]
        protected bool isGameOver = false;

        [JsonProperty]
        protected bool executeReactions { get; set; }

        protected ActionQueue() { }

        public ActionQueue(IGame game, bool executeReactions = true, bool isGameOver = false)
        {
            this.game = game;
            this.executeReactions = executeReactions;
            this.isGameOver = isGameOver;
        }

        public List<IAction> Execute(IAction action, bool withReactions = true)
        {
            return ExecuteSimultaneously(new List<IAction> { action }, withReactions);
        }

        public List<IAction> ExecuteSimultaneously(List<IAction> actions, bool withReactions = true)
        {
            executeReactions = withReactions;
            List<IAction> executedActions = Execute(game, actions);
            executeReactions = true;
            return executedActions;
        }

        public List<IAction> ExecuteSequentially(List<IAction> actions, bool withReactions = true)
        {
            List<IAction> executedActions = new List<IAction>();
            foreach(IAction action in actions)
            {
                IAction? executedAction = Execute(action, withReactions).FirstOrDefault(defaultValue: null);
                if(executedAction != null)
                {
                    executedActions.Add(executedAction);
                }
                else
                {
                    break;
                }
            }
            return executedActions;
        }

        protected List<IAction> Execute(IGame game, List<IAction> actions)
        {
            if(!executeReactions)
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
