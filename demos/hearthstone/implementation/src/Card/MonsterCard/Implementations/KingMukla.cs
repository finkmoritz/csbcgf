using csbcgf;

namespace hearthstone
{
    public class KingMukla : HearthstoneMonsterCard
    {
        protected KingMukla() { }

        public KingMukla(bool _ = true) : base(3, 5, 5)
        {
            AddReaction(new KingMuklaBattlecryReaction());
        }

        /// <summary>
        /// Battlecry: Give your opponent 2 Bananas.
        /// </summary>
        public class KingMuklaBattlecryReaction : CardReaction<SummonMonsterAction>
        {
            public override void ReactAfter(IGame game, SummonMonsterAction action)
            {
                HearthstoneGameState state = (HearthstoneGameState)game.State;
                if (action.MonsterCard == parentCard)
                {
                    foreach (IPlayer p in state.NonActivePlayers)
                    {
                        ICardCollection hand = p.GetCardCollection(CardCollectionKeys.Hand);
                        game.Execute(new AddCardToCardCollectionAction(hand, new Bananas()));
                        game.Execute(new AddCardToCardCollectionAction(hand, new Bananas()));
                    }
                }
            }
        }
    }
}
