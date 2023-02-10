using csbcgf;

namespace hearthstone
{
    public class CastTargetlessSpellAction : CastSpellAction
    {
        protected CastTargetlessSpellAction() { }

        public CastTargetlessSpellAction(
            HearthstonePlayer player,
            HearthstoneTargetlessSpellCard spellCard,
            bool isAborted = false
            ) : base(player, spellCard, isAborted)
        {
        }

        public override void Execute(IGame<HearthstoneGameState> game)
        {
            if(game.ExecuteSequentially(new List<IAction> {
                new ModifyManaStatAction(Player, -SpellCard.GetValue(StatKeys.Mana), 0),
                new RemoveCardFromCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Hand), SpellCard)
            }).Count == 2)
            {
                ((HearthstoneTargetlessSpellCard)SpellCard).Cast((HearthstoneGame)game);
                game.Execute(new AddCardToCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Graveyard), SpellCard));
            }
        }

        public override bool IsExecutable(HearthstoneGameState gameState)
        {
            return SpellCard.IsCastable(gameState);
        }
    }
}
