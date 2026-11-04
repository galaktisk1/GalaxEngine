using GalaxEngine;
using Raylib_cs;
namespace SpaceShooter.Sandbox;
public class MainGameObject : GameObject
{
    public MainGameObject()
    {
        AddComponent(new UpdateComponent());
        AddComponent(new Polygon
        {
            Points = [new(0, -100), new(-80, 50), new(-40, 100), new(40, 100), new(80, 50)],
            FillColor = new Color(64, 224, 208, 255),
        });
        AddComponent(new Collider());
        AddComponent(new Rigidbody());
        
        // old draw component
        //AddComponent(new DrawComponent());
    }
}