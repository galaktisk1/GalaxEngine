using GalaxEngine;
using Raylib_cs;
namespace TopDown;
public class SquareGameObject : GameObject
{
    public SquareGameObject() : base("SquareGameObject")
    {
        AddComponent(new Polygon { FillColor = Color.Red, Points = Assets.Square });
        AddComponent<SquareController>();
        AddComponent<Collider>();
        AddComponent<Rigidbody>();
        Transform.Scale = new Vector2(20, 20);
    }
}
