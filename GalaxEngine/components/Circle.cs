namespace GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class Circle : EngineComponent
{
    public float Radius = 10f;
    public Color FillColor = Color.Magenta;

    public override void Draw()
    {
        DrawCircleV(Transform.WorldPosition, Radius * Transform.Scale.X, FillColor);
    }
}