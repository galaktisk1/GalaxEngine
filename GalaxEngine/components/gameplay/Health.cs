namespace GalaxEngine;
public class Health : GameplayComponent
{
    public float MaxHealth { get; set; } = 100;
    public float CurrentHealth { get; set; } = 100;

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Owner.Destroy();
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth = MathF.Min(CurrentHealth + amount, MaxHealth);
    }
}
