using System;

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

        /// <summary>
        /// Represent the current Game state and provides methods to alter
        /// this Game state.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="options"></param>
        public Game(Player[] players, GameOptions options = null)
        {
            this.Players = players;
            this.ActivePlayerIndex = new Random().Next(this.Players.Length);
            this.Options = options;

            if(Options == null)
            {
                Options = new GameOptions();
            }

            Init();
        }

        public Player ActivePlayer => Players[ActivePlayerIndex];

        protected void Init()
        {
            foreach (Player player in Players)
            {
                for (int i=0; i<Options.StartHandSize; ++i)
                {
                    player.DrawCard();
                }
            }
        }

        public void EndTurn()
        {
            ActivePlayerIndex = (ActivePlayerIndex + 1) % Players.Length;
            ActivePlayer.ManaStat.Value++;
            ActivePlayer.DrawCard();
        }

        public void Attack(IMonsterCard monsterCard, ICharacter targetCharacter)
        {
            ActivePlayer.Attack(this, monsterCard, targetCharacter);
        }

        public void PlayMonster(IMonsterCard monsterCard, int boardIndex)
        {
            ActivePlayer.PlayMonster(this, monsterCard, boardIndex);
        }

        public void PlaySpell(ITargetlessSpellCard spellCard)
        {
            ActivePlayer.PlaySpell(this, spellCard);
        }

        public void PlaySpell(ITargetfulSpellCard spellCard, ICharacter targetCharacter)
        {
            ActivePlayer.PlaySpell(this, spellCard, targetCharacter);
        }
    }
}
