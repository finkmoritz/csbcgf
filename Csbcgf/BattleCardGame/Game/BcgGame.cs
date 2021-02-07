using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgGame : Game, IBcgGame
    {
        public BcgGame()
            : this(new List<IBcgPlayer>())
        {
        }

        public BcgGame(List<IBcgPlayer> players)
            : this(players, new ActionQueue(false), new List<IReaction>())
        {
        }

        [JsonConstructor]
        public BcgGame(
            List<IBcgPlayer> players,
            ActionQueue actionQueue,
            List<IReaction> reactions
            ) : base(players.ConvertAll(p => (IPlayer)p), actionQueue, reactions)
        {
        }
    }
}
