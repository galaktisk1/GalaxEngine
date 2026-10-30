using GalaxEngine;
public class LaserController : GameplayComponent
{
    public float speed = 5 * 60; 

    public override void Update()
    {
        Transform.Position += Vector2.Up * speed * Time.DeltaTime;
    }
}