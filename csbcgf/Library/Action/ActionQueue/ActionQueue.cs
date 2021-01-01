using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ActionQueue : IActionQueue
    {
        [JsonProperty]
        protected readonly Queue<IAction> actions = new Queue<IAction>();

        [JsonProperty]
        protected bool isProcessing = false;

        [JsonProperty]
        protected bool isGameOver = false;

        public bool ExecuteReactions { get; set; }

        public ActionQueue(bool executeReactions = true)
            : this(executeReactions, false, false)
        {
        }

        [JsonConstructor]
        protected ActionQueue(bool executeReactions, bool isProcessing, bool isGameOver)
        {
            this.ExecuteReactions = executeReactions;
            this.isProcessing = isProcessing;
            this.isGameOver = isGameOver;
        }

        public void Enqueue(IAction action)
        {
            if (!isGameOver)
            {
                actions.Enqueue(action);

                if (action is EndOfGameEvent)
                {
                    isGameOver = true;
                }
            }
        }

        public void Enqueue(List<IAction> actionList)
        {
            actionList.ForEach(a => Enqueue(a));
        }

        public void Process(IGame game)
        {
            if(!isProcessing && !isGameOver)
            {
                try
                {
                    isProcessing = true;
                    while (actions.Count > 0)
                    {
                        IAction action = actions.Dequeue();
                        if (action.IsExecutable(game))
                        {
                            if (ExecuteReactions)
                            {
                                game.AllCards.ForEach(c => Enqueue(c.ReactTo(game, action)));
                            }
                            action.Execute(game);
                        }
                    }
                }
                finally
                {
                    isProcessing = false;
                }
            }
        }
    }
}
