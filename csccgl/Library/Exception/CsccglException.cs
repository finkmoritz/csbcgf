using System;
namespace csbcgf
{
    public class csbcgfException : Exception
    {
        const string MessagePrefix = "csbcgf EXCEPTION: ";

        /// <summary>
        /// Library specific Exception. Usually thrown when the Game's
        /// mechanics are being violated.
        /// </summary>
        public csbcgfException()
        {
        }

        /// <summary>
        /// Library specific Exception. Usually thrown when the Game's
        /// mechanics are being violated.
        /// </summary>
        /// <param name="message"></param>
        public csbcgfException(string message)
            : base(MessagePrefix + message)
        {
        }

        /// <summary>
        /// Library specific Exception. Usually thrown when the Game's
        /// mechanics are being violated.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public csbcgfException(string message, Exception inner)
            : base(MessagePrefix + message, inner)
        {
        }
    }
}
