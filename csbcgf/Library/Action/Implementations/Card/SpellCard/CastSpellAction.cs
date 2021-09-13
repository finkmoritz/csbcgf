using System;

namespace csbcgf
{
    public abstract class CastSpellAction : Action
    {
        public IPlayer Player;

        public ISpellCard SpellCard;

        public CastSpellAction(IPlayer player, ISpellCard spellCard, bool isAborted = false)
            : base(isAborted)
        {
            Player = player;
            SpellCard = spellCard;
        }

        public override abstract void Execute(IGame game);

        public override abstract bool IsExecutable(IGameState gameState);
    }
}
