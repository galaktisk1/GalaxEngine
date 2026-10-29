namespace GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;
public static class Engine
{
    public static Scene CurrentScene = null!;
    public static int Width => GetScreenWidth();
    public static int Height => GetScreenHeight();
    public static Vector2 Center => new Vector2(Width / 2f, Height / 2f);
    public static void Start<T>(int width = 800, int height = 600) where T : Scene, new()
    {
        CurrentScene = new T();
        SetConfigFlags(ConfigFlags.ResizableWindow);
        InitWindow(width, height, "GameLoop");
        SetTargetFPS(60);
        CurrentScene.Start();
        while (!WindowShouldClose())
        {
            Input.Update();
            CurrentScene.Update();
            BeginDrawing();
            ClearBackground(Color.White);
            CurrentScene.Draw();
            EndDrawing();
        }
        CloseWindow();
    }
}