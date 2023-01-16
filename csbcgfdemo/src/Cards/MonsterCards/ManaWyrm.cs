using csbcgf;

namespace csbcgfdemo
{
    public class ManaWyrm : MonsterCard
    {
        public ManaWyrm() : base(2, 1, 3)
        {
            Reactions.Add(new ManaWyrmReaction());
        }

        /// <summary>
        /// Whenever you cast a spell, gain +1 Attack.
        /// </summary>
        public class ManaWyrmReaction : Reaction
        {
            public override void ReactTo(IGame game, IActionEvent actionEvent)
            {
                if (actionEvent.IsAfter(typeof(CastSpellAction)))
                {
                    CastSpellAction a = (CastSpellAction)actionEvent.Action;
                    ICard parentCard = FindParentCard(game);
                    IPlayer spellCardOwner = a.SpellCard.FindParentPlayer(game);
                    IPlayer manaWyrmOwner = parentCard.FindParentPlayer(game);

                    if (spellCardOwner == manaWyrmOwner
                        && manaWyrmOwner.Board.Contains(parentCard))
                    {
                        game.Execute(new ModifyAttackStatAction((IAttacking)parentCard, 1));
                    }
                }
            }
        }
    }
}
