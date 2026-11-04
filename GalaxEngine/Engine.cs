namespace GalaxEngine;

using Raylib_cs;
using static Raylib_cs.Raylib;
public static class Engine
{
    public static int Width => GetScreenWidth();
    public static int Height => GetScreenHeight();
    public static Vector2 Center => new Vector2(Width / 2f, Height / 2f);
    public static int TargetFPS = 60;
    public static string[] Layers { get; set; } = ["default", "UI"];
    public const string UILayer = "UI";
    private static bool running;
    public static void Start<T>(int width = 800, int height = 600) where T : Scene, new()
    {
        if (running)
            throw new InvalidOperationException("Engine has already been started. Use SceneManager.LoadScene to switch scenes.");
        running = true;
        SceneManager.LoadScene<T>();
        SetConfigFlags(ConfigFlags.ResizableWindow);
        InitWindow(width, height, "GameLoop");
        SetTargetFPS(TargetFPS);
        while (!WindowShouldClose())
        {
            SceneManager.Update();
            Time.Update();
            Input.Update();
            SceneManager.CurrentScene.Update();
            BeginDrawing();
            ClearBackground(Camera.Main.BackgroundColor);
            SceneManager.CurrentScene.Draw();
            // debug fps
            DrawFPS(10, 10);
            EndDrawing();
        }
        SceneManager.CurrentScene.Unload();
        Fonts.UnloadAll();
        CloseWindow();
    }
}