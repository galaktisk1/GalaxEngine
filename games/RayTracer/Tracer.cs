namespace RayTracer;
using GalaxEngine;
using System.Collections.Generic;
using System.ComponentModel;

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
    public Vector3 LightPosition = new Vector3(0, 3, 0);
    
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

    public Ray RayForPixel(int px, int py)
    {
        float u = px / (float)(Width - 1);
        float v = py / (float)(Height - 1);
        float x = u * 2 - 1;
        float y = -(v * 2 - 1); 
        Vector3 target = new Vector3(x, y, 0);
        return new Ray(CameraOrigin, (target - CameraOrigin).Normalized);
    }
    
    public Vector3 TracePixel(int px, int py)
    {
        // gets the pixel's ray and returns a color.
        Ray ray = RayForPixel(px, py);
        float closest = float.PositiveInfinity;
        Triangle? hitTriangle = null;
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
        if (hitTriangle is not Triangle hitTri)
            return Background;
        Vector3 hit = ray.PointAt(closest);
        Vector3 toLight = (LightPosition - hit).Normalized;
        float diffuse = MathF.Max(0, Vector3.Dot(hitTri.Normal, toLight));
        float ambient = 0.15f;
        return hitTri.Color * (ambient + diffuse);
    }
}