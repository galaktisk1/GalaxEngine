using GalaxEngine;
namespace SpaceShooter;
using Raylib_cs;
public class MainScene : Scene
{
    public MainScene()
    {
        var planet = new GameObject();
        planet.AddComponent(new Circle
        {
            Radius = 40,
            FillColor = Color.Gray,
        });
        Instantiate(planet);
        var ship = new MainGameObject();
        Instantiate(ship);
        var nose = new GameObject();
        nose.Transform.Position = new Vector2(0, -100);
        nose.AddComponent(new Circle
        {
            Radius = 10,
            FillColor = Color.Orange,
        });
        nose.SetParent(ship);
        Instantiate(nose);
    }
}