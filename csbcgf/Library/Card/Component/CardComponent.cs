namespace csbcgf
{
    public class CardComponent : Reaction, ICardComponent
    {
        protected ManaCostStat manaCostStat;

        public ICard Card { get; }

        public List<IReaction> Reactions { get; }

        public CardComponent(ICard card, int mana)
            : this(card, new ManaCostStat(mana, mana), new List<IReaction>())
        {
        }

        public CardComponent(ICard card, int manaValue, int manaBaseValue)
            : this(card, new ManaCostStat(manaValue, manaBaseValue), new List<IReaction>())
        {
        }

        protected CardComponent(ICard card, ManaCostStat manaCostStat, List<IReaction> reactions)
        {
            Card = card;
            this.manaCostStat = manaCostStat;
            Reactions = reactions;
        }

        public int ManaValue {
            get => manaCostStat.Value;
            set => manaCostStat.Value = value;
        }

        public int ManaBaseValue {
            get => manaCostStat.BaseValue;
            set => manaCostStat.BaseValue = value;
        }

        public List<IReaction> AllReactions()
        {
            return new List<IReaction>(Reactions);
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }
    }
}
