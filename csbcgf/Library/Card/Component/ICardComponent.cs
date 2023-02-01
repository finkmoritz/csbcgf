namespace csbcgf
{
    public interface ICardComponent : IManaful, IReactive
    {
        /// <summary>
        /// The ICard this ICardComponent is attached to.
        /// </summary>
        /// <returns>The ICard this ICardComponent is attached to or null
        /// if it is not attached.</returns>
        ICompound? ParentCard { get; set; }
    }
}
