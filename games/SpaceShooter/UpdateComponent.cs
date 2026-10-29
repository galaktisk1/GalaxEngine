
using GalaxEngine;
using Raylib_cs;

namespace SpaceShooter;
public class UpdateComponent : GameplayComponent
{   
    public float speed = 2;
    public override void Start()
    {
        Transform.Position = Engine.Center;
    }
    public override void Update()
    {
        var direction = Vector2.Zero;
        if (Input.GetKey(KeyboardKey.Up) || Input.GetKey(KeyboardKey.W))
            direction += Vector2.Up;
        if (Input.GetKey(KeyboardKey.Down) || Input.GetKey(KeyboardKey.S))
            direction += Vector2.Down;
        if (Input.GetKey(KeyboardKey.Left) || Input.GetKey(KeyboardKey.A))
            direction += Vector2.Left;
        if (Input.GetKey(KeyboardKey.Right) || Input.GetKey(KeyboardKey.D))
            direction += Vector2.Right;
        Transform.Position += direction.Normalized * speed * Time.DeltaTime;
        direction = Vector2.Zero;
    }
}