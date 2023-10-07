namespace csbcgf
{
    public abstract class GameObject<T> where T : StateMachine
    {
        public T StateMachine { get; init; }

        protected GameObject(T stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
