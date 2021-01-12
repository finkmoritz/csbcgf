﻿using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class Action : IAction
    {
        public bool IsAborted { get; set; }

        [JsonConstructor]
        public Action(bool aborted = false)
        {
            IsAborted = aborted;
        }

        public abstract void Execute(IGame game);

        public abstract bool IsExecutable(IGameState gameState);
    }
}
