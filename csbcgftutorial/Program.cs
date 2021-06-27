using System;
using System.Collections.Generic;
using csbcgf;

namespace csbcgftutorial
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            IGame myGame = new Game();

            IPlayer myPlayer1 = new Player();
            IPlayer myPlayer2 = new Player();

            myGame.Players.Add(myPlayer1);
            myGame.Players.Add(myPlayer2);

            Console.WriteLine("Life stat of player 1 = " + myPlayer1.LifeValue
                + " / " + myPlayer1.LifeBaseValue);
            Console.WriteLine("Mana pool stat of player 1 = " + myPlayer1.ManaValue
                + " / " + myPlayer1.ManaBaseValue);


            List<IPlayer> players = new List<IPlayer>();
            for (int i = 0; i < 2; ++i)
            {
                IDeck deck = new Deck();

                for (int j = 0; j < 20; ++j)
                {
                    IMonsterCard myMonsterCard = new MonsterCard(mana: 1, attack: 1, life: 1);
                    deck.Push(myMonsterCard);
                }

                players.Add(new Player(deck));
            }

            IGame game = new Game(players);
            game.StartGame(initialHandSize: 1, initialPlayerLife: 2);

            IPlayer activePlayer = game.ActivePlayer;
            Console.WriteLine("Active player's mana = " + activePlayer.ManaValue);
            IMonsterCard goblin = (IMonsterCard)activePlayer.Hand[0];
            if(goblin.IsSummonable(game))
            {
                activePlayer.CastMonster(game, goblin, 0);
                Console.WriteLine("Active player's mana = " + activePlayer.ManaValue);
            }

            game.NextTurn();

            activePlayer = game.ActivePlayer;
            IMonsterCard anotherGoblin = (IMonsterCard)activePlayer.Hand[0];
            activePlayer.CastMonster(game, anotherGoblin, 0);

            game.NextTurn();

            if(goblin.IsReadyToAttack)
            {
                goblin.Attack(game, game.NonActivePlayers[0]);
            }
            
            Console.WriteLine("First player is still alive: " + game.Players[0].IsAlive);

            Console.WriteLine("Goblin's life = " + goblin.LifeValue); // 1
            ICardComponent extraLifeComponent = new MonsterCardComponent(0, 0, 1);
            goblin.Components.Add(extraLifeComponent);
            Console.WriteLine("Goblin's life = " + goblin.LifeValue); // 2
            goblin.Components.Remove(extraLifeComponent);
            Console.WriteLine("Goblin's life = " + goblin.LifeValue); // 1
        }
    }
}
