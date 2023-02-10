using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public abstract class CastSpellAction : csbcgf.Action<HearthstoneGameState>
    {
        [JsonProperty]
        protected HearthstonePlayer player = null!;

        [JsonProperty]
        protected HearthstoneSpellCard spellCard = null!;

        protected CastSpellAction() { }

        public CastSpellAction(HearthstonePlayer player, HearthstoneSpellCard spellCard, bool isAborted = false)
            : base(isAborted)
        {
            this.player = player;
            this.spellCard = spellCard;
        }

        [JsonIgnore]
        public HearthstonePlayer Player
        {
            get => player;
        }

        [JsonIgnore]
        public HearthstoneSpellCard SpellCard
        {
            get => spellCard;
        }

        public override abstract void Execute(IGame<HearthstoneGameState> game);

        public override abstract bool IsExecutable(HearthstoneGameState gameState);
    }
}
