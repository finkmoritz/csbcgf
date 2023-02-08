using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public abstract class CastSpellAction : csbcgf.Action<HearthstoneGameState>
    {
        [JsonProperty]
        protected IPlayer player = null!;

        [JsonProperty]
        protected ISpellCard spellCard = null!;

        protected CastSpellAction() { }

        public CastSpellAction(IPlayer player, ISpellCard spellCard, bool isAborted = false)
            : base(isAborted)
        {
            this.player = player;
            this.spellCard = spellCard;
        }

        [JsonIgnore]
        public IPlayer Player
        {
            get => player;
        }

        [JsonIgnore]
        public ISpellCard SpellCard
        {
            get => spellCard;
        }

        public override abstract void Execute(IGame<HearthstoneGameState> game);

        public override abstract bool IsExecutable(HearthstoneGameState gameState);
    }
}
