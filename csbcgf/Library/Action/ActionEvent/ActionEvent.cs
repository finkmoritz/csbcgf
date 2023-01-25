using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class ActionEvent : Event, IActionEvent
    {
        [JsonProperty]
        protected IAction action = null!;

        protected ActionEvent() {}

        public ActionEvent(IAction action)
        {
            this.action = action;
        }

        [JsonIgnore]
        public IAction Action { get => action; }

        public abstract bool IsBefore(Type type);

        public abstract bool IsAfter(Type type);
    }
}
