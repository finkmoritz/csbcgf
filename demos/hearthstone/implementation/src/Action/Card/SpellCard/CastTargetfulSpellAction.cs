using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class CastTargetfulSpellAction : CastSpellAction
    {
        [JsonProperty]
        public ICharacter target = null!;

        protected CastTargetfulSpellAction() { }

        public CastTargetfulSpellAction(
            IPlayer player,
            HearthstoneTargetfulSpellCard spellCard,
            ICharacter target,
            bool isAborted = false
            ) : base(player, spellCard, isAborted)
        {
            this.spellCard = spellCard;
            this.target = target;
        }

        [JsonIgnore]
        public ICharacter Target
        {
            get => target;
        }

        public override void Execute(IGame<HearthstoneGameState> game)
        {
            if(game.ExecuteSequentially(new List<IAction> {
                new ModifyManaStatAction(Player, -SpellCard.ManaValue, 0),
                new RemoveCardFromCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Hand), SpellCard)
            }).Count == 2)
            {
                ((HearthstoneTargetfulSpellCard)SpellCard).Cast(game, Target);
                game.Execute(new AddCardToCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Graveyard), SpellCard));
            }
        }

        public override bool IsExecutable(HearthstoneGameState gameState)
        {
            return SpellCard.IsCastable(gameState);
        }
    }
}
