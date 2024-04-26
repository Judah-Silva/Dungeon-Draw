public class DealDamage : Effect
{
    private readonly int _damage;
    
    public DealDamage(int nbTurns, int damage) : base(nbTurns)
    {
        _damage = damage;
    }
    
    public DealDamage(int nbTurns, Entity entity, int damage) : base(nbTurns, entity)
    {
        _damage = damage;
    }

    protected override void PlayEffect()
    {
        Entity.TakeDamage(_damage);
    }
}
