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
            protected override void ReactAfterInternal(IGame game, SummonMonsterAction action)
            {
                if (action.MonsterCard == parentCard)
                {
                    foreach (IPlayer p in game.NonActivePlayers)
                    {
                        ICardCollection hand = p.GetCardCollection(CardCollectionKeys.Hand);
                        game.ActionQueue.Execute(new AddCardToCardCollectionAction(hand, new Bananas()));
                        game.ActionQueue.Execute(new AddCardToCardCollectionAction(hand, new Bananas()));
                    }
                }
            }
        }
    }
}
