namespace RayTracer;
using GalaxEngine;

public class Tracer
{
    public int Width { get; }
    public int Height { get; }
    public Tracer(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public Vector3 Background = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 CameraOrigin = new Vector3(0, 0, -3);
    public Vector3 LightPosition = new Vector3(1, 1.5f, -1);

    public int MaxDepth = 3;
    public List<Triangle> Triangles = 
    [
        new Triangle(
            new Vector3(-0.6f, -0.3f, 0.5f),
            new Vector3(0.4f, -0.3f, 0.5f),
            new Vector3(-0.1f, 0.7f, 0.5f),
            new Vector3(0.2f, 0.4f, 1)),
        new Triangle(
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(0, 0.5f, 0),
            new Vector3(1, 0.5f, 0)),
    ];
    public bool TryHit(Ray ray, out float closest, out Triangle hitTriangle)
    {
        closest = float.PositiveInfinity;
        hitTriangle = default;
        foreach (var triangle in Triangles)
        {
            float t = triangle.IntersectPlane(ray);
            if (!float.IsFinite(t) || t < 0 || t >= closest)
                continue;
            if(!triangle.Contains(ray.PointAt(t)))
                continue;
            closest = t;
            hitTriangle = triangle;
        }
        return closest < float.PositiveInfinity;
    }
    public Vector3 Trace(Ray ray, int depth)
    {
        if(!TryHit(ray, out float t, out Triangle hitTri))
            return Background;
        Vector3 hit = ray.PointAt(t);
        var toLightFull = LightPosition - hit;
        Vector3 toLight = toLightFull.Normalized;

        // diffuse lighting based on angle between normal and light direction
        float diffuse = MathF.Max(0, Vector3.Dot(hitTri.Normal, toLight));
        float ambient = 0.15f;
        // light falloff based on distance
        float distance = toLightFull.Magnitude;
        float falloff = 1 / (1 + 0.3f * distance * distance);
        // shadows
        var shadowRay = new Ray(hit + toLight * 0.001f, toLight);
        bool inShadow = TryHit(shadowRay, out float blockerT, out _) && blockerT < distance;
        if (inShadow)
            diffuse = 0;
        return hitTri.Color * (ambient + diffuse * falloff);
    }
    public Vector3 TracePixel(int px, int py) => Trace(RayForPixel(px, py), MaxDepth);

    public Ray RayForPixel(int px, int py)
    {
        float u = px / (float)(Width - 1);
        float v = py / (float)(Height - 1);
        float x = u * 2 - 1;
        float y = -(v * 2 - 1); 
        Vector3 target = new Vector3(x, y, 0);
        return new Ray(CameraOrigin, (target - CameraOrigin).Normalized);
    }
}