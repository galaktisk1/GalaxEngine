using GalaxEngine;
using Raylib_cs;
namespace SpaceShooter;
public class PlayerGameObject : GameObject
{
    public PlayerGameObject() : base("Player", layer: "ships")
    {
        AddComponent<UpdateComponent>();
        AddComponent<Collider>();
        AddComponent<MovementComponent>();
        AddComponent(new Polygon { FillColor = Color.Black, Points = Assets.Triangle });
        Transform.Scale = new Vector2(30, 30);
    }
}
