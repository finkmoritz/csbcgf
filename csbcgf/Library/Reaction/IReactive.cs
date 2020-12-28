using System.Collections.Generic;

namespace csbcgf
{
    public interface IReactive : IReaction
    {
        /// <summary>
        /// Collection of Reactions.
        /// </summary>
        List<IReaction> Reactions { get; }

        /// <summary>
        /// Add a Reaction to this Reactive.
        /// </summary>
        /// <param name="reaction"></param>
        void AddReaction(IReaction reaction);

        /// <summary>
        /// Remove a Reaction from this Reactive.
        /// </summary>
        /// <param name="reaction"></param>
        void RemoveReaction(IReaction reaction);
    }
}
