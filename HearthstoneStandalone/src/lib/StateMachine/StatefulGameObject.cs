namespace hearthstonestandalone
{
    public abstract class StatefulGameObject
    {
        public StateMachine StateMachine { get; init; }

        protected StatefulGameObject(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
