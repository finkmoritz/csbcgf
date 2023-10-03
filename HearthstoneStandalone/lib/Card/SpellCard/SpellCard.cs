namespace hearthstonestandalone
{
    public abstract class HearthstoneSpellCard : HearthstoneCard
    {
        public HearthstoneSpellCard(int cost)
            : base(cost)
        {

        }

        public abstract void Play(HearthstoneSpellCardPlayEventArgs args);
    }
}