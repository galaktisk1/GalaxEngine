namespace GalaxEngine;
public abstract class Component
{
    public SceneObject Owner = null!;

    public Transform Transform => Owner is GameObject gameObject 
    ? gameObject.Transform
    : throw new InvalidOperationException($"{GetType().Name} needs a GameObject, but it's on a LogicObject");
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Draw() { }
}