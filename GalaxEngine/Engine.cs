namespace GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;
public static class Engine
{
    public static Scene CurrentScene = null!;
    public static void Start<T>() where T : Scene, new()
    {
        CurrentScene = new T();
        InitWindow(500, 500, "WOW");
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