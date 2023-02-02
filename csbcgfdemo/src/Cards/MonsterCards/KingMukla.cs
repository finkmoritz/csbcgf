using csbcgf;

namespace csbcgfdemo
{
    public class KingMukla : MonsterCard
    {
        protected KingMukla() { }

        public KingMukla(bool initialize = true) : base(3, 5, 5)
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
                        game.ActionQueue.Execute(new AddCardToCardCollectionAction(p.Hand, new Bananas()));
                        game.ActionQueue.Execute(new AddCardToCardCollectionAction(p.Hand, new Bananas()));
                    }
                }
            }
        }
    }
}
