﻿using System;
using System.Collections.Generic;
using csbcgf;
using Newtonsoft.Json;

namespace csbcgftest
{
    [Serializable]
    public class DamageSpellCard : TargetfulSpellCard
    {
        public DamageSpellCard(uint damage)
            : base(new DamageSpellCardComponent((int)damage, damage))
        {
        }

        [JsonConstructor]
        protected DamageSpellCard()
        {
        }

        [Serializable]
        public class DamageSpellCardComponent : TargetfulSpellCardComponent
        {
            [JsonProperty]
            private readonly uint damage;

            public DamageSpellCardComponent(int mana, uint damage) : base(mana)
            {
                this.damage = damage;
            }

            public override void Cast(IGame game, ICharacter target)
            {
                game.Execute(new ModifyLifeStatAction(target, -(int)damage));
            }

            public override HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
            {
                HashSet<ICharacter> targets = new HashSet<ICharacter>();
                foreach (IPlayer player in gameState.Players)
                {
                    targets.Add(player);
                    player.Board.AllCards.ForEach(c => targets.Add((ICharacter)c));
                }
                return targets;
            }
        }
    }
}
