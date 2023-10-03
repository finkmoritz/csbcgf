using hearthstonestandalone;

Console.WriteLine("Welcome to Hearthstone!");

HearthstoneGame game = new HearthstoneGame();

for (int heroIndex = 0; heroIndex < 2; ++heroIndex)
{
    HearthstoneHero hero = new HearthstoneHero(20);
    game.Heros.Add(hero);
    game.GameStarted += hero.OnGameStarted;

    for (int cardIndex = 0; cardIndex < 30; ++cardIndex)
    {
        Fireball fireball = new Fireball(1);
        game.Heros[heroIndex].Deck.Add(fireball);
    }
}

game.StartGame();
