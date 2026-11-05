using GalaxEngine;
using Raylib_cs;
namespace Platformer;
public class CoinGameObject : GameObject
{
    public CoinGameObject() : base("CoinGameObject")
    {
        AddComponent(new Polygon { FillColor = Color.Orange, Points = Assets.Square });
        AddComponent<Collider>(collider => collider.Response = CollisionResponse.Overlap);
        Transform.Scale = new Vector2(10, 10);
    }
}
