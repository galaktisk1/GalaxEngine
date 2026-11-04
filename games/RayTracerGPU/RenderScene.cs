namespace RayTracerGPU;
using GalaxEngine;

class RenderScene : Scene
{
    public RenderScene()
    {
        var renderer = new GameObject(layer: Engine.UILayer);
        renderer.AddComponent(new RayTracerView());
        Instantiate(renderer);
    }
}