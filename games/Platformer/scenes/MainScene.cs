using GalaxEngine;
namespace Platformer;
public class MainScene : Scene
{
    public MainScene()
    {
        Instantiate(new SquareGameObject(), new Vector2(0, 0));
        Instantiate(new PlatformGameObject(), new Vector2(0, 50));
    }
}
