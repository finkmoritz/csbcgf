using System;
namespace csbcgf
{
    public class AfterActionEvent : ActionEvent
    {
        public AfterActionEvent(IAction action) : base(action)
        {
        }

        public override bool IsAfter(Type type)
        {
            return Action.GetType().IsAssignableFrom(type);
        }

        public override bool IsBefore(Type type)
        {
            return false;
        }
    }
}
