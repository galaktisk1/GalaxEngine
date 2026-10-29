namespace GalaxEngine;
public class GameObject : SceneObject
{
    public virtual void Draw()
    {
        foreach (var component in Components)
            component.Draw();
    }
}