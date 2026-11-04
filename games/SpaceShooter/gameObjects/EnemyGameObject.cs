using GalaxEngine;
using Raylib_cs;
namespace SpaceShooter;
public class EnemyGameObject : GameObject
{
    public EnemyGameObject() : base("Enemy", ["Enemy"], "ships")
    {
        AddComponent<MovementComponent>();
        AddComponent<Collider>();
        AddComponent(new Polygon { FillColor = Color.Green, Points = Assets.Triangle });
        Transform.Scale = new Vector2(20, 20);
        Transform.Rotation = MathF.PI;
        AddComponent<EnemyController>();
        AddComponent<Health>();
    }
}
