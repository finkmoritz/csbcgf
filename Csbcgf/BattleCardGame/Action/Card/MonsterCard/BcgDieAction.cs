using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgDieAction : Core.Action
    {
        [JsonProperty]
        public IBcgMonsterCard MonsterCard;

        [JsonConstructor]
        public BcgDieAction(IBcgMonsterCard monsterCard, bool isAborted = false)
            : base(isAborted)
        {
            MonsterCard = monsterCard;
        }

        public override object Clone()
        {
            return new BcgDieAction((IBcgMonsterCard)MonsterCard.Clone(), IsAborted);
        }

        public override void Execute(IGame game)
        {
            IPlayer owner = MonsterCard.FindParentPlayer(game);
            //game.Execute(new RemoveCardFromBoardAction(owner.Board, MonsterCard)); //TODO
            //game.Execute(new AddCardToGraveyardAction(owner.Graveyard, MonsterCard)); //TODO
        }

        public override bool IsExecutable(IGameState gameState)
        {
            IPlayer owner = MonsterCard.FindParentPlayer(gameState);
            return owner != null;
                //&& owner.Board.Contains(MonsterCard); //TODO
        }
    }
}
