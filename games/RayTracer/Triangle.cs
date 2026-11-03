namespace RayTracer;
using GalaxEngine;
public struct Triangle
{
    public Vector3 A { get; }
    public Vector3 B { get; }
    public Vector3 C { get; }
    public Vector3 Color { get; }

    public Triangle(Vector3 a, Vector3 b, Vector3 c, Vector3 color)
    {
        A = a;
        B = b;
        C = c;
        Color = color;
    }
    public Triangle RotatedY(float angle) => new Triangle(
        RotateY(A, angle),
        RotateY(B, angle),
        RotateY(C, angle),
        Color
    );
    public Vector3 Normal => Vector3.Cross(A - B, C - B).Normalized;
    public float D => -Vector3.Dot(Normal, A);
    public float IntersectPlane(Ray ray)
    {
    return (-D - Vector3.Dot(ray.Origin, Normal)) / Vector3.Dot(ray.Direction, Normal);
    }

    public bool Contains(Vector3 p)
    {
        float full = Vector3.Cross(B - A, C - A).Magnitude;
        float a = Vector3.Cross(B - p, C - p).Magnitude / full;
        float b = Vector3.Cross(A - p, C - p).Magnitude / full;
        float c = Vector3.Cross(A - p, B - p).Magnitude / full;
        return a + b + c <= 1.001f;
    }
    private static Vector3 RotateY(Vector3 v, float angle)
    {
        float cos = MathF.Cos(angle), sin = MathF.Sin(angle);
        return new Vector3(
            v.X * cos + v.Z * sin,
            v.Y,
            -v.X * sin + v.Z * cos
        );
    }
}