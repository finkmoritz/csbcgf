using System;
namespace csccgl
{
    [Serializable]
    public abstract class TargetlessSpellCardComponent : CardComponent, ITargetlessSpellCardComponent
    {
        public TargetlessSpellCardComponent(int mana) : base(mana)
        {
        }
    }
}
