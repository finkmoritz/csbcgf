using System;
namespace csccgl
{
    public interface IBoard : IDeck
    {
        ICard RemoveAt(int index);
        void AddAt(int index, ICard card);
    }
}
