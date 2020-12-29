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
                            game.AllCards.ForEach(c => Enqueue(c.ReactTo(game, action)));
                        }
                    }
                } finally
                {
                    isProcessing = false;
                }
            }
        }

        public void Enqueue(IAction action)
        {
            actions.Enqueue(action);
        }

        public void Enqueue(List<IAction> actionList)
        {
            actionList.ForEach(a => actions.Enqueue(a));
        }
    }
}
