namespace hearthstonestandalone
{
    /// <summary>
    /// Fireball deals damage equal to its cost to a hero.
    /// </summary>
    public class Fireball : HearthstoneSpellCard
    {

        public Fireball(StateMachine stateMachine, int cost)
            : base(stateMachine, cost)
        {

        }

        public void Play(HearthstoneHero actor, HearthstoneHero target)
        {
            base.Play(actor);
            target.ReceiveDamage(Cost);
        }
    }
}
