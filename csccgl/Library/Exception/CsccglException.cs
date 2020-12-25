using System;
namespace csccgl
{
    public class CsccglException : Exception
    {
        const string MessagePrefix = "CSCCGL EXCEPTION: ";

        /// <summary>
        /// Library specific Exception. Usually thrown when the Game's
        /// mechanics are being violated.
        /// </summary>
        public CsccglException()
        {
        }

        /// <summary>
        /// Library specific Exception. Usually thrown when the Game's
        /// mechanics are being violated.
        /// </summary>
        /// <param name="message"></param>
        public CsccglException(string message)
            : base(MessagePrefix + message)
        {
        }

        /// <summary>
        /// Library specific Exception. Usually thrown when the Game's
        /// mechanics are being violated.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public CsccglException(string message, Exception inner)
            : base(MessagePrefix + message, inner)
        {
        }
    }
}
