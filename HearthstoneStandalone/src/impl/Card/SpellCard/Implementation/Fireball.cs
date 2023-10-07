namespace hearthstonestandalone
{
    /// <summary>
    /// Fireball deals damage equal to its cost to a hero.
    /// </summary>
    public class Fireball : HearthstoneSpellCard
    {

        public Fireball(HearthstoneStateMachine stateMachine, int cost)
            : base(stateMachine, cost)
        {

        }

        public void Play(HearthstoneHero actor, HearthstoneHero target)
        {
            base.Play(actor);
            target.ReceiveDamage(new HearthstoneDamage { Amount = Cost });
        }
    }
}
