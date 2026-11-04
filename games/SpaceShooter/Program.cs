using GalaxEngine;
namespace SpaceShooter;
class Program
{
    static void Main(string[] args)
    {
        Engine.Layers = ["background", "default", "lasers", "ships", Engine.UILayer];
        Engine.Start<TitleScene>();
    }    
}