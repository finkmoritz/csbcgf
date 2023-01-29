namespace csbcgf
{
    public abstract class Reaction : IReaction
    {
        public abstract void ReactTo(IGame game, IActionEvent actionEvent);
    }
}
