using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgCardCollection : CardCollection, IBcgCardCollection
    {
        public BcgCardCollection()
            : this(new List<IBcgCard>())
        {
        }

        [JsonConstructor]
        public BcgCardCollection(List<IBcgCard> cards)
            : base(cards.ConvertAll(c => (ICard)c))
        {
        }
    }
}
