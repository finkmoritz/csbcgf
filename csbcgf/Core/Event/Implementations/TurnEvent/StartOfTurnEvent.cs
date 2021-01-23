using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
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
