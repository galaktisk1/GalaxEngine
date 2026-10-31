using GalaxEngine;
namespace SpaceShooter;
public class TestTextLabel : GameObject
{
    public TestTextLabel()
    {
        AddComponent(new TextLabel() { 
            FontPath = "assets/fonts/Roboto.ttf",
            Text = "Hello World",
            FontSize = 32
        });
        
        Transform.Position = new Vector2(100, 100);
    }
}