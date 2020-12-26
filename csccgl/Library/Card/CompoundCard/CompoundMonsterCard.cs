using System;
using System.Collections.Generic;
using System.Linq;

namespace csccgl
{
    public class CompoundMonsterCard : CompoundCard, IMonsterCard
    {
        public LifeStat LifeStat { get; }
        public AttackStat AttackStat { get; }

        public CompoundMonsterCard(List<IMonsterCard> components) : base(new List<ICard>())
        {
            components.ForEach(c => Components.Add(c));
            this.LifeStat = new LifeStat(components.Sum(c => c.LifeStat.Value));
            this.AttackStat = new AttackStat(components.Sum(c => c.AttackStat.Value));
        }

        public void Attack(IGame game, ICharacter targetCharacter)
        {
            game.Queue(new ModifyLifeStatAction(targetCharacter, -this.AttackStat.Value));
            game.Queue(new ModifyLifeStatAction(this, -targetCharacter.AttackStat.Value));
        }

        public HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            //Compute the intersection of all potential targets
            HashSet<ICharacter> potentialTargets = ((IMonsterCard)Components[0]).GetPotentialTargets(game);
            for(int i=1; i<Components.Count; ++i)
            {
                IMonsterCard monsterCard = (IMonsterCard)Components[i];
                HashSet<ICharacter> potTargets = monsterCard.GetPotentialTargets(game);
                potentialTargets.RemoveWhere(t => !potTargets.Contains(t));
            }
            return potentialTargets;
        }

        public bool IsAlive() => LifeStat.Value > 0;
    }
}
