using GalaxEngine;
namespace TopDown;
public class FollowerController : Component
{
    // a field, not a property, so it can be passed by ref to SmoothDamp
    private Vector2 currentVelocity = Vector2.Zero;
    public float SmoothTime = 0.5f;
    public float MaxSpeed = 100;
    public Vector2 Offset = new(0, -50);
    private GameObject? target;

    public override void Start()
    {
        target = SceneObject.Find("SquareGameObject") as GameObject;
    }

    public override void Update()
    {
        if (target is null) return;
        var targetPosition = target.Transform.Position + Offset;
        Transform.Position = Vector2.SmoothDamp(Transform.Position, targetPosition, ref currentVelocity, SmoothTime, MaxSpeed, Time.DeltaTime);
    }
}
