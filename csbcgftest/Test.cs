using csbcgf;
using NUnit.Framework;

namespace csbcgftest
{
    [TestFixture()]
    public class Test
    {
        private IGame game;

        [SetUp()]
        public void SetUp()
        {
            IPlayer[] players = new Player[2];
            for (int i=0; i<players.Length; ++i)
            {
                IStackedDeck deck = new StackedDeck();

                for (int j=0; j<5; ++j)
                {
                    CompoundMonsterCard goblin = new CompoundMonsterCard(2, 1, 2);
                    deck.Push(goblin);
                }

                players[i] = new Player(deck);
            }

            GameOptions gameOptions = new GameOptions
            {
                InitialHandSize = 1,
                InitialPlayerLife = 3
            };

            game = new Game(players, gameOptions);
        }

        [Test()]
        public void TestInitialConditions()
        {
            Assert.AreEqual(1, game.ActivePlayer.ManaStat.Value);
            Assert.AreEqual(0, game.NonActivePlayer.ManaStat.Value);
            Assert.AreEqual(1, game.ActivePlayer.ManaStat.MaxValue);
            Assert.AreEqual(0, game.NonActivePlayer.ManaStat.MaxValue);

            Assert.AreEqual(3, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(4, game.NonActivePlayer.Deck.Size);
            Assert.AreEqual(2, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(1, game.NonActivePlayer.Hand.Size);

            IMonsterCard goblin = (IMonsterCard)game.ActivePlayer.Hand.Get(0);
            Assert.AreEqual(2, goblin.ManaStat.Value);
            Assert.AreEqual(1, goblin.AttackStat.Value);
            Assert.AreEqual(2, goblin.LifeStat.Value);

            Assert.False(goblin.IsPlayable(game));
        }

        [Test()]
        public void TestGame()
        {
            IMonsterCard goblin = (IMonsterCard)game.ActivePlayer.Hand.Get(0);
            Assert.False(goblin.IsPlayable(game));

            game.NextTurn(); //Second player's turn

            Assert.AreEqual(1, game.ActivePlayer.ManaStat.Value);
            Assert.AreEqual(1, game.NonActivePlayer.ManaStat.Value);
            Assert.AreEqual(1, game.ActivePlayer.ManaStat.MaxValue);
            Assert.AreEqual(1, game.NonActivePlayer.ManaStat.MaxValue);

            Assert.AreEqual(3, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(3, game.NonActivePlayer.Deck.Size);
            Assert.AreEqual(2, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(2, game.NonActivePlayer.Hand.Size);

            game.NextTurn(); //First player's turn again

            Assert.AreEqual(2, game.ActivePlayer.ManaStat.Value);
            Assert.AreEqual(1, game.NonActivePlayer.ManaStat.Value);
            Assert.AreEqual(2, game.ActivePlayer.ManaStat.MaxValue);
            Assert.AreEqual(1, game.NonActivePlayer.ManaStat.MaxValue);

            Assert.AreEqual(2, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(3, game.NonActivePlayer.Deck.Size);
            Assert.AreEqual(3, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(2, game.NonActivePlayer.Hand.Size);

            goblin = (IMonsterCard)game.ActivePlayer.Hand.Get(0);
            Assert.True(goblin.IsPlayable(game));

            //Play monster card
            Assert.True(game.ActivePlayer.Board.IsFreeSlot(0));
            game.ActivePlayer.PlayMonster(game, goblin, 0);
            Assert.False(game.ActivePlayer.Board.IsFreeSlot(0));

            Assert.AreEqual(0, game.ActivePlayer.ManaStat.Value);
            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //Second player's turn again

            Assert.AreEqual(2, game.ActivePlayer.ManaStat.Value);
            Assert.AreEqual(0, game.NonActivePlayer.ManaStat.Value);
            Assert.AreEqual(2, game.ActivePlayer.ManaStat.MaxValue);
            Assert.AreEqual(2, game.NonActivePlayer.ManaStat.MaxValue);

            IMonsterCard otherGoblin = (IMonsterCard)game.ActivePlayer.Hand.Get(0);
            game.ActivePlayer.PlayMonster(game, otherGoblin, 0);

            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //First player's turn again

            Assert.AreEqual(3, game.ActivePlayer.ManaStat.Value);
            Assert.AreEqual(0, game.NonActivePlayer.ManaStat.Value);
            Assert.AreEqual(3, game.ActivePlayer.ManaStat.MaxValue);
            Assert.AreEqual(2, game.NonActivePlayer.ManaStat.MaxValue);

            //Attack player
            Assert.True(goblin.IsReadyToAttack);
            Assert.AreEqual(3, game.NonActivePlayer.LifeStat.Value);
            goblin.Attack(game, game.NonActivePlayer);
            Assert.AreEqual(2, game.NonActivePlayer.LifeStat.Value);
            Assert.True(game.NonActivePlayer.IsAlive);
            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //Second player's turn again
            Assert.AreEqual(2, goblin.LifeStat.Value);
            otherGoblin.Attack(game, goblin);
            Assert.AreEqual(1, goblin.LifeStat.Value);
            Assert.AreEqual(1, otherGoblin.LifeStat.Value);
        }
    }
}
