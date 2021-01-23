using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class CastTargetfulSpellAction : CastSpellAction
    {
        [JsonProperty]
        public ICharacter Target;

        [JsonConstructor]
        public CastTargetfulSpellAction(
            ITargetfulSpellCard spellCard,
            ICardCollection source,
            ICardCollection destination,
            ICharacter target,
            bool isAborted = false
            ) : base(spellCard, source, destination, isAborted)
        {
            SpellCard = spellCard;
            Target = target;
        }

        public override object Clone()
        {
            return new CastTargetfulSpellAction(
                (ITargetfulSpellCard)SpellCard.Clone(),
                null, // otherwise circular dependencies
                null, // otherwise circular dependencies
                (ICharacter)Target.Clone(),
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            IPlayer player = SpellCard.FindParentPlayer(game);
            game.Execute(new ModifyManaStatAction(player, -SpellCard.ManaValue, 0));
            game.Execute(new RemoveCardFromCardCollectionAction(SpellCard, Source));
            ((ITargetfulSpellCard)SpellCard).Cast(game, Target);
            game.Execute(new AddCardToCardCollectionAction(SpellCard, Destination));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return SpellCard.IsCastable(gameState);
        }
    }
}
