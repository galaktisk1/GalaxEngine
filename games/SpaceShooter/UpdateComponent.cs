
using GalaxEngine;
using Raylib_cs;

namespace SpaceShooter;
public class UpdateComponent : Component
{   

    public Vector2 Position = new Vector2(250, 250);
    public override void Update()
    {
        if (Input.GetKey(KeyboardKey.Up) || Input.GetKey(KeyboardKey.W))
            Position.Y -= 1;
        if (Input.GetKey(KeyboardKey.Down) || Input.GetKey(KeyboardKey.S))
            Position.Y += 1;
        if (Input.GetKey(KeyboardKey.Left) || Input.GetKey(KeyboardKey.A))
            Position.X -= 1;
        if (Input.GetKey(KeyboardKey.Right) || Input.GetKey(KeyboardKey.D))
            Position.X += 1;
    }
}