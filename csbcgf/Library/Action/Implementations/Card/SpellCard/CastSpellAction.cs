using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class CastSpellAction : Action
    {
        [JsonProperty]
        public IPlayer Player;

        [JsonProperty]
        public ISpellCard SpellCard;

        [JsonConstructor]
        public CastSpellAction(IPlayer player, ISpellCard spellCard, bool isAborted = false)
            : base(isAborted)
        {
            Player = player;
            SpellCard = spellCard;
        }

        public override abstract void Execute(IGame game);

        public override abstract bool IsExecutable(IGameState gameState);
    }
}
