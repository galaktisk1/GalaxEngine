using Raylib_cs;
using static Raylib_cs.Raylib;
using GalaxEngine;
class Program
{
    static Vector2 position = new Vector2(250, 250);
    static void Main(string[] args)
    {
        Engine.Start(Update, Draw);
    }

    static void Update()
    {
        if (Input.KeysDown.Contains(KeyboardKey.Up) || Input.KeysDown.Contains(KeyboardKey.W))
            position.Y -= 1;
        if (Input.KeysDown.Contains(KeyboardKey.Down) || Input.KeysDown.Contains(KeyboardKey.S))
            position.Y += 1;
        if (Input.KeysDown.Contains(KeyboardKey.Left) || Input.KeysDown.Contains(KeyboardKey.A))
            position.X -= 1;
        if (Input.KeysDown.Contains(KeyboardKey.Right) || Input.KeysDown.Contains(KeyboardKey.D))
            position.X += 1;
    }

    static void Draw()
    {
        float cx = position.X, cy = position.Y;
        DrawTriangle(new Vector2(cx, cy - 150), new Vector2(cx - 100, cy + 150), new Vector2(cx + 100, cy + 150), new Color(64, 224, 208, 255));
    }
}