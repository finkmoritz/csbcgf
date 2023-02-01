namespace csbcgf
{
    public abstract class SpellCard : Card, ISpellCard
    {
        protected SpellCard() : base() {}

        /// <summary>
        /// Represents a certain type of Card that has an
        /// immediate effect on the Game's state.
        /// </summary>
        public SpellCard(bool initialize = true)
            : base(initialize)
        {
        }
    }
}
