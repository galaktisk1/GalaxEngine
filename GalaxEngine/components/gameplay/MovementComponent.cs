namespace GalaxEngine;
// Unreal inspired movement component
// needs a Rigidbody added before it, until components can declare what they require
public class MovementComponent : GameplayComponent
{
    private Rigidbody Rigidbody => GetComponent<Rigidbody>()
        ?? throw new InvalidOperationException($"{nameof(MovementComponent)} on {Owner.Name} needs a Rigidbody");

    public Vector2 Velocity
    {
        get => Rigidbody.Velocity;
        set => Rigidbody.Velocity = value;
    }
    public float Gravity
    {
        get => Rigidbody.Gravity;
        set => Rigidbody.Gravity = value;
    }

    // removes the part of the velocity heading into whatever we hit
    public override void OnCollisionStay(GameObject other, Vector2 mtv)
    {
        var normal = mtv.Normalized;
        float speedIntoNormal = Vector2.Dot(Velocity, normal);
        if (speedIntoNormal < 0)
            Velocity -= normal * speedIntoNormal;
    }
}
