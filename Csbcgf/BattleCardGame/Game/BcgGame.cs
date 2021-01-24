using System;
using System.Collections.Generic;
using csbcgf.BattleCardGame;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgGame : Game, IBcgGame
    {
        public BcgGame()
            : this(new List<IPlayer>())
        {
        }

        public BcgGame(List<IPlayer> players)
            : this(players, new Random().Next(players.Count), new ActionQueue(false), new List<IReaction>())
        {
            Reactions.Add(new BcgModifyActivePlayerOnEndOfTurnEventReaction());
            Reactions.Add(new BcgModifyManaOnStartOfTurnEventReaction());
            Reactions.Add(new BcgDrawCardOnStartOfTurnEventReaction());
        }

        [JsonConstructor]
        public BcgGame(
            List<IPlayer> players,
            int activePlayerIndex,
            ActionQueue actionQueue,
            List<IReaction> reactions
            ) : base(players, activePlayerIndex, actionQueue, reactions)
        {
        }

        public override void StartGame(int initialHandSize = 4, int initialPlayerLife = 30)
        {
            //Do not trigger any reactions during setup
            actionQueue.ExecuteReactions = false;

            foreach (IPlayer player in Players)
            {
                player.ManaValue = 0;
                player.ManaBaseValue = 0;
                player.LifeValue = initialPlayerLife;
                player.LifeBaseValue = initialPlayerLife;

                for (int i = 0; i < initialHandSize; ++i)
                {
                    player.DrawCard(this);
                }
            }

            actionQueue.ExecuteReactions = true;

            base.StartGame();
        }
    }
}
