using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame.SimpleBattleCardGame
{
    [Serializable]
    public class ModifyLifeStatReaction : Reaction
    {
        [JsonConstructor]
        public ModifyLifeStatReaction()
        {
        }

        public override object Clone()
        {
            return new ModifyLifeStatReaction();
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if(actionEvent.IsAfter(typeof(BcgModifyLifeStatAction)))
            {
                BcgModifyLifeStatAction action = (BcgModifyLifeStatAction)actionEvent.Action;
                if (action.Living.LifeValue <= 0)
                {
                    if (action.Living is IBcgMonsterCard monsterCard)
                    {
                        game.Execute(new BcgDieAction(monsterCard));
                    }
                    else if (action.Living is IBcgPlayer)
                    {
                        game.Execute(new EndOfGameEvent());
                    }
                }
            }
        }
    }
}
