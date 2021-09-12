using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class ActionEvent : Event, IActionEvent
    {
        public IAction Action { get; protected set; }

        [JsonConstructor]
        public ActionEvent(IAction action)
        {
            Action = action;
        }

        public abstract bool IsBefore(Type type);

        public abstract bool IsAfter(Type type);
    }
}
