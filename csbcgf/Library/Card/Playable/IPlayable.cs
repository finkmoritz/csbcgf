using csbcgf;

namespace csbcgf
{
    public interface IPlayable
    {
        /// <summary>
        /// Checks if this Card is playable.
        /// </summary>
        /// <returns>True if this Card can be played.</returns>
        bool IsPlayable(IGame game);
    }
}
