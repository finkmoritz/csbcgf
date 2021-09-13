using System;

namespace csbcgf
{
    public class EndOfTurnEvent : Event
    {
        public EndOfTurnEvent()
        {
        }

        public override object Clone()
        {
            return new EndOfTurnEvent();
        }
    }
}
