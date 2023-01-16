namespace csbcgf
{
    public interface ICompound
    {
        /// <summary>
        /// List of components that this Compound is made of.
        /// </summary>
        List<ICardComponent> Components { get; }
    }
}
