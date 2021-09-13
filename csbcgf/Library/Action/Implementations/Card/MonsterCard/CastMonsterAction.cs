using System;

namespace csbcgf
{
    public class CastMonsterAction : Action
    {
        public IPlayer Player;

        public IMonsterCard MonsterCard;

        public int BoardIndex;

        public CastMonsterAction(IPlayer player, IMonsterCard monsterCard,
            int boardIndex, bool isAborted = false
            ) : base(isAborted)
        {
            Player = player;
            MonsterCard = monsterCard;
            BoardIndex = boardIndex;
        }

        public override object Clone()
        {
            return new CastMonsterAction(
                null, // otherwise circular dependencies
                (IMonsterCard)MonsterCard.Clone(),
                BoardIndex,
                IsAborted
                );
        }

        public override void Execute(IGame game)
        {
            game.Execute(new ModifyManaStatAction(Player, -MonsterCard.ManaValue, 0));
            game.Execute(new RemoveCardFromHandAction(Player.Hand, MonsterCard));
            game.Execute(new AddCardToBoardAction(Player.Board, MonsterCard, BoardIndex));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.IsSummonable(gameState)
                && Player.Board.IsFreeSlot(BoardIndex);
        }
    }
}
