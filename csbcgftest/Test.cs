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

            Assert.AreEqual(4, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(4, game.NonActivePlayer.Deck.Size);
            Assert.AreEqual(1, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(1, game.NonActivePlayer.Hand.Size);

            ICard goblin = game.ActivePlayer.Hand.Get(0);
            Assert.AreEqual(2, goblin.ManaStat.Value);
            Assert.AreEqual(1, ((IMonsterCard)goblin).AttackStat.Value);
            Assert.AreEqual(2, ((IMonsterCard)goblin).LifeStat.Value);

            Assert.False(goblin.IsPlayable(game));
        }

        [Test()]
        public void TestGame()
        {
            ICard goblin = game.ActivePlayer.Hand.Get(0);
            Assert.False(goblin.IsPlayable(game));

            game.EndTurn(); //End of first player's turn

            Assert.AreEqual(1, game.ActivePlayer.ManaStat.Value);
            Assert.AreEqual(1, game.NonActivePlayer.ManaStat.Value);

            Assert.AreEqual(3, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(4, game.NonActivePlayer.Deck.Size);
            Assert.AreEqual(2, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(1, game.NonActivePlayer.Hand.Size);

            game.EndTurn(); //End of second player's turn

            Assert.AreEqual(2, game.ActivePlayer.ManaStat.Value);
            Assert.AreEqual(1, game.NonActivePlayer.ManaStat.Value);

            Assert.AreEqual(3, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(3, game.NonActivePlayer.Deck.Size);
            Assert.AreEqual(2, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(2, game.NonActivePlayer.Hand.Size);

            goblin = game.ActivePlayer.Hand.Get(0);
            Assert.True(goblin.IsPlayable(game));

            Assert.True(game.ActivePlayer.Board.IsFreeSlot(0));
            game.ActivePlayer.PlayMonster(game, (IMonsterCard)goblin, 0);
            Assert.False(game.ActivePlayer.Board.IsFreeSlot(0));
        }
    }
}
