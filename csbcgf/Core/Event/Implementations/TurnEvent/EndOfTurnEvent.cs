using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class EndOfTurnEvent : Event
    {
        [JsonConstructor]
        public EndOfTurnEvent()
        {
        }

        public override object Clone()
        {
            return new EndOfTurnEvent();
        }
    }
}
