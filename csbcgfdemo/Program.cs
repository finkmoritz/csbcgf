using System;
using System.Collections.Generic;
using csbcgf;

namespace csbcgfdemo
{
    class MainClass
    {
        private const string CommandPlayMonster = "PM";
        private const string CommandPlaySpell = "PS";
        private const string CommandAttack = "A";
        private const string CommandEndTurn = "E";
        private const string CommandQuit = "Q";

        public static void Main(string[] args)
        {
            IGame game = CreateGame();
            game.StartGame(initialHandSize: 3, initialPlayerLife: 5);

            string info = string.Empty;
            string input;
            do
            {
                Console.Clear();
                Console.WriteLine(info + "\n");
                GameConverter.PrintGame(game);
                Console.WriteLine(GetOptions());
                input = Console.ReadLine();
                info = ProcessInput(game, input);
            } while (input.ToUpper() != CommandQuit);
        }

        private static IGame CreateGame()
        {
            List<IPlayer> players = new List<IPlayer>();
            for (int i=0; i<2; ++i)
            {
                IDeck deck = new Deck();
                for (int n=0; n<20; ++n)
                {
                    deck.Push(new Wisp());
                }
                players.Add(new Player(deck));
            }
            return new Game(players);
        }

        private static string GetOptions()
        {
            string options = "Choose command:\n";
            options += CommandPlayMonster + " <id> <slot_index>: Play monster card from hand (with <id>) to the board (at position <slot_index>, i.e. 0-5)\n";
            options += CommandPlaySpell + " <id> [<target_id>]: Play spell card from hand (with <id>). Cast spell on target (with <target_id>) if the spell requires a target\n";
            options += CommandAttack + " <id> <target_id>: Attack monster card (with <target_id>) with monster card (with <id>)\n";
            options += CommandEndTurn + ": End Turn\n";
            options += CommandQuit +": Quit\n";
            return options;
        }

        private static string ProcessInput(IGame game, string input)
        {
            string output = string.Empty;
            try
            {
                string[] inputParams = input.Split(' ');
                switch (inputParams[0].ToUpper())
                {
                    case CommandPlayMonster:
                        IMonsterCard monsterCard = (IMonsterCard)GetObjectById(game, inputParams[1]);
                        game.ActivePlayer.PlayMonster(game, monsterCard, int.Parse(inputParams[2]));
                        output = "Played monster card";
                        break;
                    case CommandPlaySpell:
                        ISpellCard spellCard = (ISpellCard)GetObjectById(game, inputParams[1]);
                        if (spellCard is TargetlessSpellCard targetlessSpellCard)
                        {
                            game.ActivePlayer.PlaySpell(game, targetlessSpellCard);
                        } else if (spellCard is TargetfulSpellCard targetfulSpellCard)
                        {
                            ICharacter target = (ICharacter)GetObjectById(game, inputParams[2]);
                            game.ActivePlayer.PlaySpell(game, targetfulSpellCard, target);
                        }
                        break;
                    case CommandAttack:
                        IMonsterCard monster = (IMonsterCard)GetObjectById(game, inputParams[1]);
                        ICharacter targetCard = (ICharacter)GetObjectById(game, inputParams[2]);
                        monster.Attack(game, targetCard);
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
            } catch (Exception e)
            {
                output = e.Message + "\n" + e.StackTrace;
            }
            return output;
        }

        private static Object GetObjectById(IGame game, string id)
        {
            switch(int.Parse(id.Substring(0, 1)))
            {
                case 0:
                    return game.NonActivePlayers[0];
                case 1:
                    return (ICharacter)game.NonActivePlayers[0].Hand[int.Parse(id.Substring(1, 1))];
                case 2:
                    return (ICharacter)game.NonActivePlayers[0].Board[int.Parse(id.Substring(1, 1))];
                case 3:
                    return (ICharacter)game.ActivePlayer.Board[int.Parse(id.Substring(1, 1))];
                case 4:
                    return (ICharacter)game.ActivePlayer.Hand[int.Parse(id.Substring(1, 1))];
                case 5:
                    return game.ActivePlayer;
                default:
                    throw new Exception("Unparsable id: " + id);
            }
        }
    }
}
