using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class CastTargetlessSpellAction : CastSpellAction
    {
        [JsonConstructor]
        public CastTargetlessSpellAction(
            ITargetlessSpellCard spellCard,
            ICardCollection source,
            ICardCollection destination,
            bool isAborted = false
            ) : base(spellCard, source, destination, isAborted)
        {
        }

        public override object Clone()
        {
            return new CastTargetlessSpellAction(
                (ITargetlessSpellCard)SpellCard.Clone(),
                null, // otherwise circular dependencies
                null, // otherwise circular dependencies
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            IPlayer player = SpellCard.FindParentPlayer(game);
            game.Execute(new ModifyManaStatAction(player, -SpellCard.ManaValue, 0));
            game.Execute(new RemoveCardFromCardCollectionAction(SpellCard, Source));
            ((ITargetlessSpellCard)SpellCard).Cast(game);
            game.Execute(new AddCardToCardCollectionAction(SpellCard, Destination));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return SpellCard.IsCastable(gameState);
        }
    }
}
