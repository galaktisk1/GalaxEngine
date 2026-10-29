using GalaxEngine;
namespace SpaceShooter;
using Raylib_cs;
public class MainScene : Scene
{
    public MainScene()
    {
        var rock = new GameObject();
        rock.AddComponent(new Polygon
        {
            Points = [new(100, 100), new(60,100), new(140,160)],
            FillColor = Color.Gray,
        });
        Instantiate(rock);
        Instantiate(new MainGameObject());
    }
}