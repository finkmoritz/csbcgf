using Newtonsoft.Json;

namespace csbcgf
{
    public class ModifyLifeStatAction : Action
    {
        [JsonProperty]
        protected ILiving living = null!;

        [JsonProperty]
        protected int delta;

        protected ModifyLifeStatAction() {}

        public ModifyLifeStatAction(ILiving living, int delta, bool isAborted = false)
            : base(isAborted)
        {
            this.living = living;
            this.delta = delta;
        }

        [JsonIgnore]
        public ILiving Living {
            get => living;
        }

        [JsonIgnore]
        public int Delta {
            get => delta;
            set => delta = value;
        }

        public override void Execute(IGame game)
        {
            Living.LifeValue += Delta;
            if(Living.LifeValue <= 0)
            {
                if (Living is IMonsterCard monsterCard)
                {
                    game.Execute(new DieAction(monsterCard));
                }
                else if (Living is IPlayer)
                {
                    game.Execute(new EndOfGameEvent());
                }
            }
            
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !(Living is ICardComponent)
                && Living.LifeValue > 0;
        }
    }
}
