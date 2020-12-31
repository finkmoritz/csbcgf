using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ActionQueue : IActionQueue
    {
        public bool ExecuteReactions { get; set; }

        protected Queue<IAction> actions = new Queue<IAction>();

        [JsonProperty]
        protected bool isProcessing = false;

        [JsonProperty]
        protected bool isGameOver = false;


        public ActionQueue(bool executeReactions = true)
        {
            ExecuteReactions = executeReactions;
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

        public void Enqueue(IAction action)
        {
            if(!isGameOver)
            {
                actions.Enqueue(action);

                if(action is EndOfGameEvent)
                {
                    isGameOver = true;
                }
            }
        }

        public void Enqueue(List<IAction> actionList)
        {
            actionList.ForEach(a => Enqueue(a));
        }
    }
}
