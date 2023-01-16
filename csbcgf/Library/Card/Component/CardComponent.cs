namespace csbcgf
{
    public class CardComponent : Reaction, ICardComponent
    {
        protected ManaCostStat manaCostStat;

        public List<IReaction> Reactions { get; }

        public CardComponent(int mana)
            : this(new ManaCostStat(mana, mana), new List<IReaction>())
        {
        }

        public CardComponent(int manaValue, int manaBaseValue)
            : this(new ManaCostStat(manaValue, manaBaseValue), new List<IReaction>())
        {
        }

        protected CardComponent(ManaCostStat manaCostStat, List<IReaction> reactions)
        {
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

        public ICard? FindCard(IGameState gameState)
        {
            foreach (ICard card in gameState.AllCards)
            {
                foreach (ICardComponent cardComponent in card.Components)
                {
                    if (cardComponent == this)
                    {
                        return card;
                    }
                }
            }
            return null;
        }
    }
}
