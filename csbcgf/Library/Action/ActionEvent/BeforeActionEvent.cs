using System;
namespace csbcgf
{
    public class BeforeActionEvent : ActionEvent
    {
        public BeforeActionEvent(IAction action) : base(action)
        {
        }

        public override bool IsAfter(Type type)
        {
            return false;
        }

        public override bool IsBefore(Type type)
        {
            return Action.GetType().IsAssignableFrom(type);
        }
    }
}
