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
            return type.IsAssignableFrom(Action.GetType());
        }

        public override bool IsBefore(Type type)
        {
            return false;
        }
    }
}
