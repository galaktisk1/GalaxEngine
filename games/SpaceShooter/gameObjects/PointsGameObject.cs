using GalaxEngine;
namespace SpaceShooter;
public class PointsGameObject : GameObject
{
    public PointsGameObject() : base("Points", ["Points"], Engine.UILayer)
    {
        AddComponent<PointsController>();
        AddComponent(new TextLabel { Text = "0" });
    }
}
