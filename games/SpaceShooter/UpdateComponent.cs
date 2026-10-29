
using GalaxEngine;
using Raylib_cs;

namespace SpaceShooter;
public class UpdateComponent : Component
{   

    public Vector2 Position = new Vector2(250, 250);
    public override void Update()
    {
        if (Input.KeysDown.Contains(KeyboardKey.Up) || Input.KeysDown.Contains(KeyboardKey.W))
            Position.Y -= 1;
        if (Input.KeysDown.Contains(KeyboardKey.Down) || Input.KeysDown.Contains(KeyboardKey.S))
            Position.Y += 1;
        if (Input.KeysDown.Contains(KeyboardKey.Left) || Input.KeysDown.Contains(KeyboardKey.A))
            Position.X -= 1;
        if (Input.KeysDown.Contains(KeyboardKey.Right) || Input.KeysDown.Contains(KeyboardKey.D))
            Position.X += 1;
    }
}