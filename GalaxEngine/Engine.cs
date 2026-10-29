namespace GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;
public static class Engine
{
    public static void Start(Action update, Action draw)
    {
        InitWindow(500, 500, "WOW");
        SetTargetFPS(60);
        while (!WindowShouldClose())
        {
            update();
            BeginDrawing();
            ClearBackground(Color.White);
            draw();
            EndDrawing();
        }
        CloseWindow();
    }
}