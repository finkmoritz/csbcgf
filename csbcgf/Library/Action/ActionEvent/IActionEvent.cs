namespace csbcgf
{
    public interface IActionEvent : IAction
    {
        /// <summary>
        /// Contains the Action this event refers to.
        /// </summary>
        IAction Action { get; }
    }
}
