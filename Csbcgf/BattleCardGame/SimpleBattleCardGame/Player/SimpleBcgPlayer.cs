using System;
using System.Collections.Generic;
using Csbcgf.BattleCardGame;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame.SimpleBattleCardGame
{
    [Serializable]
    public class SimpleBcgPlayer : BcgPlayer
    {
        [JsonIgnore]
        public const string CardCollectionKeyDeck = "CardCollectionKeyDeck";

        [JsonIgnore]
        public const string CardCollectionKeyHand = "CardCollectionKeyHand";

        [JsonIgnore]
        public const string CardCollectionKeyBoard = "CardCollectionKeyBoard";

        [JsonIgnore]
        public const string CardCollectionKeyGraveyard = "CardCollectionKeyGraveyard";

        public SimpleBcgPlayer(int mana, int attack, int life)
            : this(
                  new BcgManaPoolStat(mana, mana),
                  new BcgAttackStat(attack),
                  new BcgLifeStat(life))
        {
        }

        public SimpleBcgPlayer(
            BcgManaPoolStat manaPoolStat,
            BcgAttackStat attackStat,
            BcgLifeStat lifeStat
            ) : this(
                manaPoolStat,
                attackStat,
                lifeStat,
                new List<IReaction>(),
                new Dictionary<string, ICardCollection>())
        {
            CardCollections.Add(CardCollectionKeyDeck, new BcgCardCollection());
            CardCollections.Add(CardCollectionKeyHand, new BcgCardCollection());
            CardCollections.Add(CardCollectionKeyBoard, new BcgCardCollection());
            CardCollections.Add(CardCollectionKeyGraveyard, new BcgCardCollection());
        }

        [JsonConstructor]
        public SimpleBcgPlayer(
            BcgManaPoolStat manaPoolStat,
            BcgAttackStat attackStat,
            BcgLifeStat lifeStat,
            List<IReaction> reactions,
            Dictionary<string, ICardCollection> cardCollections
            ) : base(manaPoolStat, attackStat, lifeStat, reactions, cardCollections)
        {
        }

        public void SummonMonster(IBcgGame game, IBcgMonsterCard monsterCard)
        {
            if (!monsterCard.IsSummonable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new BcgSummonMonsterAction(
                monsterCard,
                CardCollections[CardCollectionKeyHand],
                CardCollections[CardCollectionKeyBoard]
            ));
        }

        public void CastSpell(IBcgGame game, IBcgTargetlessSpellCard spellCard)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new BcgCastTargetlessSpellAction(
                spellCard,
                CardCollections[CardCollectionKeyHand],
                CardCollections[CardCollectionKeyGraveyard]
            ));
        }

        public void CastSpell(IBcgGame game, IBcgTargetfulSpellCard spellCard, IBcgCharacter target)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new BcgCastTargetfulSpellAction(
                spellCard,
                CardCollections[CardCollectionKeyHand],
                CardCollections[CardCollectionKeyGraveyard],
                target
            ));
        }

        public void DrawCard(IBcgGame game)
        {
            game.Execute(new DrawCardAction(this));
        }

        public override HashSet<IBcgCharacter> GetPotentialTargets(IBcgGameState gameState)
        {
            return new HashSet<IBcgCharacter>();
        }
    }
}
