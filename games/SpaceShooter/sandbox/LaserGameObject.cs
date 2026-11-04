using GalaxEngine;
using Raylib_cs;
namespace SpaceShooter.Sandbox;
public class LaserGameObject : GameObject
{
    public LaserGameObject()
    {
        AddComponent(new LaserController());
        AddComponent(new Polygon
        {
            Points = [new(-2, -10), new(2, -10), new(2, 10), new(-2, 10)],
            FillColor = Color.Red
        });
    }
}