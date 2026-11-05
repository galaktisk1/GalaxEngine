using GalaxEngine;
namespace TopDown;
public class MainScene : Scene
{
    public MainScene()
    {
        Instantiate(new SquareGameObject(), new Vector2(0, 0));
        Instantiate(new BoxGameObject(), new Vector2(100, 0));
        Instantiate(new FollowerGameObject(), new Vector2(50, -50));

        // arena: a 500 x 400 floor, walls 20 thick
        Instantiate(new WallGameObject(new Vector2(270, 10)), new Vector2(0, -210));
        Instantiate(new WallGameObject(new Vector2(270, 10)), new Vector2(0, 210));
        Instantiate(new WallGameObject(new Vector2(10, 200)), new Vector2(-260, 0));
        Instantiate(new WallGameObject(new Vector2(10, 200)), new Vector2(260, 0));
    }
}
