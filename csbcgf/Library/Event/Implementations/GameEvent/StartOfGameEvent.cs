using System;

namespace csbcgf
{
    public class StartOfGameEvent : Event
    {
        public StartOfGameEvent()
        {
        }

        public override object Clone()
        {
            return new StartOfGameEvent();
        }
    }
}
