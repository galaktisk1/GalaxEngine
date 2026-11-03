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
    public Vector3 CameraOrigin = new Vector3(0, 0, -1);
    
    public Triangle Triangle = new Triangle(
        new Vector3(-0.5f, -0.5f, 0),
        new Vector3(0.5f, -0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(1, 0.5f, 0)
    );

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
        float t = Triangle.IntersectPlane(ray);
        if (!float.IsFinite(t) || t < 0)
            return Background;
        Vector3 hit = ray.PointAt(t);
        if(Triangle.Contains(hit))
            return Triangle.Color;
        return Background;
    }
}