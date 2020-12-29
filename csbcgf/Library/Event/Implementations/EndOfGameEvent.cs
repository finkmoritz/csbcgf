using System;
using csbcgf;

namespace csccgl
{
    [Serializable]
    public class EndOfGameEvent : Event
    {
        public EndOfGameEvent()
        {
        }

        public override bool IsExecutable(IGame game)
        {
            return true;
        }
    }
}
