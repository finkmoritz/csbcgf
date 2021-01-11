using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class CastSpellAction : IAction
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

        public abstract void Execute(IGame game);

        public abstract bool IsExecutable(IGameState gameState);
    }
}
