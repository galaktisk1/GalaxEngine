using Raylib_cs;
using static Raylib_cs.Raylib;
class Program
{
    static Vector2 position = new Vector2(250, 250);
    static void Main(string[] args)
    {
        InitWindow(500, 500, "WOW");
        SetTargetFPS(60);
        while (!WindowShouldClose())
        {
            Update();
            Draw();
        }
        CloseWindow();
    }

    static void Update()
    {
        position.X += 1;
    }
    static void Draw()
    {
        float cx = position.X, cy = position.Y;
        BeginDrawing();
        ClearBackground(Color.White);
        DrawTriangle(new Vector2(cx, cy - 150), new Vector2(cx - 100, cy + 150), new Vector2(cx + 100, cy + 150), new Color(64, 224, 208, 255));
        EndDrawing();
    }
}