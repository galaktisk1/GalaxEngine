namespace GalaxEngine;
using Raylib_cs;
public class Camera : EngineComponent
{
    public Color BackgroundColor = Color.White;
    public static Camera Main => SceneManager.CurrentScene.MainCamera;
}