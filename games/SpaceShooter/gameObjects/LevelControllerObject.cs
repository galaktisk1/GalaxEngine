using GalaxEngine;
namespace SpaceShooter;
// a LogicObject: it only runs level logic, it has no position and draws nothing
public class LevelControllerObject : LogicObject
{
    public LevelControllerObject() : base("LevelController")
    {
        AddComponent<LevelController>();
    }
}
