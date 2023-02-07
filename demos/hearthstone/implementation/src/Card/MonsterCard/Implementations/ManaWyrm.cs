using csbcgf;

namespace hearthstone
{
    public class ManaWyrm : HearthstoneMonsterCard
    {
        protected ManaWyrm() { }

        public ManaWyrm(bool _ = true) : base(2, 1, 3)
        {
            AddReaction(new ManaWyrmReaction());
        }

        /// <summary>
        /// Whenever you cast a spell, gain +1 Attack.
        /// </summary>
        public class ManaWyrmReaction : CardReaction<CastSpellAction>
        {
            protected override void ReactAfterInternal(IGame game, CastSpellAction action)
            {
                IPlayer? spellCardOwner = action.SpellCard.Owner;
                IPlayer? manaWyrmOwner = parentCard?.Owner;

                if (manaWyrmOwner != null
                    && parentCard != null
                    && spellCardOwner == manaWyrmOwner
                    && manaWyrmOwner.GetCardCollection(CardCollectionKeys.Board).Contains(parentCard))
                {
                    game.ActionQueue.Execute(new ModifyAttackStatAction((IAttacking)parentCard, 1));
                }
            }
        }
    }
}
