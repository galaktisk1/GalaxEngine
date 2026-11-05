using GalaxEngine;
using Raylib_cs;
namespace TopDown;
// no Rigidbody, so collisions treat it as infinitely heavy
public class WallGameObject : GameObject
{
    public WallGameObject(Vector2 halfSize) : base("WallGameObject")
    {
        AddComponent(new Polygon { FillColor = Color.DarkGray, Points = Assets.Square });
        AddComponent<Collider>();
        Transform.Scale = halfSize;
    }
}
