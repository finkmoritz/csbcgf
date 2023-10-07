namespace csbcgf
{

    public abstract class Event<T> : EventArgs where T : StateMachine
    {
        public abstract void Execute(T stateMachine);
    }
}
