using csbcgf;

namespace hearthstonestandalone
{
    public class HearthstoneGame : GameObject<HearthstoneStateMachine>
    {
        public List<HearthstoneHero> Heros { get; init; }
        public int CurrentHeroIndex { get; init; }

        public HearthstoneGame(HearthstoneStateMachine stateMachine) : base(stateMachine)
        {
            Heros = new List<HearthstoneHero>();
            CurrentHeroIndex = 0;
        }

        public void StartGame()
        {
            StateMachine.SendHearthstoneGameStarted(new HearthstoneGameStartedEvent());
        }

        public void NextTurn()
        {
            StateMachine.SendHearthstoneTurnStarted(new HearthstoneTurnStartedEvent { CurrentHero = Heros[CurrentHeroIndex] });
        }
    }
}
