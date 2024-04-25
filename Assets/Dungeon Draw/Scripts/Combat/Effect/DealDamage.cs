public class DealDamage
{
    public int damage;
    public DealDamage(int damage)
    {
        this.damage = damage;
    }
    public void ApplyEffect(Entity entity)
    {
        entity.TakeDamage(damage);
    }
}
