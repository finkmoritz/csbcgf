namespace hearthstonestandalone
{

    public abstract class Event : EventArgs
    {
        public abstract void Execute(StateMachine stateMachine);
    }
}
