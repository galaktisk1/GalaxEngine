using GalaxEngine;
using Raylib_cs;
namespace TopDown;
public class BoxGameObject : GameObject
{
    public BoxGameObject() : base("BoxGameObject")
    {
        AddComponent(new Polygon { FillColor = Color.Orange, Points = Assets.Square });
        AddComponent<Collider>();
        AddComponent<Rigidbody>(rb => { rb.Mass = 2; rb.Friction = 0.3f; rb.StaticFriction = 0.4f; rb.Bounciness = 0.8f; });
        Transform.Scale = new Vector2(11, 11);
    }
}
