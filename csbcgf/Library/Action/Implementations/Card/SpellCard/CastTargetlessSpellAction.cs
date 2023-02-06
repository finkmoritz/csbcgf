namespace csbcgf
{
    public class CastTargetlessSpellAction : CastSpellAction
    {
        protected CastTargetlessSpellAction() { }

        public CastTargetlessSpellAction(
            IPlayer player,
            ITargetlessSpellCard spellCard,
            bool isAborted = false
            ) : base(player, spellCard, isAborted)
        {
        }

        public override void Execute(IGame game)
        {
            if(game.ActionQueue.ExecuteSequentially(new List<IAction> {
                new ModifyManaStatAction(Player, -SpellCard.ManaValue, 0),
                new RemoveCardFromCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Hand), SpellCard)
            }).Count == 2)
            {
                ((ITargetlessSpellCard)SpellCard).Cast(game);
                game.ActionQueue.Execute(new AddCardToCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Graveyard), SpellCard));
            }
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return SpellCard.IsCastable(gameState);
        }
    }
}
