using NUnit.Framework;
using csbcgf;

namespace hearthstone
{
    [TestFixture()]
    public class Test
    {
        private HearthstoneGame game = null!;

        [SetUp()]
        public void SetUp()
        {
            HearthstoneGameState gameState = new HearthstoneGameState();

            for (int i = 0; i < 2; ++i)
            {
                HearthstonePlayer player = new HearthstonePlayer();
                player.AddStat(StatKeys.Life, new Stat(2, 2));

                ICardCollection deck = player.GetCardCollection(CardCollectionKeys.Deck);

                for (int j = 0; j < 3; ++j)
                {
                    ICard fireball = new DamageSpellCard(3);
                    deck.Add(fireball);
                }

                for (int j = 0; j < 2; ++j)
                {
                    ICard goblin = new HearthstoneMonsterCard(2, 1, 2);
                    deck.Add(goblin);
                }

                gameState.AddPlayer(player);
            }

            game = new HearthstoneGame(gameState);

            foreach (HearthstonePlayer player in game.State.Players)
            {
                player.DrawCard(game);
            }

            game.NextTurn();
        }

        [Test()]
        public void TestInitialConditions()
        {
            HearthstoneGameState state = game.State;

            Assert.That(state.ActivePlayer.GetValue(StatKeys.Mana), Is.EqualTo(1));
            Assert.That(state.NonActivePlayers.First().GetValue(StatKeys.Mana), Is.EqualTo(0));
            Assert.That(state.ActivePlayer.GetBaseValue(StatKeys.Mana), Is.EqualTo(1));
            Assert.That(state.NonActivePlayers.First().GetBaseValue(StatKeys.Mana), Is.EqualTo(0));

            Assert.That(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(3));
            Assert.That(state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(4));
            Assert.That(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(2));
            Assert.That(state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(1));

            HearthstoneMonsterCard goblin = (HearthstoneMonsterCard)state.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[0];
            Assert.That(goblin.GetValue(StatKeys.Mana), Is.EqualTo(2));
            Assert.That(goblin.GetValue(StatKeys.Attack), Is.EqualTo(1));
            Assert.That(goblin.GetValue(StatKeys.Life), Is.EqualTo(2));

            Assert.False(goblin.IsSummonable(state));
        }

        [Test()]
        public void TestGame()
        {
            HearthstoneGameState state = game.State;

            HearthstoneMonsterCard goblin = (HearthstoneMonsterCard)state.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[0];
            Assert.False(goblin.IsSummonable(state));

            game.NextTurn(); //Second player's turn

            Assert.That(state.ActivePlayer.GetValue(StatKeys.Mana), Is.EqualTo(1));
            Assert.That(state.NonActivePlayers.First().GetValue(StatKeys.Mana), Is.EqualTo(1));
            Assert.That(state.ActivePlayer.GetBaseValue(StatKeys.Mana), Is.EqualTo(1));
            Assert.That(state.NonActivePlayers.First().GetBaseValue(StatKeys.Mana), Is.EqualTo(1));

            Assert.That(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(3));
            Assert.That(state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(3));
            Assert.That(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(2));
            Assert.That(state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(2));

            game.NextTurn(); //First player's turn again

            Assert.That(state.ActivePlayer.GetValue(StatKeys.Mana), Is.EqualTo(2));
            Assert.That(state.NonActivePlayers.First().GetValue(StatKeys.Mana), Is.EqualTo(1));
            Assert.That(state.ActivePlayer.GetBaseValue(StatKeys.Mana), Is.EqualTo(2));
            Assert.That(state.NonActivePlayers.First().GetBaseValue(StatKeys.Mana), Is.EqualTo(1));

            Assert.That(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(2));
            Assert.That(state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Deck).Size, Is.EqualTo(3));
            Assert.That(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(3));
            Assert.That(state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Hand).Size, Is.EqualTo(2));

            goblin = (HearthstoneMonsterCard)state.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[0];
            Assert.True(goblin.IsSummonable(state));

