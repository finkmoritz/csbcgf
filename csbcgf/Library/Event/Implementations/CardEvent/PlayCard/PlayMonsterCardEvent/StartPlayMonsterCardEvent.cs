using System;

namespace csbcgf
{
    [Serializable]
    public class StartPlayMonsterCardEvent : StartPlayCardEvent
    {
        public StartPlayMonsterCardEvent(IMonsterCard monsterCard, int boardIndex)
            : base(monsterCard)
        {
            BoardIndex = boardIndex;
        }

        public StartPlayMonsterCardEvent(Func<IMonsterCard> getMonsterCard, int boardIndex)
            : base(getMonsterCard)
        {
            BoardIndex = boardIndex;
        }

        public int BoardIndex { get; protected set; }
    }
}
