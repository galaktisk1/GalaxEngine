using GalaxEngine;
namespace SpaceShooter;
using Raylib_cs;
public class MainScene : Scene
{
    public MainScene() : base(Color.RayWhite)
    {
        // test go 1
        var planet = new GameObject();
        planet.AddComponent(new Circle
        {
            Radius = 40,
            FillColor = Color.Gray,
        });
        Instantiate(planet);
        // ship game object
        var ship = new MainGameObject();
        ship.AddComponent(new HoverHighlight());
        Instantiate(ship);

        // test go 2 parenting
        var nose = new GameObject();
        nose.Transform.Position = new Vector2(0, -100);
        nose.AddComponent(new Circle
        {
            Radius = 10,
            FillColor = Color.Orange,
        });
        nose.SetParent(ship);
        Instantiate(nose);
        
        // test ui element
        Instantiate(new TestTextLabel());
    }
}