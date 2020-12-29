using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public class ActionQueue : IActionQueue
    {
        protected Queue<IAction> actions = new Queue<IAction>();

        protected bool isProcessing = false;

        public ActionQueue()
        {
        }

        public void Process(IGame game)
        {
            if(!isProcessing)
            {
                try
                {
                    isProcessing = true;
                    while (actions.Count > 0)
                    {
                        IAction action = actions.Dequeue();
                        if (action.IsExecutable(game))
                        {
                            action.Execute(game);
                            game.AllCards.ForEach(c => Queue(c.ReactTo(game, action)));
                        }
                    }
                } finally
                {
                    isProcessing = false;
                }
            }
        }

        public void Queue(IAction action)
        {
            actions.Enqueue(action);
        }

        public void Queue(List<IAction> actionList)
        {
            actionList.ForEach(a => actions.Enqueue(a));
        }
    }
}
