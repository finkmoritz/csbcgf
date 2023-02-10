using csbcgf;

namespace hearthstone
{
    class MainClass
    {
        private const string CommandSummon = "S";
        private const string CommandCast = "C";
        private const string CommandAttack = "A";
        private const string CommandEndTurn = "E";
        private const string CommandQuit = "Q";

        public static void Main(string[] args)
        {
            HearthstoneGame game = CreateGame();
            game.NextTurn();

            string info = string.Empty;
            string input;
            do
            {
                Console.Clear();
                Console.WriteLine(info + "\n");
                ConsoleUtil.PrintGame(game.State);
                Console.WriteLine(GetOptions());
                input = Console.ReadLine() ?? "";
                info = ProcessInput(game, input);
            } while (input.ToUpper() != CommandQuit);
        }

        private static HearthstoneGame CreateGame()
        {
            HearthstoneGameState gameState = new HearthstoneGameState();
            for (int i = 0; i < 2; ++i)
            {
                IPlayer player = new Player();
                player.AddStat(StatKeys.Life, new Stat(5, 5));

                ICardCollection deck = player.GetCardCollection(CardCollectionKeys.Deck);
                for (int n = 0; n < 3; ++n)
                {
                    deck.Add(new Wisp());
                    deck.Add(new ArgentSquire());
                    deck.Add(new Bananas());
                    deck.Add(new ManaWyrm());
                    deck.Add(new FarSight());
                    deck.Add(new KingMukla());
                }
                deck.Shuffle();

                gameState.AddPlayer(player);
            }

            HearthstoneGame game = new HearthstoneGame(gameState);

            foreach (HearthstonePlayer player in gameState.Players)
            {
                for (int i = 0; i < 3; ++i)
                {
                    player.DrawCard(game);
                }
            }

            return game;
        }

        private static string GetOptions()
        {
            string options = "Choose command:\n";
            options += CommandSummon + " <id>: Summon monster card from hand (with <id>) to the board\n";
            options += CommandCast + " <id> [<target_id>]: Cast spell card from hand (with <id>). Cast spell on target (with <target_id>) if the spell requires a target\n";
            options += CommandAttack + " <id> <target_id>: Attack monster card (with <target_id>) with monster card (with <id>)\n";
            options += CommandEndTurn + ": End Turn\n";
            options += CommandQuit + ": Quit\n";
            return options;
        }

        private static string ProcessInput(HearthstoneGame game, string input)
        {
            string output = string.Empty;
            try
            {
                HearthstoneGameState state = game.State;
                HearthstonePlayer activePlayer = (HearthstonePlayer)state.ActivePlayer;

                string[] inputParams = input.Split(' ');
                switch (inputParams[0].ToUpper())
                {
                    case CommandSummon:
                        HearthstoneMonsterCard monsterCard = (HearthstoneMonsterCard)GetObjectById(game, inputParams[1]);
                        activePlayer.SummonMonster(game, monsterCard);
                        output = "Cast monster card";
                        break;
                    case CommandCast:
                        HearthstoneSpellCard spellCard = (HearthstoneSpellCard)GetObjectById(game, inputParams[1]);
                        activePlayer = (HearthstonePlayer)state.ActivePlayer;
                        if (spellCard is HearthstoneTargetlessSpellCard targetlessSpellCard)
                        {
                            activePlayer.CastSpell(game, targetlessSpellCard);
                            output = "Cast spell";
                        }
                        else if (spellCard is HearthstoneTargetfulSpellCard targetfulSpellCard)
                        {
                            IStatContainer target = (IStatContainer)GetObjectById(game, inputParams[2]);
                            activePlayer.CastSpell(game, targetfulSpellCard, target);
                            output = "Cast spell onto target";
                        }
                        break;
                    case CommandAttack:
                        HearthstoneMonsterCard monster = (HearthstoneMonsterCard)GetObjectById(game, inputParams[1]);
                        IStatContainer targetCard = (IStatContainer)GetObjectById(game, inputParams[2]);
                        monster.Attack(game, targetCard);
                        output = "Attacked";
                        break;
                    case CommandEndTurn:
                        game.NextTurn();
                        output = "Player ended turn.";
                        break;
                    case CommandQuit:
                        break;
                    default:
                        output = "Invalid command: " + input;
                        break;
                }
            }
            catch (Exception e)
            {
                output = e.Message + "\n" + e.StackTrace;
            }
            return output;
        }

        private static Object GetObjectById(HearthstoneGame game, string id)
        {
            HearthstoneGameState state = game.State;
            switch (int.Parse(id.Substring(0, 1)))
            {
                case 0:
                    return state.NonActivePlayers.First();
                case 1:
                    return state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Hand)[int.Parse(id.Substring(1, 1))];
                case 2:
                    return state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Board)[int.Parse(id.Substring(1, 1))]!;
                case 3:
                    return state.ActivePlayer.GetCardCollection(CardCollectionKeys.Board)[int.Parse(id.Substring(1, 1))]!;
                case 4:
                    return state.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand)[int.Parse(id.Substring(1, 1))];
                case 5:
                    return state.ActivePlayer;
                default:
                    throw new Exception("Unparsable id: " + id);
            }
        }
    }
}
