using GalaxEngine;
using Raylib_cs;
namespace SpaceShooter;
public class Level01 : Scene
{
    public Level01() : base(Color.Gray)
    {
        Instantiate(new LevelControllerObject());
        Instantiate(new EnemyGameObject(), new Vector2(300, 100));
    }
}
