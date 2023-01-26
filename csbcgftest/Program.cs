using NUnit.Framework;
using csbcgf;

namespace csbcgftest
{
    [TestFixture()]
    public class Test
    {
        private IGame game = null!;

        [SetUp()]
        public void SetUp()
        {
            List<IPlayer> players = new List<IPlayer>();
            for (int i=0; i<2; ++i)
            {
                IDeck deck = new Deck();

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

                players.Add(new Player(deck));
            }

            game = new Game(players);
            game.StartGame(initialHandSize: 1, initialPlayerLife: 2);
        }

        [Test()]
        public void TestInitialConditions()
        {
            Assert.AreEqual(1, game.ActivePlayer.ManaValue);
            Assert.AreEqual(0, game.NonActivePlayers[0].ManaValue);
            Assert.AreEqual(1, game.ActivePlayer.ManaBaseValue);
            Assert.AreEqual(0, game.NonActivePlayers[0].ManaBaseValue);

            Assert.AreEqual(3, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(4, game.NonActivePlayers[0].Deck.Size);
            Assert.AreEqual(2, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(1, game.NonActivePlayers[0].Hand.Size);

            IMonsterCard goblin = (IMonsterCard)game.ActivePlayer.Hand[0];
            Assert.AreEqual(2, goblin.ManaValue);
            Assert.AreEqual(1, goblin.AttackValue);
            Assert.AreEqual(2, goblin.LifeValue);

            Assert.False(goblin.IsSummonable(game));
        }

        [Test()]
        public void TestGame()
        {
            IMonsterCard goblin = (IMonsterCard)game.ActivePlayer.Hand[0];
            Assert.False(goblin.IsSummonable(game));

            game.NextTurn(); //Second player's turn

            Assert.AreEqual(1, game.ActivePlayer.ManaValue);
            Assert.AreEqual(1, game.NonActivePlayers[0].ManaValue);
            Assert.AreEqual(1, game.ActivePlayer.ManaBaseValue);
            Assert.AreEqual(1, game.NonActivePlayers[0].ManaBaseValue);

            Assert.AreEqual(3, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(3, game.NonActivePlayers[0].Deck.Size);
            Assert.AreEqual(2, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(2, game.NonActivePlayers[0].Hand.Size);

            game.NextTurn(); //First player's turn again

            Assert.AreEqual(2, game.ActivePlayer.ManaValue);
            Assert.AreEqual(1, game.NonActivePlayers[0].ManaValue);
            Assert.AreEqual(2, game.ActivePlayer.ManaBaseValue);
            Assert.AreEqual(1, game.NonActivePlayers[0].ManaBaseValue);

            Assert.AreEqual(2, game.ActivePlayer.Deck.Size);
            Assert.AreEqual(3, game.NonActivePlayers[0].Deck.Size);
            Assert.AreEqual(3, game.ActivePlayer.Hand.Size);
            Assert.AreEqual(2, game.NonActivePlayers[0].Hand.Size);

            goblin = (IMonsterCard)game.ActivePlayer.Hand[0];
            Assert.True(goblin.IsSummonable(game));

            //Play monster card
            Assert.True(game.ActivePlayer.Board.IsFreeSlot(0));
            game.ActivePlayer.CastMonster(game, goblin, 0);
            Assert.False(game.ActivePlayer.Board.IsFreeSlot(0));
            Assert.AreEqual(1, game.ActivePlayer.Board.AllCards.Count);

            Assert.AreEqual(0, game.ActivePlayer.ManaValue);
            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //Second player's turn again

            Assert.AreEqual(2, game.ActivePlayer.ManaValue);
            Assert.AreEqual(0, game.NonActivePlayers[0].ManaValue);
            Assert.AreEqual(2, game.ActivePlayer.ManaBaseValue);
            Assert.AreEqual(2, game.NonActivePlayers[0].ManaBaseValue);

            IMonsterCard otherGoblin = (IMonsterCard)game.ActivePlayer.Hand[0];
            game.ActivePlayer.CastMonster(game, otherGoblin, 0);

            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //First player's turn again

            Assert.AreEqual(3, game.ActivePlayer.ManaValue);
            Assert.AreEqual(0, game.NonActivePlayers[0].ManaValue);
            Assert.AreEqual(3, game.ActivePlayer.ManaBaseValue);
            Assert.AreEqual(2, game.NonActivePlayers[0].ManaBaseValue);

            //Attack player
            Assert.True(goblin.IsReadyToAttack);
            Assert.AreEqual(2, game.NonActivePlayers[0].LifeValue);
            goblin.Attack(game, game.NonActivePlayers[0]);
            Assert.AreEqual(1, game.NonActivePlayers[0].LifeValue);
            Assert.True(game.NonActivePlayers[0].IsAlive);
            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //Second player's turn again

            Assert.AreEqual(2, goblin.LifeValue);
            otherGoblin.Attack(game, goblin);
            Assert.AreEqual(1, goblin.LifeValue);
            Assert.AreEqual(1, otherGoblin.LifeValue);

            game.NextTurn(); //First player's turn again

            //Both monster cards die in battle
            Assert.True(game.ActivePlayer.Graveyard.IsEmpty);
            Assert.True(game.NonActivePlayers[0].Graveyard.IsEmpty);
            Assert.False(game.ActivePlayer.Board.IsEmpty);
            Assert.False(game.NonActivePlayers[0].Board.IsEmpty);
            goblin.Attack(game, otherGoblin);
            Assert.AreEqual(1, game.ActivePlayer.Graveyard.Size);
            Assert.AreEqual(1, game.NonActivePlayers[0].Graveyard.Size);
            Assert.True(game.ActivePlayer.Board.IsEmpty);
            Assert.True(game.NonActivePlayers[0].Board.IsEmpty);

            //Destroy second player
            Assert.AreEqual(1, game.NonActivePlayers[0].LifeValue);
            Assert.True(game.NonActivePlayers[0].IsAlive);

            ITargetfulSpellCard fireball = (ITargetfulSpellCard)game.ActivePlayer.Hand[2];
            game.ActivePlayer.CastSpell(game, fireball, game.NonActivePlayers[0]);

            Assert.AreEqual(0, game.NonActivePlayers[0].LifeValue);
            Assert.False(game.NonActivePlayers[0].IsAlive);
        }

        [Test()]
        public void TestGameSerialization()
        {
            string serializedGame = JsonSerializer.ToJson(game);
            IGame? gameCopy = JsonSerializer.FromJson<Game>(serializedGame);
            string serializedGameCopy = JsonSerializer.ToJson(gameCopy);
            Assert.AreEqual(serializedGame, serializedGameCopy, "Expected:\n{0}\n\nActual:\n{1}\n", new object[] {serializedGame, serializedGameCopy});

            Assert.AreEqual(2, gameCopy!.Players.Count);
            Assert.AreEqual(5, gameCopy!.ActivePlayer.AllCards.Count);
        }
    }
}
