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
        public class KingMuklaBattlecryReaction : CardReaction<HearthstoneGameState, HearthstoneGame, SummonMonsterAction>
        {
            public override void ReactAfter(HearthstoneGame game, SummonMonsterAction action)
            {
                if (action.MonsterCard == parentCard)
                {
                    foreach (IPlayer p in game.State.NonActivePlayers)
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
