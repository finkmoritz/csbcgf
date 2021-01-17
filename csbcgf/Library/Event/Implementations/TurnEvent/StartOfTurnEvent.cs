using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class StartOfTurnEvent : Event
    {
        [JsonConstructor]
        public StartOfTurnEvent()
        {
        }

        public override object Clone()
        {
            return new StartOfTurnEvent();
        }
    }
}
