using System;

namespace csbcgf
{
    public class CastTargetlessSpellAction : CastSpellAction
    {
        public CastTargetlessSpellAction(
            IPlayer player,
            ITargetlessSpellCard spellCard,
            bool isAborted = false
            ) : base(player, spellCard, isAborted)
        {
        }

        public override object Clone()
        {
            return new CastTargetlessSpellAction(
                null, // otherwise circular dependencies
                (ITargetlessSpellCard)SpellCard.Clone(),
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            game.Execute(new ModifyManaStatAction(Player, -SpellCard.ManaValue, 0));
            game.Execute(new RemoveCardFromHandAction(Player.Hand, SpellCard));
            ((ITargetlessSpellCard)SpellCard).Cast(game);
            game.Execute(new AddCardToGraveyardAction(Player.Graveyard, SpellCard));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return SpellCard.IsCastable(gameState);
        }
    }
}
