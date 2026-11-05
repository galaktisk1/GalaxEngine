using GalaxEngine;
using Raylib_cs;
namespace Platformer;
public class SquareController : Component
{
    public float Speed = 50;

    public override void Update()
    {
        if (Input.GetKey(KeyboardKey.Right))
            Transform.Position.X += Time.DeltaTime * Speed;
        if (Input.GetKey(KeyboardKey.Left))
            Transform.Position.X -= Time.DeltaTime * Speed;
    }

    public override void OnCollisionStay(GameObject other, Vector2 mtv)
    {
        var rigidbody = GetComponent<Rigidbody>()!;
        // Velocity is a struct behind a property, so Y can't be set on its own
        // rigidbody.Velocity = new Vector2(rigidbody.Velocity.X, 0);
    }
}
