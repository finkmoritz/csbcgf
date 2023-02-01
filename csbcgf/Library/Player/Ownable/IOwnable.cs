namespace csbcgf
{

    public interface IOwnable
    {
        /// <summary>
        /// Return the IPlayer that owns this IOwnable.
        /// </summary>
        /// <returns>The IPlayer that owns this IOwnable or null if it
        /// is not owned by a player.</returns>
        IPlayer? Owner { get; set; }
    }
}
