using System;
using csbcgf;

namespace csccgl
{
    [Serializable]
    public class StartOfGameEvent : Event
    {
        public StartOfGameEvent()
        {
        }

        public override bool IsExecutable(IGame game)
        {
            return true;
        }
    }
}
