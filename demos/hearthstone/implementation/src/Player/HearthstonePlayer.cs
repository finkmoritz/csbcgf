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

        public void DrawCard(HearthstoneGame game)
        {
            game.Execute(new DrawCardAction(this));
        }

        public void SummonMonster(HearthstoneGame game, HearthstoneMonsterCard monsterCard)
        {
            if (!monsterCard.IsSummonable(game.State))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new SummonMonsterAction(this, monsterCard));
        }

        public void CastSpell(HearthstoneGame game, HearthstoneTargetlessSpellCard spellCard)
        {
            if (!spellCard.IsCastable(game.State))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new CastTargetlessSpellAction(this, spellCard));
        }

        public void CastSpell(HearthstoneGame game, HearthstoneTargetfulSpellCard spellCard, IStatContainer target)
        {
            if (!spellCard.IsCastable(game.State))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new CastTargetfulSpellAction(this, spellCard, target));
        }
    }
}
