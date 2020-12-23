using System;
namespace csccgl
{
    public class CsccglException : Exception
    {
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
        public CsccglException(string message) : base(message)
        {
        }

        /// <summary>
        /// Library specific Exception. Usually thrown when the Game's
        /// mechanics are being violated.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public CsccglException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
