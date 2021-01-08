using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyAttackStatAction : IAction
    {
        [JsonProperty]
        public ICharacter Character;

        [JsonProperty]
        public int Delta;

        [JsonConstructor]
        public ModifyAttackStatAction(ICharacter character, int delta)
        {
            Character = character;
            Delta = delta;
        }

        public void Execute(IGame game)
        {
            Character.AttackValue += Delta;
        }

        public bool IsExecutable(IGame gameState)
        {
            return true;
        }
    }
}
