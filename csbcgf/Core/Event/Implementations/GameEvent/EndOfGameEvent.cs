using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class EndOfGameEvent : Event
    {
        [JsonConstructor]
        public EndOfGameEvent()
        {
        }

        public override object Clone()
        {
            return new EndOfGameEvent();
        }
    }
}
