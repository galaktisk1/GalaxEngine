using GalaxEngine;
using Raylib_cs;
namespace Platformer;
public class SquareGameObject : GameObject
{
    public SquareGameObject() : base("SquareGameObject")
    {
        AddComponent(new Polygon { FillColor = Color.Red, Points = Assets.Square });
        AddComponent<SquareController>();
        AddComponent<Collider>();
        AddComponent<Rigidbody>();
        AddComponent<MovementComponent>(movement => movement.Gravity = 100);
        Transform.Scale = new Vector2(20, 20);
    }
}
