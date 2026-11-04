namespace SpaceShooter;
using GalaxEngine;
public class TitleScene : Scene
{
    public TitleScene()
    {
        var center = Engine.Center;

        var controller = new LogicObject();
        controller.AddComponent(new TitleController());
        Instantiate(controller);
        
        var txt = new GameObject();
        txt.AddComponent(new TextLabel() {
            Text = "Space Shooter",
            FontSize = 48,
            Align = TextAlign.Center
        });
        var pos = center + new Vector2(0, -40);
        Instantiate(txt, pos);
        
        var txt2 = new GameObject();
        txt2.AddComponent(new TextLabel() {
            Text = "Press Space to Start",
            FontSize = 20,
            Align = TextAlign.Center
        });
        var pos2 = center + new Vector2(0, 40);
        Instantiate(txt2, pos2);
    }
}