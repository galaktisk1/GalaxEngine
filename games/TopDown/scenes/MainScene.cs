using GalaxEngine;
namespace TopDown;
public class MainScene : Scene
{
    public MainScene()
    {
        Instantiate(new SquareGameObject(), new Vector2(0, 0));
        Instantiate(new BoxGameObject(), new Vector2(100, 0));
        Instantiate(new FollowerGameObject(), new Vector2(50, -50));
    }
}
