﻿using System;

namespace csccgl
{
    [Serializable]
    public class Game : IGame
    {
        public Player[] Players { get; }

        /// <summary>
        /// Index of the active Player. Refers to the Players array.
        /// Also see the ActivePlayer accessor.
        /// </summary>
        public int ActivePlayerIndex { get; protected set; }

        /// <summary>
        /// Additional GameOptions that help customizing a Game.
        /// </summary>
        public readonly GameOptions Options;

        protected ActionQueue actions = new ActionQueue();

        /// <summary>
        /// Represent the current Game state and provides methods to alter
        /// this Game state.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="options"></param>
        public Game(Player[] players, GameOptions options = null)
        {
            if(players.Length != 2)
            {
                throw new CsccglException("Parameter 'players' must feature exactly two Player entries!");
            }

            this.Players = players;
            this.ActivePlayerIndex = new Random().Next(this.Players.Length);
            this.Options = options;

            if(Options == null)
            {
                Options = new GameOptions();
            }

            Init(Options);

            actions.Process(this);
        }

        public Player ActivePlayer => Players[ActivePlayerIndex];

        public Player NonActivePlayer => Players[1 - ActivePlayerIndex];

        protected void Init(GameOptions options)
        {
            foreach (Player player in Players)
            {
                player.ManaStat.Value = 0;
                player.LifeStat.Value = options.InitialPlayerLife;
                for (int i=0; i<Options.InitialHandSize; ++i)
                {
                    player.DrawCard(this);
                }
            }
            ActivePlayer.ManaStat.Value = 1;
        }

        public void EndTurn()
        {
            ActivePlayerIndex = (ActivePlayerIndex + 1) % Players.Length;
            Queue(new ModifyManaStatAction(ActivePlayer.ManaStat, 1));
            ActivePlayer.DrawCard(this);

            actions.Process(this);
        }

        public void Attack(IMonsterCard monsterCard, ICharacter targetCharacter)
        {
            ActivePlayer.Attack(this, monsterCard, targetCharacter);
            actions.Process(this);
        }

        public void PlayMonster(IMonsterCard monsterCard, int boardIndex)
        {
            ActivePlayer.PlayMonster(this, monsterCard, boardIndex);
            actions.Process(this);
        }

        public void PlaySpell(ITargetlessSpellCard spellCard)
        {
            ActivePlayer.PlaySpell(this, spellCard);
            actions.Process(this);
        }

        public void PlaySpell(ITargetfulSpellCard spellCard, ICharacter targetCharacter)
        {
            ActivePlayer.PlaySpell(this, spellCard, targetCharacter);
            actions.Process(this);
        }

        public void Queue(IAction action)
        {
            actions.Queue(action);
        }
    }
}
