using System.Collections.Immutable;
using Newtonsoft.Json;

namespace csbcgf
{
    public class Player : StatContainer, IPlayer
    {
        [JsonProperty]
        protected List<IReaction> reactions = null!;

        [JsonProperty]
        protected IDictionary<string, ICardCollection> cardCollections = null!;

        protected Player() { }

        /// <summary>
        /// Represents a Player.
        /// </summary>
        public Player(bool _ = true) : base(_)
        {
            this.cardCollections = new Dictionary<string, ICardCollection>();
            this.reactions = new List<IReaction>();
        }

        [JsonIgnore]
        public IEnumerable<ICard> AllCards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach(ICardCollection cardCollection in cardCollections.Values)
                {
                    allCards.AddRange(cardCollection.Cards);
                }
                return allCards.ToImmutableList();
            }
        }

        [JsonIgnore]
        public IEnumerable<IReaction> Reactions
        {
            get => reactions.ToImmutableList();
        }

        public ICardCollection GetCardCollection(string key)
        {
            return cardCollections[key];
        }

        public void AddCardCollection(string key, ICardCollection cardCollection)
        {
            cardCollections.Add(key, cardCollection);
            cardCollection.Owner = this;
        }

        public bool RemoveCardCollection(string key)
        {
            if(!cardCollections.ContainsKey(key))
            {
                return false;
            }
            cardCollections[key].Owner = null;
            return cardCollections.Remove(key);
        }

        public IEnumerable<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            foreach (ICard card in AllCards)
            {
                allReactions.AddRange(card.AllReactions());
            }
            return allReactions.ToImmutableList();
        }

        public void AddReaction(IReaction reaction)
        {
            reactions.Add(reaction);
        }

        public bool RemoveReaction(IReaction reaction)
        {
            return reactions.Remove(reaction);
        }
    }
}
