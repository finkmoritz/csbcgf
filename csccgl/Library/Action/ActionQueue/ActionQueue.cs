using System;
using System.Collections.Generic;

namespace csccgl
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
                }
            }
        }

        public void Queue(IAction action)
        {
            actions.Enqueue(action);
        }
    }
}
