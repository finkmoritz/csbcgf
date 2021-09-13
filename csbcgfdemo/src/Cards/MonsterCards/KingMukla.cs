using System;
using csbcgf;

namespace csbcgfdemo
{
    public class KingMukla : MonsterCard
    {
        public KingMukla() : base(3, 5, 5)
        {
            Reactions.Add(new KingMuklaBattlecryReaction());
        }

        /// <summary>
        /// Battlecry: Give your opponent 2 Bananas.
        /// </summary>
        public class KingMuklaBattlecryReaction : Reaction
        {
            public override object Clone()
            {
                return new KingMuklaBattlecryReaction();
            }

            public override void ReactTo(IGame game, IActionEvent actionEvent)
            {
                if (actionEvent.IsAfter(typeof(CastMonsterAction)))
                {
                    CastMonsterAction action = (CastMonsterAction)actionEvent.Action;
                    ICard parentCard = FindParentCard(game);

                    if (action.MonsterCard == parentCard)
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
