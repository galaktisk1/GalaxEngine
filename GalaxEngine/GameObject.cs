namespace GalaxEngine;
public class GameObject : SceneObject
{
    public Transform Transform { get; }
    public GameObject()
    {
        Transform = new Transform();
        AddComponent(Transform);
    }
    public virtual void Draw()
    {
        foreach (var component in Components)
            component.Draw();
    }
}