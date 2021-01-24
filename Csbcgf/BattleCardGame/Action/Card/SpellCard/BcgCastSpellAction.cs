using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public abstract class BcgCastSpellAction : Core.Action
    {
        [JsonProperty]
        public IBcgSpellCard SpellCard;

        [JsonProperty]
        public ICardCollection Source;

        [JsonProperty]
        public ICardCollection Destination;

        [JsonConstructor]
        public BcgCastSpellAction(
            IBcgSpellCard spellCard,
            ICardCollection source,
            ICardCollection destination,
            bool isAborted = false
        ) : base(isAborted)
        {
            SpellCard = spellCard;
            Source = source;
            Destination = destination;
        }

        public override abstract void Execute(IGame game);

        public override abstract bool IsExecutable(IGameState gameState);
    }
}
