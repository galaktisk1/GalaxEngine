namespace GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class Polygon : EngineComponent
{
    // points *should* be in counter-clockwise order
    // but theres a guard in place to reverse them if they are not
    public List<Vector2> Points = new List<Vector2>();
    public Color FillColor = Color.Magenta;
    public Color StrokeColor = Color.Black;
    public float LineWidth = 0;

    public override void Draw()
    {
        if (Points.Count < 3)
            return;
        var matrix = Transform.WorldMatrix;
        var points = Points.Select(p => System.Numerics.Vector2.Transform(p, matrix)).ToArray();
        // safe guard for if the points are not in counter-clockwise order
        if (SignedArea(points) > 0)
            Array.Reverse(points);
        
        DrawTriangleFan(points, points.Length, FillColor);
        if (LineWidth > 0)
            for (int i = 0; i < points.Length; i++)
            {
                Vector2 start = points[i];
                Vector2 end = points[(i + 1) % points.Length];
                DrawLineEx(start, end, LineWidth, StrokeColor);
            }
    }

    // this is used to determine if the points are in counter-clockwise order
    private static float SignedArea(System.Numerics.Vector2[] points)
    {
        float sum = 0;
        for (int i = 0; i < points.Length; i++)
        {
            var a = points[i];
            var b = points[(i + 1) % points.Length];
            sum += a.X * b.Y - b.X * a.Y;
        }
        return sum;
    }

}