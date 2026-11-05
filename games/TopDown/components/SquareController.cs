using GalaxEngine;
using Raylib_cs;
namespace TopDown;
public class SquareController : Component
{
    public float Speed = 50;

    public override void FixedUpdate()
    {
        if (Input.GetKey(KeyboardKey.Right))
            Transform.Position.X += Time.DeltaTime * Speed;
        if (Input.GetKey(KeyboardKey.Left))
            Transform.Position.X -= Time.DeltaTime * Speed;
        if (Input.GetKey(KeyboardKey.Up))
            Transform.Position.Y -= Time.DeltaTime * Speed;
        if (Input.GetKey(KeyboardKey.Down))
            Transform.Position.Y += Time.DeltaTime * Speed;
    }
}
