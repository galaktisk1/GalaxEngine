using GalaxEngine;
namespace SpaceShooter;
public class LaserController : Component
{
    public float Speed = 3 * 60;

    private MovementComponent movement = null!;

    public override void Start()
    {
        movement = GetComponent<MovementComponent>()!;
    }

    public override void Update()
    {
        movement.Velocity = new Vector2(0, -Speed);

        var player = SceneObject.Find("Player") as GameObject;
        if (player is null || Vector2.Distance(Transform.Position, player.Transform.Position) > 250)
            Owner.Destroy();

        // "collision", really a trigger, check
        foreach (var sceneObject in SceneObject.FindWithTag("Enemy"))
        {
            if (sceneObject is not GameObject enemy)
                continue;
            if (Vector2.Distance(Transform.Position, enemy.Transform.Position) >= 20)
                continue;

            Owner.Destroy();
            var health = enemy.GetComponent<Health>();
            if (health is not null)
                health.CurrentHealth--;
            foreach (var points in SceneObject.FindComponents<PointsController>())
                points.UpdatePoints(1);
        }
    }
}
