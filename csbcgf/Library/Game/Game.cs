using Newtonsoft.Json;

namespace csbcgf
{
    public class Game<T> : IGame<T> where T : IGameState
    {
        [JsonProperty]
        protected T state = default(T)!;

        [JsonProperty]
        protected bool isGameOver = false;

        [JsonProperty]
        protected bool executeReactions { get; set; }

        protected Game()
        {
        }

        public Game(T state, bool executeReactions = true, bool isGameOver = false)
        {
            this.state = state;
            this.executeReactions = executeReactions;
            this.isGameOver = isGameOver;
        }

        [JsonIgnore]
        public T State
        {
            get => state;
        }

        [JsonIgnore]
        IGameState IGame.State
        {
            get => state;
        }

        
        public List<IAction> Execute(IAction action, bool withReactions = true)
        {
            return ExecuteSimultaneously(new List<IAction> { action }, withReactions);
        }

        public List<IAction> ExecuteSimultaneously(List<IAction> actions, bool withReactions = true)
        {
            executeReactions = withReactions;
            List<IAction> executedActions = Execute(actions);
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

        protected List<IAction> Execute(List<IAction> actions)
        {
            if(!executeReactions)
            {
                return new List<IAction>();
            }
            
            List<IAction> executedActions = new List<IAction>(actions);

            CallActionsOrDiscard(executedActions, true,
                (IAction action) => {
                    if (action is GameOverEvent)
                    {
                        isGameOver = true;
                    }
                }
            );

            if(!isGameOver)
            {
                CallActionsOrDiscard(executedActions, true,
                    (IAction action) => {
                        foreach (IReaction reaction in State.AllReactions())
                        {
                            reaction.ReactBefore(this, action);
                        }
                    }
                );

                CallActionsOrDiscard(executedActions, false,
                    (IAction action) => action.Execute(this)
                );

                CallActionsOrDiscard(executedActions, false,
                    (IAction action) => {
                        foreach (IReaction reaction in State.AllReactions())
                        {
                            reaction.ReactAfter(this, action);
                        }
                    }
                );
            }
            
            return executedActions;
        }

        private void CallActionsOrDiscard(List<IAction> actions, bool checkIfExecutable, System.Action<IAction> func)
        {
            List<IAction> actionsToBeRemoved = new List<IAction>();
            foreach (IAction action in actions) 
            {
                if (!action.IsAborted && (!checkIfExecutable || action.IsExecutable(State)))
                {
                    func(action);
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
