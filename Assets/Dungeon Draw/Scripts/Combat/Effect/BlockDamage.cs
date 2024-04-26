public class BlockDamage : Effect
{
    private readonly int _damage;
    
    public BlockDamage(int nbTurns, int damage) : base(nbTurns)
    {
        _damage = damage;
    }
    
    public BlockDamage(int nbTurns, Entity entity, int damage) : base(nbTurns, entity)
    {
        _damage = damage;
    }

    protected override void PlayEffect()
    {
        Entity.AddBlockDamage(_damage);
    }
}
