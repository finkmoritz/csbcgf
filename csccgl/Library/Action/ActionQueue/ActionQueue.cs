using System;
using System.Collections.Generic;

namespace csbcgf
{
    public class ActionQueue : IActionQueue
    {
        protected Queue<IAction> actions = new Queue<IAction>();

        public ActionQueue()
        {
        }

        public void Process(IGame game)
        {
            while(actions.Count > 0)
            {
                IAction action = actions.Dequeue();
                if(action.IsExecutable(game))
                {
                    action.Execute(game);
                    game.AllCards.ForEach(c => QueueAll(c.ReactTo(action)));
                }
            }
        }

        public void Queue(IAction action)
        {
            actions.Enqueue(action);
        }

        protected void QueueAll(List<IAction> actionList)
        {
            actionList.ForEach(a => actions.Enqueue(a));
        }
    }
}
