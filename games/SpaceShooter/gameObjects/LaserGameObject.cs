using GalaxEngine;
using Raylib_cs;
namespace SpaceShooter;
public class LaserGameObject : GameObject
{
    public LaserGameObject() : base("Laser", layer: "lasers")
    {
        AddComponent<LaserController>();
        AddComponent<MovementComponent>();
        AddComponent(new Polygon { FillColor = Color.Red, Points = Assets.Triangle });
        Transform.Scale = new Vector2(3, 8);
    }
}
