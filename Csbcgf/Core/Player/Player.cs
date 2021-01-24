using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class Player : IPlayer
    {
        public List<IReaction> Reactions { get; }

        public Dictionary<string, ICardCollection> CardCollections { get; }

        /// <summary>
        /// Represents a Player and all his/her associated Cards.
        /// </summary>
        public Player()
            : this(
                  new List<IReaction>(),
                  new Dictionary<string, ICardCollection>())
        {
        }

        [JsonConstructor]
        protected Player(
            List<IReaction> reactions,
            Dictionary<string, ICardCollection> cardCollections)
        {
            Reactions = reactions;
            CardCollections = cardCollections;
        }

        [JsonIgnore]
        public List<ICard> AllCards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach (ICardCollection cc in CardCollections.Values)
                {
                    allCards.AddRange(cc);
                }
                return allCards;
            }
        }

        public List<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            AllCards.ForEach(c => allReactions.AddRange(c.AllReactions()));
            return allReactions;
        }

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }

        public virtual object Clone()
        {
            List<IReaction> reactionsClone = new List<IReaction>();
            foreach (IReaction reaction in Reactions)
            {
                reactionsClone.Add((IReaction)reaction.Clone());
            }

            Dictionary<string, ICardCollection> cardCollectionsClone = new Dictionary<string, ICardCollection>();
            foreach (KeyValuePair<string, ICardCollection> kv in CardCollections)
            {
                cardCollectionsClone.Add(kv.Key, (ICardCollection)kv.Value.Clone());
            }

            return new Player(reactionsClone, cardCollectionsClone);
        }

        public ICard FindParentCard(IGameState gameState)
        {
            throw new CsbcgfException("Cannot use method 'FindParentCard' on " +
                "instance of type 'Player'");
        }

        public IPlayer FindParentPlayer(IGameState gameState)
        {
            return this;
        }
    }
}
