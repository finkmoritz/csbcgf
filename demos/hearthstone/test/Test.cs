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
            game = new Game();

            for (int i = 0; i < 2; ++i)
            {
                IPlayer player = new Player();
                player.LifeValue = 2;
                player.LifeBaseValue = 2;

                ICardCollection deck = player.GetCardCollection(CardCollectionKeys.Deck);

                for (int j = 0; j < 3; ++j)
                {
                    ICard fireball = new DamageSpellCard(3);
                    deck.Add(fireball);
                }

                for (int j = 0; j < 2; ++j)
                {
                    ICard goblin = new MonsterCard(2, 1, 2);
                    deck.Add(goblin);
                }

                game.AddPlayer(player);
            }

            foreach (IPlayer player in game.Players)
            {
                player.DrawCard(game);
            }

            game.Start();
        }

        [Test()]
        public void TestInitialConditions()
        {
            Assert.That(game.ActivePlayer.ManaValue, Is.EqualTo(1));
            Assert.That(game.NonActivePlayers.First().ManaValue, Is.EqualTo(0));
            Assert.That(game.ActivePlayer.ManaBaseValue, Is.EqualTo(1));
            Assert.That(game.NonActivePlayers.First().ManaBaseValue, Is.EqualTo(0));

            Assert.That(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(3));
            Assert.That(game.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(4));
            Assert.That(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(2));
            Assert.That(game.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(1));

            IMonsterCard goblin = (IMonsterCard)game.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[0];
            Assert.That(goblin.ManaValue, Is.EqualTo(2));
            Assert.That(goblin.AttackValue, Is.EqualTo(1));
            Assert.That(goblin.LifeValue, Is.EqualTo(2));

            Assert.False(goblin.IsSummonable(game));
        }

        [Test()]
        public void TestGame()
        {
            IMonsterCard goblin = (IMonsterCard)game.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[0];
            Assert.False(goblin.IsSummonable(game));

            game.NextTurn(); //Second player's turn

            Assert.That(game.ActivePlayer.ManaValue, Is.EqualTo(1));
            Assert.That(game.NonActivePlayers.First().ManaValue, Is.EqualTo(1));
            Assert.That(game.ActivePlayer.ManaBaseValue, Is.EqualTo(1));
            Assert.That(game.NonActivePlayers.First().ManaBaseValue, Is.EqualTo(1));

            Assert.That(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(3));
            Assert.That(game.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(3));
            Assert.That(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(2));
            Assert.That(game.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(2));

            game.NextTurn(); //First player's turn again

            Assert.That(game.ActivePlayer.ManaValue, Is.EqualTo(2));
            Assert.That(game.NonActivePlayers.First().ManaValue, Is.EqualTo(1));
            Assert.That(game.ActivePlayer.ManaBaseValue, Is.EqualTo(2));
            Assert.That(game.NonActivePlayers.First().ManaBaseValue, Is.EqualTo(1));

            Assert.That(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(2));
            Assert.That(game.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(3));
            Assert.That(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(3));
            Assert.That(game.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(2));

            goblin = (IMonsterCard)game.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[0];
            Assert.True(goblin.IsSummonable(game));

            //Play monster card
            Assert.True(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).IsEmpty);
            game.ActivePlayer.SummonMonster(game, goblin);
            Assert.False(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).IsEmpty);
            Assert.That(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).Cards.Count, Is.EqualTo(1));

            Assert.That(game.ActivePlayer.ManaValue, Is.EqualTo(0));
            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //Second player's turn again

            Assert.That(game.ActivePlayer.ManaValue, Is.EqualTo(2));
            Assert.That(game.NonActivePlayers.First().ManaValue, Is.EqualTo(0));
            Assert.That(game.ActivePlayer.ManaBaseValue, Is.EqualTo(2));
            Assert.That(game.NonActivePlayers.First().ManaBaseValue, Is.EqualTo(2));

            IMonsterCard otherGoblin = (IMonsterCard)game.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[0];
            game.ActivePlayer.SummonMonster(game, otherGoblin);

            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //First player's turn again

            Assert.That(game.ActivePlayer.ManaValue, Is.EqualTo(3));
            Assert.That(game.NonActivePlayers.First().ManaValue, Is.EqualTo(0));
            Assert.That(game.ActivePlayer.ManaBaseValue, Is.EqualTo(3));
            Assert.That(game.NonActivePlayers.First().ManaBaseValue, Is.EqualTo(2));

            //Attack player
            Assert.True(goblin.IsReadyToAttack);
            Assert.That(game.NonActivePlayers.First().LifeValue, Is.EqualTo(2));
            goblin.Attack(game, game.NonActivePlayers.First());
            Assert.That(game.NonActivePlayers.First().LifeValue, Is.EqualTo(1));
            Assert.True(game.NonActivePlayers.First().IsAlive);
            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //Second player's turn again

            Assert.That(goblin.LifeValue, Is.EqualTo(2));
            otherGoblin.Attack(game, goblin);
            Assert.That(goblin.LifeValue, Is.EqualTo(1));
            Assert.That(otherGoblin.LifeValue, Is.EqualTo(1));

            game.NextTurn(); //First player's turn again

            //Both monster cards die in battle
            Assert.True(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Graveyard).IsEmpty);
            Assert.True(game.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Graveyard).IsEmpty);
            Assert.False(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).IsEmpty);
            Assert.False(game.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Board).IsEmpty);
            goblin.Attack(game, otherGoblin);
            Assert.That(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Graveyard).Size, Is.EqualTo(1));
            Assert.That(game.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Graveyard).Size, Is.EqualTo(1));
            Assert.True(game.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).IsEmpty);
            Assert.True(game.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Board).IsEmpty);

            //Destroy second player
            Assert.That(game.NonActivePlayers.First().LifeValue, Is.EqualTo(1));
            Assert.True(game.NonActivePlayers.First().IsAlive);

            ITargetfulSpellCard fireball = (ITargetfulSpellCard)game.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[2];
            game.ActivePlayer.CastSpell(game, fireball, game.NonActivePlayers.First());

            Assert.That(game.NonActivePlayers.First().LifeValue, Is.EqualTo(0));
            Assert.False(game.NonActivePlayers.First().IsAlive);
        }

        [Test()]
        public void TestGameSerialization()
        {
            string serializedGame = JsonSerializer.ToJson(game);
            IGame? gameCopy = JsonSerializer.FromJson<Game>(serializedGame);
            string serializedGameCopy = JsonSerializer.ToJson(gameCopy);
            Assert.That(serializedGameCopy, Is.EqualTo(serializedGame), "Expected:\n{0}\n\nActual:\n{1}\n", new object[] { serializedGame, serializedGameCopy });

            Assert.That(gameCopy!.Players.Count, Is.EqualTo(2));
            Assert.That(gameCopy!.ActivePlayer.AllCards.Count, Is.EqualTo(5));
        }
    }
}
