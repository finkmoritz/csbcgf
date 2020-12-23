using System;
namespace csccgl
{
    public interface IHand : IDeck
    {
        ICard RemoveAt(int index);
        void Add(ICard card);
    }
}
