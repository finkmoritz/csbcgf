using System;
using csbcgf;

namespace csccgl
{
    [Serializable]
    public class StartOfTurnEvent : Event
    {
        public StartOfTurnEvent()
        {
        }

        public override bool IsExecutable(IGame game)
        {
            return true;
        }
    }
}
