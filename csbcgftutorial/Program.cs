using csbcgf;

namespace csbcgftutorial
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            IGame game = new Game();

            game.AddPlayer(new Player());
            game.AddPlayer(new Player());

            for (int i = 0; i < 2; ++i)
            {
                ICardCollection deck = game.Players.ElementAt(i).Deck;
                for (int j = 0; j < 20; ++j)
                {
                    IMonsterCard myMonsterCard = new MonsterCard(mana: 1, attack: 1, life: 1);
                    deck.Add(myMonsterCard);
                }
            }

            game.StartGame(initialHandSize: 1, initialPlayerLife: 2);

            IPlayer activePlayer = game.ActivePlayer;
            Console.WriteLine("Active player's mana = " + activePlayer.ManaValue);
            IMonsterCard goblin = (IMonsterCard)activePlayer.Hand[0];
            if (goblin.IsSummonable(game))
            {
                activePlayer.CastMonster(game, goblin);
                Console.WriteLine("Active player's mana = " + activePlayer.ManaValue);
            }

            game.NextTurn();

            activePlayer = game.ActivePlayer;
            IMonsterCard anotherGoblin = (IMonsterCard)activePlayer.Hand[0];
            activePlayer.CastMonster(game, anotherGoblin);

            game.NextTurn();

            if (goblin.IsReadyToAttack)
            {
                goblin.Attack(game, game.NonActivePlayers.First());
            }

            Console.WriteLine("First player is still alive: " + game.Players.First().IsAlive);

            Console.WriteLine("Goblin's life = " + goblin.LifeValue); // 1
            ICardComponent extraLifeComponent = new MonsterCardComponent(0, 0, 1);
            goblin.AddComponent(extraLifeComponent);
            Console.WriteLine("Goblin's life = " + goblin.LifeValue); // 2
            goblin.RemoveComponent(extraLifeComponent);
            Console.WriteLine("Goblin's life = " + goblin.LifeValue); // 1
        }
    }
}
