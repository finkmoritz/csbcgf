using System.Collections.Immutable;
using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class StatContainer : IStatContainer
    {
        [JsonProperty]
        protected IDictionary<string, IList<IStat>> stats = null!;

        protected StatContainer() { }

        public StatContainer(bool _ = true)
        {
            stats = new Dictionary<string, IList<IStat>>();
        }

        [JsonIgnore]
        public IDictionary<string, IList<IStat>> Stats
        {
            get => stats.ToImmutableDictionary();
        }

        public bool RemoveStat(string key, IStat stat)
        {
            if (stats.ContainsKey(key))
            {
                return stats[key].Remove(stat);
            }
            return false;
        }

        public void AddStat(string key, IStat stat)
        {
            if (!stats.ContainsKey(key))
            {
                stats.Add(key, new List<IStat>());
            }
            stats[key].Add(stat);
        }

        public virtual int GetValue(string key)
        {
            if (!stats.ContainsKey(key))
            {
                return 0;
            }
            else
            {
                return stats[key].Sum(s => s.Value);
            }
        }

        public virtual int GetBaseValue(string key)
        {
            if (!stats.ContainsKey(key))
            {
                return 0;
            }
            else
            {
                return stats[key].Sum(s => s.BaseValue);
            }
        }
    }
}
