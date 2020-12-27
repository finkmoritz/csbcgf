using System;
using csbcgf;

namespace csccgl
{
    public class StartOfTurnEvent : Event
    {
        public StartOfTurnEvent()
        {
        }

        public override bool IsExecutable(IGame game) => true;
    }
}
