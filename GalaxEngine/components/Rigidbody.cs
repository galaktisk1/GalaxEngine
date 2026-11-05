namespace GalaxEngine;

public class Rigidbody : EngineComponent
{
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    public float Gravity { get; set; } = 0;

    public override void FixedUpdate()
    {
        Velocity += new Vector2(0, Gravity) * Time.DeltaTime;
        Transform.Position += Velocity * Time.DeltaTime;
    }
}