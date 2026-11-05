using GalaxEngine;
namespace TopDown;
class Program
{
    static void Main(string[] args)
    {
        Engine.Layers = ["background", "midground", "foreground", "default", Engine.UILayer];
        Engine.Start<MainScene>();
    }
}
