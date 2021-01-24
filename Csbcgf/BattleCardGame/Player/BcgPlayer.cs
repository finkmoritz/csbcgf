using System;
using csbcgf.BattleCardGame;
using Csbcgf.Core;

namespace Csbcgf.BattleCardGame
{
    public class BcgPlayer : Player, IBcgPlayer
    {
        public BcgPlayer()
        {
        }

        public void SummonMonster(IBcgGame game, IBcgMonsterCard monsterCard)
        {
            if (!monsterCard.IsSummonable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new BcgSummonMonsterAction(this, monsterCard));
        }

        public void CastSpell(IBcgGame game, IBcgTargetlessSpellCard spellCard)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new BcgCastTargetlessSpellAction(this, spellCard));
        }

        public void CastSpell(IBcgGame game, IBcgTargetfulSpellCard spellCard, IBcgCharacter target)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new BcgCastTargetfulSpellAction(this, spellCard, target));
        }

        public void DrawCard(IBcgGame game)
        {
            game.Execute(new BcgDrawCardAction(this));
        }
    }
}
