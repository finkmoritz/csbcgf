namespace hearthstonestandalone
{
    public class HearthstoneGame : StatefulGameObject
    {
        public List<HearthstoneHero> Heros { get; init; }
        public int CurrentHeroIndex { get; init; }

        public HearthstoneGame(StateMachine stateMachine) : base(stateMachine)
        {
            Heros = new List<HearthstoneHero>();
            CurrentHeroIndex = 0;
        }

        public void StartGame()
        {
            StateMachine.OnGameStarted();
        }

        public void NextTurn()
        {
            StateMachine.OnTurnStarted(Heros[CurrentHeroIndex]);
        }
    }
}
