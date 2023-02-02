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
            if(game.ExecuteSequentially(new List<IAction> {
                new ModifyManaStatAction(Player, -SpellCard.ManaValue, 0),
                new RemoveCardFromCardCollectionAction(Player.Hand, SpellCard)
            }).Count == 2)
            {
                ((ITargetlessSpellCard)SpellCard).Cast(game);
                game.Execute(new AddCardToCardCollectionAction(Player.Graveyard, SpellCard));
            }
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return SpellCard.IsCastable(gameState);
        }
    }
}
