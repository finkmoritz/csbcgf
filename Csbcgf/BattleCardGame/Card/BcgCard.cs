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
        public BcgCard()
            : this(new List<IBcgCardComponent>(), new List<IReaction>())
        {
        }

        public BcgCard(List<IBcgCardComponent> components, List<IReaction> reactions)
            : this(components.ConvertAll(c => (ICardComponent)c), reactions)
        {
        }

        [JsonConstructor]
        protected BcgCard(List<ICardComponent> components, List<IReaction> reactions)
            : base(components, reactions)
        {
        }

        [JsonIgnore]
        public int ManaValue {
            get => Math.Max(0, Components.Sum(c => ((BcgCardComponent)c).ManaValue));
            set
            {
                Components.Add(new BcgCardComponent(value - Components.Sum(c => ((BcgCardComponent)c).ManaValue), 0));
            }
        }

        [JsonIgnore]
        public int ManaBaseValue {
            get => Math.Max(0, Components.Sum(c => ((BcgCardComponent)c).ManaBaseValue));
            set
            {
                Components.Add(new BcgCardComponent(0, value - Components.Sum(c => ((BcgCardComponent)c).ManaBaseValue)));
            }
        }

        public virtual bool IsCastable(IBcgGameState gameState)
        {
            IBcgPlayer owner = (IBcgPlayer)FindParentPlayer(gameState);
            return owner != null
                && owner == gameState.ActivePlayer
                && ManaValue <= owner.ManaValue;
        }

        public override ICard FindParentCard(IGameState gameState)
        {
            return this;
        }
    }
}
