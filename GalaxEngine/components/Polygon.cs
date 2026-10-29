namespace GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class Polygon : EngineComponent
{
    public List<Vector2> Points = new List<Vector2>();
    public Color FillColor = Color.Magenta;
    public Color StrokeColor = Color.Black;
    public float LineWidth = 0;

    public override void Draw()
    {
        DrawTriangleFan(Points.Select(p => (System.Numerics.Vector2)p).ToArray(), Points.Count, FillColor);
        if (LineWidth > 0)
            for (int i = 0; i < Points.Count; i++)
            {
                Vector2 start = Points[i];
                Vector2 end = Points[(i + 1) % Points.Count];
                DrawLineEx(start, end, LineWidth, StrokeColor);
            }
    }

}