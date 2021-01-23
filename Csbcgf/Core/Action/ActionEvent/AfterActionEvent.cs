using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class AfterActionEvent : ActionEvent
    {
        [JsonConstructor]
        public AfterActionEvent(IAction action) : base(action)
        {
        }

        public override object Clone()
        {
            return new AfterActionEvent((IAction)Action.Clone());
        }

        public override bool IsAfter(Type type)
        {
            return type.IsAssignableFrom(Action.GetType());
        }

        public override bool IsBefore(Type type)
        {
            return false;
        }
    }
}
