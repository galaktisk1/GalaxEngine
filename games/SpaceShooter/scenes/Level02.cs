using GalaxEngine;
namespace SpaceShooter;
public class Level02 : Scene
{
    public Level02()
    {
        Instantiate(new LevelControllerObject());
        Instantiate(new EnemyGameObject(), new Vector2(300, 100));
        Instantiate(new EnemyGameObject(), new Vector2(200, 100));
    }
}
