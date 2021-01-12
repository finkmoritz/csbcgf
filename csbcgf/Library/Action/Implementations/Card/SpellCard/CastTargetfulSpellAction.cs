using System;
using Newtonsoft.Json;

namespace csbcgf
{
    public class CastTargetfulSpellAction : CastSpellAction
    {
        [JsonProperty]
        public ICharacter Target;

        public CastTargetfulSpellAction(IPlayer player, ITargetfulSpellCard spellCard, ICharacter target)
            : base(player, spellCard)
        {
            SpellCard = spellCard;
            Target = target;
        }

        public override void Execute(IGame game)
        {
            game.Execute(new ModifyManaStatAction(Player, -SpellCard.ManaValue, 0));
            game.Execute(new RemoveCardFromHandAction(Player.Hand, SpellCard));
            ((ITargetfulSpellCard)SpellCard).Cast(game, Target);
            game.Execute(new AddCardToGraveyardAction(Player.Graveyard, SpellCard));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return SpellCard.IsCastable(gameState);
        }
    }
}
