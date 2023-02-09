using Newtonsoft.Json;

namespace csbcgf
{
    public class ModifyLifeStatAction<T> : Action<T> where T : IGameState
    {
        [JsonProperty]
        protected ILiving living = null!;

        [JsonProperty]
        protected int delta;

        protected ModifyLifeStatAction() { }

        public ModifyLifeStatAction(ILiving living, int delta, bool isAborted = false)
            : base(isAborted)
        {
            this.living = living;
            this.delta = delta;
        }

        [JsonIgnore]
        public ILiving Living
        {
            get => living;
        }

        [JsonIgnore]
        public int Delta
        {
            get => delta;
            set => delta = value;
        }

        public override void Execute(IGame<T> game)
        {
            Living.LifeValue += Delta;
        }

        public override bool IsExecutable(T gameState)
        {
            return !(Living is ICardComponent)
                && Living.LifeValue > 0;
        }
    }
}
