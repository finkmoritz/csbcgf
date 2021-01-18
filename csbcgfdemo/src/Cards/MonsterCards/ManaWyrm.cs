using System;
using csbcgf;
using Newtonsoft.Json;

namespace csbcgfdemo
{
    [Serializable]
    public class ManaWyrm : MonsterCard
    {
        [JsonConstructor]
        public ManaWyrm() : base(2, 1, 3)
        {
            AddReaction(new ManaWyrmReaction());
        }

        /// <summary>
        /// Whenever you cast a spell, gain +1 Attack.
        /// </summary>
        [Serializable]
        public class ManaWyrmReaction : Reaction
        {
            public override object Clone()
            {
                return new ManaWyrmReaction();
            }

            public override void ReactTo(IGame game, IActionEvent actionEvent)
            {
                if (actionEvent.IsAfter(typeof(CastSpellAction)))
                {
                    CastSpellAction a = (CastSpellAction)actionEvent.Action;
                    ICard parentCard = FindParentCard(game);
                    IPlayer spellCardOwner = a.SpellCard.FindOwner(game);
                    IPlayer manaWyrmOwner = parentCard.FindOwner(game);

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
