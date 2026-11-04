using GalaxEngine;
using Raylib_cs;
namespace SpaceShooter;
public class HelperGameObject : GameObject
{
    public HelperGameObject() : base("HelperGameObject", layer: "ships")
    {
        AddComponent(new Polygon { FillColor = Color.Purple, Points = Assets.Triangle });
        Transform.Scale = new Vector2(0.25f, 0.25f);
    }
}
