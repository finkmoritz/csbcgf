using Newtonsoft.Json;

namespace csbcgf
{
    public class ModifyLifeStatAction<T> : Action<T> where T : IGameState
    {
        [JsonProperty]
        protected IStatContainer living = null!;

        [JsonProperty]
        protected int delta;

        protected ModifyLifeStatAction() { }

        public ModifyLifeStatAction(IStatContainer living, int delta, bool isAborted = false)
            : base(isAborted)
        {
            this.living = living;
            this.delta = delta;
        }

        [JsonIgnore]
        public IStatContainer Living
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
            Living.AddStat(StatKeys.Life, new Stat(Delta, 0));
        }

        public override bool IsExecutable(T gameState)
        {
            return Living.GetValue(StatKeys.Life) > 0;
        }
    }
}
