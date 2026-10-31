using GalaxEngine;
public class LaserController : GameplayComponent
{
    public float speed = 5 * 60; 
    public Vector2 Direction = Vector2.Up;

    public override void Update()
    {
        Transform.Position += Direction * speed * Time.DeltaTime;
        if (Transform.Position.Y < -20)
            Owner.Destroy();
    }
}