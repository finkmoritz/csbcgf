using csbcgf;
using csccgltest;
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

                for (int j=0; j<3; ++j)
                {
                    ICard fireball = new DamageSpellCard(3);
                    deck.Push(fireball);
                }

                for (int j=0; j<2; ++j)
                {
                    ICard goblin = new MonsterCard(2, 1, 2);
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
            Assert.AreEqual(1, game.ActivePlayer.ManaValue);
            Assert.AreEqual(0, game.NonActivePlayer.ManaValue);
            Assert.AreEqual(1, game.ActivePlayer.ManaBaseValue);
            Assert.AreEqual(0, game.NonActivePlayer.ManaBaseValue);

            Assert.AreEqual(3, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(4, game.NonActivePlayer.Deck.Size);
            Assert.AreEqual(2, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(1, game.NonActivePlayer.Hand.Size);

            IMonsterCard goblin = (IMonsterCard)game.ActivePlayer.Hand[0];
            Assert.AreEqual(2, goblin.ManaValue);
            Assert.AreEqual(1, goblin.AttackValue);
            Assert.AreEqual(2, goblin.LifeValue);

            Assert.False(goblin.IsPlayable(game));
        }

        [Test()]
        public void TestGame()
        {
            IMonsterCard goblin = (IMonsterCard)game.ActivePlayer.Hand[0];
            Assert.False(goblin.IsPlayable(game));

            game.NextTurn(); //Second player's turn

            Assert.AreEqual(1, game.ActivePlayer.ManaValue);
            Assert.AreEqual(1, game.NonActivePlayer.ManaValue);
            Assert.AreEqual(1, game.ActivePlayer.ManaBaseValue);
            Assert.AreEqual(1, game.NonActivePlayer.ManaBaseValue);

            Assert.AreEqual(3, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(3, game.NonActivePlayer.Deck.Size);
            Assert.AreEqual(2, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(2, game.NonActivePlayer.Hand.Size);

            game.NextTurn(); //First player's turn again

            Assert.AreEqual(2, game.ActivePlayer.ManaValue);
            Assert.AreEqual(1, game.NonActivePlayer.ManaValue);
            Assert.AreEqual(2, game.ActivePlayer.ManaBaseValue);
            Assert.AreEqual(1, game.NonActivePlayer.ManaBaseValue);

            Assert.AreEqual(2, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(3, game.NonActivePlayer.Deck.Size);
            Assert.AreEqual(3, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(2, game.NonActivePlayer.Hand.Size);

            goblin = (IMonsterCard)game.ActivePlayer.Hand[0];
            Assert.True(goblin.IsPlayable(game));

            //Play monster card
            Assert.True(game.ActivePlayer.Board.IsFreeSlot(0));
            game.ActivePlayer.PlayMonster(game, goblin, 0);
            Assert.False(game.ActivePlayer.Board.IsFreeSlot(0));
            Assert.AreEqual(1, game.ActivePlayer.Board.AllCards.Count);

            Assert.AreEqual(0, game.ActivePlayer.ManaValue);
            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //Second player's turn again

            Assert.AreEqual(2, game.ActivePlayer.ManaValue);
            Assert.AreEqual(0, game.NonActivePlayer.ManaValue);
            Assert.AreEqual(2, game.ActivePlayer.ManaBaseValue);
            Assert.AreEqual(2, game.NonActivePlayer.ManaBaseValue);

            IMonsterCard otherGoblin = (IMonsterCard)game.ActivePlayer.Hand[0];
            game.ActivePlayer.PlayMonster(game, otherGoblin, 0);

            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //First player's turn again

            Assert.AreEqual(3, game.ActivePlayer.ManaValue);
            Assert.AreEqual(0, game.NonActivePlayer.ManaValue);
            Assert.AreEqual(3, game.ActivePlayer.ManaBaseValue);
            Assert.AreEqual(2, game.NonActivePlayer.ManaBaseValue);

            //Attack player
            Assert.True(goblin.IsReadyToAttack);
            Assert.AreEqual(3, game.NonActivePlayer.LifeValue);
            goblin.Attack(game, game.NonActivePlayer);
            Assert.AreEqual(2, game.NonActivePlayer.LifeValue);
            Assert.True(game.NonActivePlayer.IsAlive);
            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //Second player's turn again

            Assert.AreEqual(2, goblin.LifeValue);
            otherGoblin.Attack(game, goblin);
            Assert.AreEqual(1, goblin.LifeValue);
            Assert.AreEqual(1, otherGoblin.LifeValue);

            //Destroy opposing player
            Assert.AreEqual(3, game.NonActivePlayer.LifeValue);
            Assert.True(game.NonActivePlayer.IsAlive);

            ITargetfulSpellCard fireball = (ITargetfulSpellCard)game.ActivePlayer.Hand[2];
            game.ActivePlayer.PlaySpell(game, fireball, game.NonActivePlayer);

            Assert.AreEqual(0, game.NonActivePlayer.LifeValue);
            Assert.False(game.NonActivePlayer.IsAlive);
        }
    }
}
