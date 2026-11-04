using GalaxEngine;
using Raylib_cs;
namespace SpaceShooter;
public class UpdateComponent : Component
{
    // 20 frames at 60 fps, the JS cooldown counted frames
    private const float LaserCooldown = 20f / 60f;

    private float speed;
    private float timeSinceLaser;
    private MovementComponent movement = null!;

    public override void Start()
    {
        speed = 2 * 60;
        movement = GetComponent<MovementComponent>()!;
    }

    public override void Update()
    {
        // diagonal movement is faster than straight, same as the JS version at this point
        var velocity = Vector2.Zero;
        if (Input.GetKey(KeyboardKey.Up) || Input.GetKey(KeyboardKey.W))
            velocity.Y -= speed;
        if (Input.GetKey(KeyboardKey.Down) || Input.GetKey(KeyboardKey.S))
            velocity.Y += speed;
        if (Input.GetKey(KeyboardKey.Left) || Input.GetKey(KeyboardKey.A))
            velocity.X -= speed;
        if (Input.GetKey(KeyboardKey.Right) || Input.GetKey(KeyboardKey.D))
            velocity.X += speed;
        movement.Velocity = velocity;

        timeSinceLaser += Time.DeltaTime;
        if (timeSinceLaser > LaserCooldown && Input.GetKey(KeyboardKey.Space))
        {
            timeSinceLaser = 0;
            SceneManager.CurrentScene.Instantiate(new LaserGameObject(), Transform.Position);
        }

        Camera.Main.Transform.Position = Transform.Position;
        Camera.Main.Transform.Scale = new Vector2(1, 1);

        if (Input.GetKeyDown(KeyboardKey.Backspace))
            SceneManager.LoadScene<TitleScene>();
    }

    public override void OnCollisionStay(GameObject other, Vector2 mtv)
    {
        Console.WriteLine($"Collision with {other.Name} with MTV {mtv}");
    }
}
