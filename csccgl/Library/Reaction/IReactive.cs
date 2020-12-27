using System;
namespace csccgl
{
    public interface IReactive : IReaction
    {
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
