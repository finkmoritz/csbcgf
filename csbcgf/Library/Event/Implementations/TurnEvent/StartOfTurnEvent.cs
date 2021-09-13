using System;

namespace csbcgf
{
    public class StartOfTurnEvent : Event
    {
        public StartOfTurnEvent()
        {
        }

        public override object Clone()
        {
            return new StartOfTurnEvent();
        }
    }
}
