using csbcgf;

namespace hearthstone
{

    public class HearthstoneGameObject : GameObject<HearthstoneStateMachine>
    {
        public List<HearthstoneGameObject> Components { get; init; }

        public HearthstoneGameObject(HearthstoneStateMachine stateMachine) : base(stateMachine)
        {
            Components = new List<HearthstoneGameObject>();
        }
    }
}
