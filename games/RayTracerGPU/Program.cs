namespace RayTracerGPU;
using GalaxEngine;
class Program
{
    static void Main(string[] args)
    {
        Engine.TargetFPS = 0;
        Engine.Start<RenderScene>();
    }
}
