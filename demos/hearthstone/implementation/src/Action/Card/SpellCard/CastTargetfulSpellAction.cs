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
            ITargetfulSpellCard spellCard,
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

        public override void Execute(IGame game)
        {
            if(game.ActionQueue.ExecuteSequentially(new List<IAction> {
                new ModifyManaStatAction(Player, -SpellCard.ManaValue, 0),
                new RemoveCardFromCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Hand), SpellCard)
            }).Count == 2)
            {
                ((ITargetfulSpellCard)SpellCard).Cast(game, Target);
                game.ActionQueue.Execute(new AddCardToCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Graveyard), SpellCard));
            }
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return SpellCard.IsCastable(gameState);
        }
    }
}
