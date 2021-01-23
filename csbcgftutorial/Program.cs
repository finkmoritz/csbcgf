using System;
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

            IMonsterCard myMonsterCard = new MonsterCard(
                mana: 1,
                attack: 1,
                life: 1
            );
        }
    }
}
