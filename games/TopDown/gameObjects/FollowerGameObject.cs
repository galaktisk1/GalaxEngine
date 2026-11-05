using GalaxEngine;
using Raylib_cs;
namespace TopDown;
public class FollowerGameObject : GameObject
{
    public FollowerGameObject() : base("FollowerGameObject")
    {
        AddComponent(new Polygon { FillColor = Color.Blue, Points = Assets.Square });
        AddComponent<FollowerController>();
        Transform.Scale = new Vector2(10, 10);
    }
}
