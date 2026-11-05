using GalaxEngine;
using Raylib_cs;
namespace TopDown;
public class BoxGameObject : GameObject
{
    public BoxGameObject() : base("BoxGameObject")
    {
        AddComponent(new Polygon { FillColor = Color.Orange, Points = Assets.Square });
        AddComponent<Collider>();
        AddComponent<Rigidbody>();
        Transform.Scale = new Vector2(11, 11);
    }
}
