using GalaxEngine;
using Raylib_cs;
namespace Platformer;
public class SquareController : Component
{
    public float Speed = 50;
    // set every FixedUpdate, then cleared by OnCollisionStay if we're touching something
    private bool hasJumped = false;
    private float lastJumpTime = float.NegativeInfinity;
    private Rigidbody rigidbody = null!;

    public override void Start()
    {
        rigidbody = GetComponent<Rigidbody>()!;
    }

    public override void FixedUpdate()
    {
        if (Input.GetKey(KeyboardKey.Right) || Input.GetKey(KeyboardKey.D))
            Transform.Position.X += Time.DeltaTime * Speed;
        if (Input.GetKey(KeyboardKey.Left) || Input.GetKey(KeyboardKey.A))
            Transform.Position.X -= Time.DeltaTime * Speed;

        hasJumped = true;
    }

    public override void Update()
    {
        if (Input.GetKey(KeyboardKey.Space) && !hasJumped)
        {
            rigidbody.Velocity = new Vector2(rigidbody.Velocity.X, -Speed * 2);
            lastJumpTime = Time.TimeSinceStart;
        }
        // holding jump keeps the upward speed for a moment, so taps hop and holds go higher
        if (Input.GetKey(KeyboardKey.Space) && Time.TimeSinceStart - lastJumpTime < 0.3f)
            rigidbody.Velocity = new Vector2(rigidbody.Velocity.X, -Speed * 2);

        Camera.Main.Transform.Position = Transform.Position;
    }

    public override void OnOverlapStay(GameObject other, Vector2 mtv)
    {
        if (other.Name == "CoinGameObject")
            other.Destroy();
    }

    public override void OnCollisionStay(GameObject other, Vector2 mtv)
    {
        if (mtv.Y < 0)
            hasJumped = false;
        else if (mtv.Y > 0)
            lastJumpTime = float.NegativeInfinity;
    }
}
