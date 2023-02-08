using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class PlayerReaction<T> : Reaction<T>, IPlayerReaction<T> where T : IAction
    {
        [JsonProperty]
        protected IPlayer parentPlayer = null!;

        protected PlayerReaction() { }

        public PlayerReaction(IPlayer parentPlayer)
        {
            this.parentPlayer = parentPlayer;
        }

        [JsonIgnore]
        public IPlayer ParentPlayer
        {
            get
            {
                return parentPlayer;
            }
        }
    }
}
