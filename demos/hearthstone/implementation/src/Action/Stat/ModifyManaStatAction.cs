using Newtonsoft.Json;

namespace csbcgf
{
    public class ModifyManaStatAction : Action
    {
        [JsonProperty]
        protected IStatContainer manaful = null!;

        [JsonProperty]
        protected int deltaValue;

        [JsonProperty]
        protected int deltaBaseValue;

        protected ModifyManaStatAction() { }

        public ModifyManaStatAction(
            IStatContainer manaful,
            int deltaValue,
            int deltaBaseValue,
            bool isAborted = false
            ) : base(isAborted)
        {
            this.manaful = manaful;
            this.deltaValue = deltaValue;
            this.deltaBaseValue = deltaBaseValue;
        }

        [JsonIgnore]
        public IStatContainer Manaful
        {
            get => manaful;
        }

        [JsonIgnore]
        public int DeltaValue
        {
            get => deltaValue;
            set => deltaValue = value;
        }

        [JsonIgnore]
        public int DeltaBaseValue
        {
            get => deltaBaseValue;
            set => deltaBaseValue = value;
        }

        public override void Execute(IGame game)
        {
            Manaful.AddStat(StatKeys.Mana, new Stat(DeltaValue, DeltaBaseValue));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
