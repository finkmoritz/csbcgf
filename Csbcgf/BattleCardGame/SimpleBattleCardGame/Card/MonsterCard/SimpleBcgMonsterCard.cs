using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame.SimpleBattleCardGame
{
    [Serializable]
    public class SimpleBcgMonsterCard : BcgMonsterCard
    {
        public SimpleBcgMonsterCard()
            : this(new List<IBcgMonsterCardComponent>())
        {
        }

        public SimpleBcgMonsterCard(int mana, int attack, int life)
            : this(new List<IBcgMonsterCardComponent> { new BcgMonsterCardComponent(mana, attack, life) })
        {
        }

        public SimpleBcgMonsterCard(List<IBcgMonsterCardComponent> components)
            : this(components, false)
        {
        }

        public SimpleBcgMonsterCard(
            List<IBcgMonsterCardComponent> components,
            bool isReadyToAttack
            ) : this(components.ConvertAll(c => (ICardComponent)c), new List<IReaction>(), isReadyToAttack)
        {
            Reactions.Add(new BcgSetReadyToAttackOnStartOfTurnEventReaction());
        }

        [JsonConstructor]
        public SimpleBcgMonsterCard(
            List<ICardComponent> components,
            List<IReaction> reactions,
            bool isReadyToAttack
            ) : base(components, reactions, isReadyToAttack)
        {
            IsReadyToAttack = isReadyToAttack;
        }

        public override void Attack(IBcgGame game, IBcgCharacter target)
        {
            base.Attack(game, target);
            game.Execute(new BcgModifyReadyToAttackAction(this, false));
        }
    }
}
