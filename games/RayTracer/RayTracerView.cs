namespace RayTracer;
using GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;

class RayTracerView : Component
{
    private Triangle baseTriangle;
    const int Width = 400;
    const int Height = 400;
    private Color[] pixels = new Color[Width * Height];
    private Texture2D texture;
    private Tracer tracer = new Tracer(Width, Height);
    private Triangle currentTriangle; 

    public override void Start()
    {
        var image = GenImageColor(Width, Height, Color.Black);
        texture = LoadTextureFromImage(image);
        UnloadImage(image);
        baseTriangle = new Triangle(
        new Vector3(-0.5f, -0.5f, 0),
        new Vector3(0.5f, -0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(1, 0.5f, 0)
    );
    }

    private void Render()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                pixels[y * Width + x] = ToColor(tracer.TracePixel(x, y));
            }
        }
    }

    public override void Update()
    {
        currentTriangle = baseTriangle.RotatedY(Time.TimeSinceStart);
        tracer.Triangle = currentTriangle;
        Render();
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

    private static Color ToColor(Vector3 c) => new Color(
    (int)(Math.Clamp(c.X, 0, 1) * 255),
    (int)(Math.Clamp(c.Y, 0, 1) * 255),
    (int)(Math.Clamp(c.Z, 0, 1) * 255),
    255);
}