using csbcgf;

namespace csccgl
{
    public interface ICardComponent : IManaful, IReactive
    {
        /// <summary>
        /// Provides the parent Card this Component is attached to.
        /// Returns null if the Component is not assigned to a Card.
        /// </summary>
        ICard ParentCard { get; set; }
    }
}
