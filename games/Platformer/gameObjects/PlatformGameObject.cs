using GalaxEngine;
using Raylib_cs;
namespace Platformer;
public class PlatformGameObject : GameObject
{
    public PlatformGameObject() : base("PlatformGameObject")
    {
        AddComponent(new Polygon { FillColor = Color.Blue, Points = Assets.Square });
        AddComponent<Collider>();
        Transform.Scale = new Vector2(350, 20);
    }
}