            //Play monster card
            Assert.True(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).IsEmpty);
            ((HearthstonePlayer)state.ActivePlayer).SummonMonster(game, goblin);
            Assert.False(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).IsEmpty);
            Assert.That(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).Cards.Count, Is.EqualTo(1));

            Assert.That(state.ActivePlayer.GetValue(StatKeys.Mana), Is.EqualTo(0));
            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //Second player's turn again

            Assert.That(state.ActivePlayer.GetValue(StatKeys.Mana), Is.EqualTo(2));
            Assert.That(state.NonActivePlayers.First().GetValue(StatKeys.Mana), Is.EqualTo(0));
            Assert.That(state.ActivePlayer.GetBaseValue(StatKeys.Mana), Is.EqualTo(2));
            Assert.That(state.NonActivePlayers.First().GetBaseValue(StatKeys.Mana), Is.EqualTo(2));

            HearthstoneMonsterCard otherGoblin = (HearthstoneMonsterCard)state.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[0];
            ((HearthstonePlayer)state.ActivePlayer).SummonMonster(game, otherGoblin);

            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //First player's turn again

            Assert.That(state.ActivePlayer.GetValue(StatKeys.Mana), Is.EqualTo(3));
            Assert.That(state.NonActivePlayers.First().GetValue(StatKeys.Mana), Is.EqualTo(0));
            Assert.That(state.ActivePlayer.GetBaseValue(StatKeys.Mana), Is.EqualTo(3));
            Assert.That(state.NonActivePlayers.First().GetBaseValue(StatKeys.Mana), Is.EqualTo(2));

            //Attack second player
            Assert.True(goblin.IsReadyToAttack);
            Assert.That(state.NonActivePlayers.First().GetValue(StatKeys.Life), Is.EqualTo(2));
            goblin.Attack(game, state.NonActivePlayers.First());
            Assert.That(state.NonActivePlayers.First().GetValue(StatKeys.Life), Is.EqualTo(1));
            Assert.False(goblin.IsReadyToAttack);

            game.NextTurn(); //Second player's turn again

            Assert.That(goblin.GetValue(StatKeys.Life), Is.EqualTo(2));
            otherGoblin.Attack(game, goblin);
            Assert.That(goblin.GetValue(StatKeys.Life), Is.EqualTo(1));
            Assert.That(otherGoblin.GetValue(StatKeys.Life), Is.EqualTo(1));

            game.NextTurn(); //First player's turn again

            //Both monster cards die in battle
            Assert.True(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Graveyard).IsEmpty);
            Assert.True(state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Graveyard).IsEmpty);
            Assert.False(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).IsEmpty);
            Assert.False(state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Board).IsEmpty);
            goblin.Attack(game, otherGoblin);
            Assert.That(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Graveyard).Size, Is.EqualTo(1));
            Assert.That(state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Graveyard).Size, Is.EqualTo(1));
            Assert.True(state.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).IsEmpty);
            Assert.True(state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Board).IsEmpty);

            //Destroy second player
            Assert.That(state.NonActivePlayers.First().GetValue(StatKeys.Life), Is.EqualTo(1));

            HearthstoneTargetfulSpellCard fireball = (HearthstoneTargetfulSpellCard)state.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[2];
            ((HearthstonePlayer)state.ActivePlayer).CastSpell(game, fireball, state.NonActivePlayers.First());

            Assert.That(state.NonActivePlayers.First().GetValue(StatKeys.Life), Is.EqualTo(-2));
        }

        [Test()]
        public void TestGameSerialization()
        {
            string serializedGame = JsonSerializer.ToJson(game);
            HearthstoneGame? gameCopy = JsonSerializer.FromJson<HearthstoneGame>(serializedGame);
            string serializedGameCopy = JsonSerializer.ToJson(gameCopy);
            Assert.That(serializedGameCopy, Is.EqualTo(serializedGame), "Expected:\n{0}\n\nActual:\n{1}\n", new object[] { serializedGame, serializedGameCopy });

            Assert.That(gameCopy!.State.Players.Count, Is.EqualTo(2));
            Assert.That((gameCopy!.State).ActivePlayer.AllCards.Count, Is.EqualTo(5));
        }
    }
}
