using GalaxEngine;
using Raylib_cs;
namespace SpaceShooter;
// shared level content, loaded additively into Level01 and Level02 by LevelController
public class GenericLevel : Scene
{
    public GenericLevel()
    {
        var main = Instantiate(new PlayerGameObject(), new Vector2(300, 300));
        Instantiate(new PointsGameObject(), new Vector2(10, 10));
        var helper = Instantiate(new HelperGameObject(), new Vector2(2, 1));
        helper.SetParent(main);

        var sceneTimer = new GameObject("SceneTimer", layer: Engine.UILayer);
        sceneTimer.AddComponent(new TextLabel { Color = Color.White, FontSize = 30 });
        sceneTimer.AddComponent<SceneTimerController>();
        Instantiate(sceneTimer, new Vector2(10, 50));

        var wrappedText = new GameObject("WrappedText", layer: Engine.UILayer);
        wrappedText.AddComponent(new TextLabel
        {
            Color = Color.White,
            FontSize = 30,
            MaxWidth = 100,
            Text = "We the People of the United States, in Order to form a more perfect Union, establish Justice, insure domestic Tranquility, provide for the common defence, promote the general Welfare, and secure the Blessings of Liberty to ourselves and our Posterity, do ordain and establish this Constitution for the United States of America.",
        });
        Instantiate(wrappedText, new Vector2(10, 100));
    }
}
