using System;
namespace csbcgf
{
    [Serializable]
    public class CsbcgfException : Exception
    {
        const string MessagePrefix = "csbcgf EXCEPTION: ";

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
