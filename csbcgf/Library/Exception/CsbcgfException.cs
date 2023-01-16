namespace csbcgf
{
    public class CsbcgfException : Exception
    {
        protected const string MessagePrefix = "C# Battle Card Game Framework EXCEPTION: ";

        /// <summary>
        /// Library specific Exception. Usually thrown when the Game's
        /// mechanics are being violated.
        /// </summary>
        public CsbcgfException()
        {
        }

        /// <summary>
        /// Library specific Exception. Usually thrown when the Game's
        /// mechanics are being violated.
        /// </summary>
        /// <param name="message"></param>
        public CsbcgfException(string message)
            : base(MessagePrefix + message)
        {
        }

        /// <summary>
        /// Library specific Exception. Usually thrown when the Game's
        /// mechanics are being violated.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public CsbcgfException(string message, Exception inner)
            : base(MessagePrefix + message, inner)
        {
        }
    }
}
