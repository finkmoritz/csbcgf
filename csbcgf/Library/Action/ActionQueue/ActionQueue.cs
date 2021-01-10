using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ActionQueue : IActionQueue
    {
        [JsonProperty]
        protected bool isGameOver = false;

        public bool ExecuteReactions { get; set; }

        public ActionQueue(bool executeReactions = true)
            : this(executeReactions, false)
        {
        }

        [JsonConstructor]
        protected ActionQueue(bool executeReactions, bool isGameOver)
        {
            ExecuteReactions = executeReactions;
            this.isGameOver = isGameOver;
        }

        public void Process(IGame game, IAction action)
        {
            if (!isGameOver && action.IsExecutable(game))
            {
                ProcessReactions(game, new BeforeActionEvent(action));
                action.Execute(game);
                ProcessReactions(game, new AfterActionEvent(action));
            }

            if (action is EndOfGameEvent)
            {
                isGameOver = true;
            }
        }

        private void ProcessReactions(IGame game, IActionEvent actionEvent)
        {
            if (ExecuteReactions)
            {
                ProcessReactions(game, actionEvent, game.AllCards.ConvertAll(c => (IReactive)c));
                ProcessReactions(game, actionEvent, game.Players.ConvertAll(p => (IReactive)p));
                ProcessReactions(game, actionEvent, new List<IReactive> { game });
            }
        }

        private void ProcessReactions(IGame game, IActionEvent actionEvent, List<IReactive> reactives)
        {
            foreach (IReactive reactive in reactives)
            {
                foreach (IAction reaction in reactive.ReactTo(game, actionEvent))
                {
                    Process(game, reaction);
                }
            }
        }
    }
}
