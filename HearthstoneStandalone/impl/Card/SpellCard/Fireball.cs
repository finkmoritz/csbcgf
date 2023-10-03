namespace hearthstonestandalone
{
    /// <summary>
    /// Fireball deals damage equal to its cost to a hero.
    /// </summary>
    public class Fireball : HearthstoneSpellCard
    {

        public Fireball(int cost)
            : base(cost)
        {

        }

        public override void Play(HearthstoneSpellCardPlayEventArgs args)
        {
            args.Target?.ReceiveDamage(Cost);
        }
    }
}
