using System.Collections.Generic;

namespace csccgl
{
    public class CsccglExample
    {
        public CsccglExample()
        {
        }
    }

    public class MyMonsterCard : MonsterCard
    {
        public MyMonsterCard() : base(2, 3, 1)
        {
        }

        public override List<ICharacter> GetPotentialTargets(Game game)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsPlayable(Game game)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MyTargetlessSpellCard : TargetlessSpellCard
    {
        public MyTargetlessSpellCard() : base(5)
        {
        }

        public override bool IsPlayable(Game game)
        {
            throw new System.NotImplementedException();
        }

        public override void Play(Game game)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MyTargetfulSpellCard : TargetfulSpellCard
    {
        public MyTargetfulSpellCard() : base(4)
        {
        }

        public override List<ICharacter> GetPotentialTargets(Game game)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsPlayable(Game game)
        {
            throw new System.NotImplementedException();
        }

        public override void Play(Game game, ICharacter targetCharacter)
        {
            throw new System.NotImplementedException();
        }
    }
}
