using Newtonsoft.Json;

namespace csbcgf
{
    public class CastTargetfulSpellAction : CastSpellAction
    {
        [JsonProperty]
        public ICharacter target = null!;

        protected CastTargetfulSpellAction() {}

        public CastTargetfulSpellAction(
            IPlayer player,
            ITargetfulSpellCard spellCard,
            ICharacter target,
            bool isAborted = false
            ) : base(player, spellCard, isAborted)
        {
            this.spellCard = spellCard;
            this.target = target;
        }

        [JsonIgnore]
        public ICharacter Target {
            get => target;
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
