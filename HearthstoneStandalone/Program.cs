using hearthstonestandalone;

class Program
{

    public static void Main()
    {
        HearthstoneGame game = new HearthstoneGame();

        for (int heroIndex = 0; heroIndex < 2; ++heroIndex)
        {
            HearthstoneHero hero = new HearthstoneHero();
            game.Heros.Add(hero);
            game.GameStarted += hero.OnGameStarted;
            game.TurnStarted += hero.OnTurnStarted;

            for (int cardIndex = 0; cardIndex < 30; ++cardIndex)
            {
                Fireball fb = new Fireball(1);
                hero.Deck.Add(fb);
            }
        }

        game.StartGame();
        game.NextTurn();

        PrintGameState(game);

        HearthstoneHero currentHero = game.Heros[game.CurrentHeroIndex];

        Fireball fireball = (Fireball)currentHero.Hand[0];
        HearthstoneSpellCardPlayEventArgs args = new HearthstoneSpellCardPlayEventArgs();
        args.Target = game.Heros[1 - game.CurrentHeroIndex];
        currentHero.PlaySpellCard(fireball, args);

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
