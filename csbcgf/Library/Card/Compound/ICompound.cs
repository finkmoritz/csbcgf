namespace csbcgf
{
    public interface ICompound : IStatContainer
    {
        /// <summary>
        /// List of components that this ICompound is made of.
        /// </summary>
        IEnumerable<ICardComponent> Components { get; }

        /// <summary>
        /// Add a component to this ICompound.
        /// </summary>
        void AddComponent(ICardComponent cardComponent);

        /// <summary>
        /// Remove a component from this ICompound.
        /// </summary>
        bool RemoveComponent(ICardComponent cardComponent);
    }
}
