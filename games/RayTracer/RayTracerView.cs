namespace RayTracer;
using GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;

class RayTracerView : Component
{
    const int Width = 400;
    const int Height = 400;
    private Color[] pixels = new Color[Width * Height];
    private Texture2D texture;

    public override void Start()
    {
        var image = GenImageColor(Width, Height, Color.Black);
        texture = LoadTextureFromImage(image);
        UnloadImage(image);
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                float u = x / (float)(Width - 1);
                float v = y / (float)(Height - 1);
                pixels[y * Width + x] = new Color((int)(u * 255), (int)(v * 255), 0, 255);
            }
        }
    }
    public override void Draw()
    {
        UpdateTexture(texture, pixels);
        DrawTexture(texture, 0, 0, Color.White);
    }
    public override void OnDestroy()
    {
        Console.WriteLine("Destroying RayTracerView");
        UnloadTexture(texture);
    }
}