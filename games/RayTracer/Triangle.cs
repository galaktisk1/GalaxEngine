namespace RayTracer;
using GalaxEngine;
public struct Triangle
{
    public Vector3 A { get; }
    public Vector3 B { get; }
    public Vector3 C { get; }
    public Vector3 Color { get; }
    public float Reflectivity { get; }

    public Triangle(Vector3 a, Vector3 b, Vector3 c, Vector3 color, float reflectivity = 0.0f)
    {
        A = a;
        B = b;
        C = c;
        Color = color;
        Reflectivity = reflectivity;
        Normal = Vector3.Cross(A - B, C - B).Normalized;
        D = -Vector3.Dot(Normal, A);
        Area = Vector3.Cross(B - A, C - A).Magnitude;
    }
    public Triangle RotatedY(float angle) => new Triangle(
        RotateY(A, angle),
        RotateY(B, angle),
        RotateY(C, angle),
        Color,
        Reflectivity
    );
    public Vector3 Normal {get;}
    public float D {get;}
    public float Area {get;}
    public float IntersectPlane(Ray ray)
    {
    return (-D - Vector3.Dot(ray.Origin, Normal)) / Vector3.Dot(ray.Direction, Normal);
    }

    public bool Contains(Vector3 p)
    {
        float a = Vector3.Cross(B - p, C - p).Magnitude / Area;
        float b = Vector3.Cross(A - p, C - p).Magnitude / Area;
        float c = Vector3.Cross(A - p, B - p).Magnitude / Area;
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