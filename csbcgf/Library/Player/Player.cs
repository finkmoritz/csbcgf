﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class Player : IPlayer
    {
        [JsonProperty]
        protected ManaPoolStat manaPoolStat;

        [JsonProperty]
        protected AttackStat attackStat;

        [JsonProperty]
        protected LifeStat lifeStat;

        public List<IReaction> Reactions { get; }

        public IDeck Deck { get; protected set; }
        public IHand Hand { get; protected set; }
        public IBoard Board { get; protected set; }
        public IDeck Graveyard { get; protected set; }

        /// <summary>
        /// Represents a Player and all his/her associated Cards.
        /// </summary>
        public Player() : this(new Deck())
        {
        }

        /// <summary>
        /// Represents a Player and all his/her associated Cards.
        /// </summary>
        /// <param name="deck"></param>
        public Player(IDeck deck)
            : this(deck, new Hand(), new Board(), new Deck(),
                  new ManaPoolStat(0, 0), new AttackStat(0), new LifeStat(0),
                  new List<IReaction>())
        {
        }

        [JsonConstructor]
        protected Player(IDeck deck, IHand hand, IBoard board, IDeck graveyard,
            ManaPoolStat manaPoolStat, AttackStat attackStat, LifeStat lifeStat,
            List<IReaction> reactions)
        {
            Deck = deck;
            Hand = hand;
            Board = board;
            Graveyard = graveyard;

            this.manaPoolStat = manaPoolStat;
            this.attackStat = attackStat;
            this.lifeStat = lifeStat;
            Reactions = reactions;
        }

        [JsonIgnore]
        public bool IsAlive => lifeStat.Value > 0;

        [JsonIgnore]
        public List<ICard> AllCards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                allCards.AddRange(Deck.AllCards);
                allCards.AddRange(Hand.AllCards);
                allCards.AddRange(Board.AllCards);
                allCards.AddRange(Graveyard.AllCards);
                return allCards;
            }
        }

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

        [JsonIgnore]
        public List<ICharacter> Characters
        {
            get
            {
                List<ICharacter> characters = new List<ICharacter>
                {
                    this
                };
                Board.AllCards.ForEach(c => characters.Add((ICharacter)c));
                return characters;
            }
        }

        public List<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            AllCards.ForEach(c => allReactions.AddRange(c.AllReactions()));
            return allReactions;
        }

        public void DrawCard(IGame game)
        {
            game.Execute(new DrawCardAction(this));
        }

        public void CastMonster(IGame game, IMonsterCard monsterCard, int boardIndex)
        {
            if (!monsterCard.IsSummonable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            if (!Board.IsFreeSlot(boardIndex))
            {
                throw new CsbcgfException("Slot with index " + boardIndex +
                    " is already occupied!");
            }

            game.Execute(new CastMonsterAction(this, monsterCard, boardIndex));
        }

        public void CastSpell(IGame game, ITargetlessSpellCard spellCard)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new CastTargetlessSpellAction(this, spellCard));
        }

        public void CastSpell(IGame game, ITargetfulSpellCard spellCard, ICharacter target)
        {
            if (!spellCard.IsCastable(game))
            {
                throw new CsbcgfException("Tried to play a card that is " +
                    "not playable!");
            }

            game.Execute(new CastTargetfulSpellAction(this, spellCard, target));
        }

        public HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            return new HashSet<ICharacter>();
        }

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }

        public virtual object Clone()
        {
            List<IReaction> reactionsClone = new List<IReaction>();
            foreach (IReaction reaction in Reactions)
            {
                reactionsClone.Add((IReaction)reaction.Clone());
            }

            return new Player(
                (IDeck)Deck.Clone(),
                (IHand)Hand.Clone(),
                (IBoard)Board.Clone(),
                (IDeck)Graveyard.Clone(),
                (ManaPoolStat)manaPoolStat.Clone(),
                (AttackStat)attackStat.Clone(),
                (LifeStat)lifeStat.Clone(),
                reactionsClone
            );
        }

        public ICard FindParentCard(IGameState gameState)
        {
            throw new CsbcgfException("Cannot use method 'FindParentCard' on " +
                "instance of type 'Player'");
        }

        public IPlayer FindParentPlayer(IGameState gameState)
        {
            return this;
        }
    }
}
