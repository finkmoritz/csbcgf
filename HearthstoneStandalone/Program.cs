using hearthstonestandalone;

class Program
{

    public static void Main()
    {
        HearthstoneStateMachine stateMachine = new HearthstoneStateMachine();

        HearthstoneGame game = new HearthstoneGame(stateMachine);

        for (int heroIndex = 0; heroIndex < 2; ++heroIndex)
        {
            HearthstoneHero hero = new HearthstoneHero(stateMachine);
            game.Heros.Add(hero);

            for (int cardIndex = 0; cardIndex < 30; ++cardIndex)
            {
                Fireball fb = new Fireball(stateMachine, 1);
                hero.Deck.Add(fb);
            }
        }

        game.StartGame();
        game.NextTurn();

        PrintGameState(game);

        HearthstoneHero currentHero = game.Heros[game.CurrentHeroIndex];

        Fireball fireball = (Fireball)currentHero.Hand[0];
        fireball.Play(currentHero, game.Heros[1 - game.CurrentHeroIndex]);

        PrintGameState(game);
    }

    private static void PrintGameState(HearthstoneGame game)
    {
        Console.WriteLine("\n\n-------------\n");

        HearthstoneHero hero0 = game.Heros[0];
        Console.WriteLine("Hero 0 (diamonds=" + hero0.Diamonds + ", life=" + hero0.Life + ", cards=" + hero0.Hand.Count + ")");

        HearthstoneHero hero1 = game.Heros[1];
        Console.WriteLine("Hero 1 (diamonds=" + hero1.Diamonds + ", life=" + hero1.Life + ", cards=" + hero1.Hand.Count + ")");

        Console.WriteLine("\n-------------\n\n");
    }
}
