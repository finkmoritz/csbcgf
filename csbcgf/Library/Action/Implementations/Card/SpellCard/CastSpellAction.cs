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

        public CastSpellAction(IPlayer player, ISpellCard spellCard)
        {
            Player = player;
            SpellCard = spellCard;
        }

        public override abstract void Execute(IGame game);

        public override abstract bool IsExecutable(IGameState gameState);
    }
}
