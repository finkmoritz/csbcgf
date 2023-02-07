using csbcgf;

namespace hearthstone
{
    public class HearthstonePlayer : Player
    {
        protected HearthstonePlayer() { }

        public HearthstonePlayer(bool _ = true) : base(_)
        {
            AddReaction(new EndGameOnModifyLifeStatActionReaction(this));

            AddCardCollection(CardCollectionKeys.Deck, new CardCollection());
            AddCardCollection(CardCollectionKeys.Hand, new CardCollection());
            AddCardCollection(CardCollectionKeys.Board, new CardCollection());
            AddCardCollection(CardCollectionKeys.Graveyard, new CardCollection());
        }

        public void DrawCard(IGame game)
        {
            game.ActionQueue.Execute(new DrawCardAction(this));
        }

        public void SummonMonster(IGame game, IMonsterCard monsterCard)
        {
            if (!monsterCard.IsSummonable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.ActionQueue.Execute(new SummonMonsterAction(this, monsterCard));
        }

        public void CastSpell(IGame game, ITargetlessSpellCard spellCard)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.ActionQueue.Execute(new CastTargetlessSpellAction(this, spellCard));
        }

        public void CastSpell(IGame game, ITargetfulSpellCard spellCard, ICharacter target)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.ActionQueue.Execute(new CastTargetfulSpellAction(this, spellCard, target));
        }
    }
}
