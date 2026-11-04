namespace GalaxEngine;

using Raylib_cs;
using static Raylib_cs.Raylib;
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
    public Vector2 ScreenToWorld(Vector2 screen) => GetScreenToWorld2D(screen, Camera2D);
    public Vector2 WorldToScreen(Vector2 world) => GetWorldToScreen2D(world, Camera2D);
}