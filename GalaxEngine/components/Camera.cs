namespace GalaxEngine;

using Raylib_cs;
public class Camera : EngineComponent
{
    public Color BackgroundColor = Color.White;
    public static Camera Main => SceneManager.CurrentScene.MainCamera;
    public Camera2D Camera2D => new()
    {
        Target = Transform.WorldPosition,
        Offset = Engine.Center,
        Rotation = -Mathf.RadToDeg(Transform.WorldRotation),
        Zoom = 1f / Transform.Scale.X
    };
}