using System;
using System.Linq;

namespace csbcgf
{
    public class ModifyActivePlayerAction : IAction
    {
        public readonly IPlayer NewActivePlayer;

        public ModifyActivePlayerAction(IPlayer newActivePlayer)
        {
            this.NewActivePlayer = newActivePlayer;
        }

        public void Execute(IGame game)
        {
            game.ActivePlayer = NewActivePlayer;
        }

        public bool IsExecutable(IGame game)
        {
            if(!game.Players.ToList().Contains(NewActivePlayer))
            {
                throw new CsbcgfException("Could not change the active " +
                    "player because the specified player is not involved " +
                    "in the game!");
            }
            return NewActivePlayer != game.ActivePlayer;
        }
    }
}
