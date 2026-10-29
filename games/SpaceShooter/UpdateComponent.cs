
using GalaxEngine;
using Raylib_cs;

namespace SpaceShooter;
public class UpdateComponent : GameplayComponent
{   
    public Vector2 Position;
    public override void Start()
    {
        Position = Engine.Center;
    }
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