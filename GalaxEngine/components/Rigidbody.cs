namespace GalaxEngine;

public class Rigidbody : EngineComponent
{
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    public float Mass { get; set; } = 1;
    public float Gravity { get; set; } = 0;
    public float Friction { get; set; } = 0;
    public float StaticFriction { get; set; } = 0;
    public float Bounciness { get; set; } = 0;
    public float InverseMass => Mass > 0 ? 1f / Mass : 0f;
    private Vector2 force;

    public void AddForce(Vector2 f) => force += f;
    public void AddImpulse(Vector2 impulse) => Velocity += impulse / Mass;
    public Vector2 Momentum => Velocity * Mass;
    internal void Integrate(float dt, float floorGravity)
    {
        // normal force for friction calculation
        float normal = Mass * floorGravity;
        bool atRest = Velocity.SqrMagnitude < 0.0001f;
        if (atRest)
        {
            if (force.Magnitude <= StaticFriction * normal)
                force = Vector2.Zero;
            else
                force -= force.Normalized * Friction * normal;
        }
        else
            force += -Velocity.Normalized * Friction * normal;
        // f = ma, where a is the acceleration due to all forces including friction and gravity
        var acceleration = force / Mass + new Vector2(0, Gravity);
        var oldVelocity = Velocity;
        // Velocity update based on acceleration and time 
        Velocity += acceleration * dt;
        // if the velocity has reversed direction due to friction, stop the object
        if (Vector2.Dot(Velocity, oldVelocity) < 0)
            Velocity = Vector2.Zero;
        Transform.Position += Velocity * dt;
        force = Vector2.Zero;
    }
}