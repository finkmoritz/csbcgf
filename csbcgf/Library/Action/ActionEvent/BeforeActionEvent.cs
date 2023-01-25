namespace csbcgf
{
    public class BeforeActionEvent : ActionEvent
    {
        protected BeforeActionEvent() {}

        public BeforeActionEvent(IAction action) : base(action)
        {
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
