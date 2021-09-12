using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class StartOfGameEvent : Event
    {
        [JsonConstructor]
        public StartOfGameEvent()
        {
        }

        public override object Clone()
        {
            return new StartOfGameEvent();
        }
    }
}
