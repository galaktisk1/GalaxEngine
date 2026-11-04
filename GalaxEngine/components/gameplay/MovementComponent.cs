namespace GalaxEngine;
// Unreal inspired movement component
public class MovementComponent : GameplayComponent
{
    public Vector2 Velocity { get; set; } = Vector2.Zero;

    public override void Update()
    {
        Transform.Position += Velocity * Time.DeltaTime;
    }
}
