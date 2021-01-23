using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public abstract class Action : IAction
    {
        public bool IsAborted { get; set; }

        [JsonConstructor]
        public Action(bool isAborted = false)
        {
            IsAborted = isAborted;
        }

        public abstract void Execute(IGame game);
        public abstract bool IsExecutable(IGameState gameState);
        public abstract object Clone();
    }
}
