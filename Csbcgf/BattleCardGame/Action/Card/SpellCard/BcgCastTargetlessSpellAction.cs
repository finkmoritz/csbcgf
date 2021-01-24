using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgCastTargetlessSpellAction : BcgCastSpellAction
    {
        [JsonConstructor]
        public BcgCastTargetlessSpellAction(
            IBcgTargetlessSpellCard spellCard,
            ICardCollection source,
            ICardCollection destination,
            bool isAborted = false
            ) : base(spellCard, source, destination, isAborted)
        {
        }

        public override object Clone()
        {
            return new BcgCastTargetlessSpellAction(
                (IBcgTargetlessSpellCard)SpellCard.Clone(),
                null, // otherwise circular dependencies
                null, // otherwise circular dependencies
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            IBcgPlayer player = (IBcgPlayer)SpellCard.FindParentPlayer(game);
            game.Execute(new BcgModifyManaStatAction(player, -SpellCard.ManaValue, 0));
            game.Execute(new RemoveCardFromCardCollectionAction(SpellCard, Source));
            ((IBcgTargetlessSpellCard)SpellCard).Cast((IBcgGame)game);
            game.Execute(new AddCardToCardCollectionAction(SpellCard, Destination));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return SpellCard.IsCastable((IBcgGameState)gameState);
        }
    }
}
