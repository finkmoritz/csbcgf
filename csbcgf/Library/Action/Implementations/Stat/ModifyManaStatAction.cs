using System;

namespace csbcgf
{
    public class ModifyManaStatAction : Action
    {
        public IManaful Manaful;

        public int DeltaValue;

        public int DeltaBaseValue;

        public ModifyManaStatAction(
            IManaful manaful,
            int deltaValue,
            int deltaBaseValue,
            bool isAborted = false
            ) : base(isAborted)
        {
            Manaful = manaful;
            DeltaValue = deltaValue;
            DeltaBaseValue = deltaBaseValue;
        }

        public override object Clone()
        {
            return new ModifyManaStatAction(
                (IManaful)Manaful.Clone(),
                DeltaValue,
                DeltaBaseValue,
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            Manaful.ManaBaseValue += DeltaBaseValue;
            Manaful.ManaValue += DeltaValue;
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
