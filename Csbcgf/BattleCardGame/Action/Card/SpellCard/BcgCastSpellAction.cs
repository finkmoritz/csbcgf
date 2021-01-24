using System;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public abstract class BcgCastSpellAction : Action
    {
        [JsonProperty]
        public ISpellCard SpellCard;

        [JsonProperty]
        public ICardCollection Source;

        [JsonProperty]
        public ICardCollection Destination;

        [JsonConstructor]
        public BcgCastSpellAction(
            ISpellCard spellCard,
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
