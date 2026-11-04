namespace SpaceShooter;
using GalaxEngine;
using Raylib_cs;

public class TitleController : Component
{
    public override void Update()
    {
        if (Input.GetKeyUp(KeyboardKey.Space))
            SceneManager.LoadScene<Level01>();
        if (Input.GetKeyUp(KeyboardKey.Tab))
            SceneManager.LoadScene<Sandbox.MainScene>();
    }
}