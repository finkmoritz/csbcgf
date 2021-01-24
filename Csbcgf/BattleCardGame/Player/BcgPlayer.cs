using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgPlayer : Player, IBcgPlayer
    {
        [JsonIgnore]
        public const string CardCollectionKeyDeck = "CardCollectionKeyDeck";

        [JsonIgnore]
        public const string CardCollectionKeyHand = "CardCollectionKeyHand";

        [JsonIgnore]
        public const string CardCollectionKeyBoard = "CardCollectionKeyBoard";

        [JsonIgnore]
        public const string CardCollectionKeyGraveyard = "CardCollectionKeyGraveyard";

        [JsonProperty]
        protected BcgManaPoolStat manaPoolStat;

        [JsonProperty]
        protected BcgAttackStat attackStat;

        [JsonProperty]
        protected BcgLifeStat lifeStat;

        public BcgPlayer()
            : this(0, 0, 0)
        {
        }

        public BcgPlayer(int mana, int attack, int life)
            : this(
                  new BcgManaPoolStat(mana, mana),
                  new BcgAttackStat(attack),
                  new BcgLifeStat(life))
        {
        }

        public BcgPlayer(
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
        public BcgPlayer(
            BcgManaPoolStat manaPoolStat,
            BcgAttackStat attackStat,
            BcgLifeStat lifeStat,
            List<IReaction> reactions,
            Dictionary<string, ICardCollection> cardCollections
            ) : base(reactions, cardCollections)
        {
            this.manaPoolStat = manaPoolStat;
            this.attackStat = attackStat;
            this.lifeStat = lifeStat;
        }

        [JsonIgnore]
        public bool IsAlive => lifeStat.Value > 0;

        [JsonIgnore]
        public int AttackValue
        {
            get => attackStat.Value;
            set => attackStat.Value = Math.Max(0, value);
        }

        [JsonIgnore]
        public int AttackBaseValue
        {
            get => attackStat.BaseValue;
            set => attackStat.BaseValue = Math.Max(0, value);
        }

        [JsonIgnore]
        public int LifeValue
        {
            get => lifeStat.Value;
            set => lifeStat.Value = Math.Max(0, value);
        }

        [JsonIgnore]
        public int LifeBaseValue
        {
            get => lifeStat.BaseValue;
            set => lifeStat.BaseValue = Math.Max(0, value);
        }

        [JsonIgnore]
        public int ManaValue
        {
            get => manaPoolStat.Value;
            set => manaPoolStat.Value = Math.Max(0, value);
        }

        [JsonIgnore]
        public int ManaBaseValue
        {
            get => manaPoolStat.BaseValue;
            set => manaPoolStat.BaseValue = Math.Max(0, value);
        }

        public HashSet<IBcgCharacter> GetPotentialTargets(IBcgGameState gameState)
        {
            return new HashSet<IBcgCharacter>();
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
            game.Execute(new BcgDrawCardAction(this));
        }
    }
}
