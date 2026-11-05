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
        AddComponent<Rigidbody>(rb => {rb.Mass = 1; rb.Friction = 0.3f; rb.StaticFriction = 0.4f; rb.Bounciness = 0.2f; });
        Transform.Scale = new Vector2(20, 20);
    }
}
