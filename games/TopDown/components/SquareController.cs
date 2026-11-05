using GalaxEngine;
using Raylib_cs;
namespace TopDown;

public class SquareController : Component
{
    public float Force = 600;
    private Rigidbody rigidbody = null!;

    public override void Start()
    {
        rigidbody = GetComponent<Rigidbody>()!;
    }
    public override void FixedUpdate()
    {
        var direction = Vector2.Zero;
        if (Input.GetKey(KeyboardKey.Right)) direction += Vector2.Right;
        if (Input.GetKey(KeyboardKey.Left)) direction += Vector2.Left;
        if (Input.GetKey(KeyboardKey.Up)) direction += Vector2.Up;
        if (Input.GetKey(KeyboardKey.Down)) direction += Vector2.Down;
        rigidbody.AddForce(direction.Normalized * Force);
    }
    public override void Update()
    {
        Camera.Main.Transform.Position = Transform.Position;
    }
}
