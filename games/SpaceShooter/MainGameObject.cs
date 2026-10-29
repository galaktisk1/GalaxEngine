using GalaxEngine;

namespace SpaceShooter;
public class MainGameObject : GameObject
{
    public MainGameObject()
    {
        AddComponent(new UpdateComponent());
        AddComponent(new DrawComponent());
    }
}