using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgCastTargetfulSpellAction : BcgCastSpellAction
    {
        [JsonProperty]
        public IBcgCharacter Target;

        [JsonConstructor]
        public BcgCastTargetfulSpellAction(
            IBcgTargetfulSpellCard spellCard,
            ICardCollection source,
            ICardCollection destination,
            IBcgCharacter target,
            bool isAborted = false
            ) : base(spellCard, source, destination, isAborted)
        {
            SpellCard = spellCard;
            Target = target;
        }

        public override object Clone()
        {
            return new BcgCastTargetfulSpellAction(
                (IBcgTargetfulSpellCard)SpellCard.Clone(),
                null, // otherwise circular dependencies
                null, // otherwise circular dependencies
                (IBcgCharacter)Target.Clone(),
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            IBcgPlayer player = (IBcgPlayer)SpellCard.FindParentPlayer(game);
            game.Execute(new BcgModifyManaStatAction(player, -SpellCard.ManaValue, 0));
            game.Execute(new RemoveCardFromCardCollectionAction(SpellCard, Source));
            ((IBcgTargetfulSpellCard)SpellCard).Cast((IBcgGame)game, Target);
            game.Execute(new AddCardToCardCollectionAction(SpellCard, Destination));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return SpellCard.IsCastable((IBcgGameState)gameState);
        }
    }
}
