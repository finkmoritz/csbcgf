namespace csbcgf
{
    public interface IPlayer : IStatContainer, IReactive
    {
        /// <summary>
        /// Get all Cards from the Player's Decks.
        /// </summary>
        IEnumerable<ICard> AllCards { get; }

        ICardCollection GetCardCollection(string key);

        void AddCardCollection(string key, ICardCollection cardCollection);

        bool RemoveCardCollection(string key);
    }
}
