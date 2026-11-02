namespace RayTracer;
using GalaxEngine;

class RenderScene : Scene
{
    public RenderScene()
    {
        var renderer = new GameObject();
        renderer.AddComponent(new RayTracerView());
        Instantiate(renderer);
    }
}