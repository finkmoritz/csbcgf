using System;

namespace csbcgf
{
    public class ModifyLifeStatAction : Action
    {
        public ILiving Living;

        public int Delta;

        public ModifyLifeStatAction(ILiving living, int delta, bool isAborted = false)
            : base(isAborted)
        {
            Living = living;
            Delta = delta;
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
