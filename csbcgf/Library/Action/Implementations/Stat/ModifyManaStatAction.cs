using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyManaStatAction : IAction
    {
        [JsonProperty]
        public ICharacter Character;

        [JsonProperty]
        public int DeltaValue;

        [JsonProperty]
        public int DeltaBaseValue;

        [JsonConstructor]
        public ModifyManaStatAction(ICharacter character, int deltaValue, int deltaBaseValue)
        {
            Character = character;
            DeltaValue = deltaValue;
            DeltaBaseValue = deltaBaseValue;
        }

        public void Execute(IGame game)
        {
            Character.ManaBaseValue += DeltaBaseValue;
            Character.ManaValue += DeltaValue;
        }

        public bool IsExecutable(IGame gameState)
        {
            return true;
        }
    }
}
