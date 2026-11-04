using GalaxEngine;
namespace SpaceShooter;
public class EnemyController : Component
{
    private const float Speed = 2 * 60;

    private int direction = 1;
    private Health health = null!;
    private MovementComponent movement = null!;

    public override void Start()
    {
        health = GetComponent<Health>()!;
        movement = GetComponent<MovementComponent>()!;
        health.MaxHealth = 2;
        health.CurrentHealth = 2;
    }

    public override void Update()
    {
        float x = Transform.Position.X;
        if (x > 600)
            direction = -1;
        if (x < 100)
            direction = 1;
        movement.Velocity = new Vector2(Speed * direction, 0);

        if (health.CurrentHealth <= 0)
            Owner.Destroy();
    }
}
