namespace csbcgf
{
    public interface IReaction
    {
        /// <summary>
        /// React on a given ActionEvent.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="actionEvent"></param>
        void ReactTo(IGame game, IActionEvent actionEvent);
    }
}
