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
        Instantiate(new MainGameObject());
    }
}