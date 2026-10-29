namespace GalaxEngine;
public abstract class Component
{
    public SceneObject Owner = null!;
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Draw() { }
}