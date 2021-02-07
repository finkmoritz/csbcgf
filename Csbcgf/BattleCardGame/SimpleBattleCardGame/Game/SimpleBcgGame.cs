using System;
using System.Collections.Generic;
using System.Linq;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame.SimpleBattleCardGame
{
    [Serializable]
    public class SimpleBcgGame : BcgGame
    {
        [JsonProperty]
        protected int activePlayerIndex;

        public SimpleBcgGame()
            : this(new List<IBcgPlayer>(), new ActionQueue(), new List<IReaction>(), 0)
        {
            Reactions.Add(new BcgModifyActivePlayerOnEndOfTurnEventReaction());
            Reactions.Add(new BcgModifyManaOnStartOfTurnEventReaction());
            Reactions.Add(new BcgDrawCardOnStartOfTurnEventReaction());
            Reactions.Add(new ModifyLifeStatReaction());
        }

        [JsonConstructor]
        public SimpleBcgGame(
            List<IBcgPlayer> players,
            ActionQueue actionQueue,
            List<IReaction> reactions,
            int activePlayerIndex
            ) : base(players, actionQueue, reactions)
        {
            this.activePlayerIndex = activePlayerIndex;
        }

        [JsonIgnore]
        public IPlayer ActivePlayer
        {
            get => Players[activePlayerIndex];
            set
            {
                activePlayerIndex = Players.IndexOf(value);
            }
        }

        [JsonIgnore]
        public List<IPlayer> NonActivePlayers
        {
            get
            {
                return Players.Where(p => p != ActivePlayer).ToList();
            }
        }

        public override void StartGame()
        {
            //Do not trigger any reactions during setup
            actionQueue.ExecuteReactions = false;

            foreach (SimpleBcgPlayer player in Players)
            {
                player.ManaValue = 0;
                player.ManaBaseValue = 0;
                player.LifeValue = 30;
                player.LifeBaseValue = 30;

                for (int i = 0; i < 4; ++i)
                {
                    player.DrawCard(this);
                }
            }

            actionQueue.ExecuteReactions = true;

            activePlayerIndex = new Random().Next(Players.Count);

            base.StartGame();
        }
    }
}
