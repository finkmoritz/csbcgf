﻿using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class CastMonsterAction : IAction
    {
        [JsonProperty]
        public IPlayer Player;

        [JsonProperty]
        public IMonsterCard MonsterCard;

        [JsonProperty]
        public int BoardIndex;

        public CastMonsterAction(IPlayer player, IMonsterCard monsterCard, int boardIndex)
        {
            Player = player;
            MonsterCard = monsterCard;
            BoardIndex = boardIndex;
        }

        public void Execute(IGame game)
        {
            game.Execute(new ModifyManaStatAction(Player, -MonsterCard.ManaValue, 0));
            game.Execute(new RemoveCardFromHandAction(Player.Hand, MonsterCard));
            game.Execute(new AddCardToBoardAction(Player.Board, MonsterCard, BoardIndex));
        }

        public bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.IsCastable(gameState)
                && Player.Board.IsFreeSlot(BoardIndex);
        }
    }
}