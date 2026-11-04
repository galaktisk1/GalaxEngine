using GalaxEngine;
namespace SpaceShooter;
public class LevelController : Component
{
    public override void Start()
    {
        SceneManager.LoadScene<GenericLevel>(additive: true);
    }

    public override void Update()
    {
        if (SceneObject.Find("Enemy") is null)
            SceneManager.LoadScene<Level02>();
    }
}
