using System;

namespace csbcgf
{
    public class EndOfGameEvent : Event
    {
        public EndOfGameEvent()
        {
        }

        public override object Clone()
        {
            return new EndOfGameEvent();
        }
    }
}
