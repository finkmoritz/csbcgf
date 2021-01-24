using System;
using System.Collections.Generic;
using System.Linq;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public abstract class BcgCard : Card, IBcgCard
    {
        public BcgCard() : this(new List<ICardComponent>(), new List<IReaction>())
        {
        }

        [JsonConstructor]
        protected BcgCard(List<ICardComponent> components, List<IReaction> reactions)
            : base(components, reactions)
        {
        }

        [JsonIgnore]
        public int ManaValue {
            get => Math.Max(0, Components.Sum(c => c.ManaValue));
            set
            {
                Components.Add(new CardComponent(value - Components.Sum(c => c.ManaValue), 0));
            }
        }

        [JsonIgnore]
        public int ManaBaseValue {
            get => Math.Max(0, Components.Sum(c => c.ManaBaseValue));
            set
            {
                Components.Add(new CardComponent(0, value - Components.Sum(c => c.ManaBaseValue)));
            }
        }

        public virtual bool IsCastable(IGameState gameState)
        {
            IBcgPlayer owner = FindParentPlayer(gameState);
            return owner != null
                && owner == gameState.ActivePlayer
                && ManaValue <= gameState.ActivePlayer.ManaValue;
        }

        public override ICard FindParentCard(IGameState gameState)
        {
            return this;
        }

        public override IBcgPlayer FindParentPlayer(IGameState gameState)
        {
            foreach (IBcgPlayer player in gameState.Players)
            {
                if (player.AllCards.Contains(this))
                {
                    return player;
                }
            }
            return null;
        }
    }
}
