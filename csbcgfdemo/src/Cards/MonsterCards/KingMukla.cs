using csbcgf;

namespace csbcgfdemo
{
    public class KingMukla : MonsterCard
    {
        protected KingMukla() {}
        
        public KingMukla(bool initialize = true) : base(3, 5, 5)
        {
            AddReaction(new KingMuklaBattlecryReaction());
        }

        /// <summary>
        /// Battlecry: Give your opponent 2 Bananas.
        /// </summary>
        public class KingMuklaBattlecryReaction : CardReaction
        {
            public override void ReactTo(IGame game, IActionEvent actionEvent)
            {
                if (actionEvent.IsAfter(typeof(CastMonsterAction)))
                {
                    CastMonsterAction action = (CastMonsterAction)actionEvent.Action;
                    if (action.MonsterCard == parentCard)
                    {
                        foreach(IPlayer p in game.NonActivePlayers)
                        {
                            game.Execute(new AddCardToCardCollectionAction(p.Hand, new Bananas()));
                            game.Execute(new AddCardToCardCollectionAction(p.Hand, new Bananas()));
                        }
                        ;
                    }
                }
            }
        }
    }
}
