using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class BeforeActionEvent : ActionEvent
    {
        [JsonConstructor]
        public BeforeActionEvent(IAction action) : base(action)
        {
        }

        public override object Clone()
        {
            return new BeforeActionEvent((IAction)Action.Clone());
        }

        public override bool IsAfter(Type type)
        {
            return false;
        }

        public override bool IsBefore(Type type)
        {
            return type.IsAssignableFrom(Action.GetType());
        }
    }
}
