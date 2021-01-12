using System;
using csbcgf;

namespace csbcgfdemo
{
    [Serializable]
    public class KingMukla : MonsterCard
    {
        public KingMukla() : base(3, 5, 5)
        {
            AddReaction(new KingMuklaBattlecryReaction(this));
        }

        /// <summary>
        /// Battlecry: Give your opponent 2 Bananas.
        /// </summary>
        [Serializable]
        public class KingMuklaBattlecryReaction : IReaction
        {
            public IMonsterCard ParentCard;

            public KingMuklaBattlecryReaction(IMonsterCard parentCard)
            {
                ParentCard = parentCard;
            }

            public void ReactTo(IGame game, IActionEvent actionEvent)
            {
                if (actionEvent.IsAfter(typeof(CastMonsterAction)))
                {
                    CastMonsterAction action = (CastMonsterAction)actionEvent.Action;
                    if (action.MonsterCard == ParentCard)
                    {
                        game.NonActivePlayers.ForEach(p =>
                            {
                                game.Execute(new AddCardToHandAction(p.Hand, new Bananas()));
                                game.Execute(new AddCardToHandAction(p.Hand, new Bananas()));
                            }
                        );
                    }
                }
            }
        }
    }
}
