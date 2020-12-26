namespace csccgl
{
    public interface ICard : IOwnable
    {
        /// <summary>
        /// Costs to pay in order for the Card to be played.
        /// </summary>
        ManaStat ManaStat { get; }

        /// <summary>
        /// Checks if this Card is playable.
        /// </summary>
        /// <returns>True if this Card can be played.</returns>
        bool IsPlayable(IGame game);
    }
}
