namespace RayTracer;
using GalaxEngine;
class Program
{
    static void Main(string[] args)
    {
        Engine.TargetFPS = 180;
        Engine.Start<RenderScene>();
    }
}
