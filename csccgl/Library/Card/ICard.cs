namespace csccgl
{
    public interface ICard
    {
        /// <summary>
        /// Costs to pay in order for the Card to be played.
        /// </summary>
        ManaStat ManaStat { get; }
    }
}
