using System;

namespace csbcgf
{
    [Serializable]
    public class EndPlayMonsterCardEvent : EndPlayCardEvent
    {
        public EndPlayMonsterCardEvent(IMonsterCard monsterCard, int boardIndex)
            : base(monsterCard)
        {
            BoardIndex = boardIndex;
        }

        public EndPlayMonsterCardEvent(Func<IMonsterCard> getMonsterCard, int boardIndex)
            : base(getMonsterCard)
        {
            BoardIndex = boardIndex;
        }

        public int BoardIndex { get; protected set; }
    }
}
