namespace GalaxEngine;
public abstract class SceneObject
{
    public List<Component> Components = new List<Component>();
    public void AddComponent(Component component)
    {
        Components.Add(component);
        component.Owner = this;
    }
    public virtual void Start() {
        foreach (var component in Components)
            component.Start();
    }
    public virtual void Update() {
        foreach (var component in Components)
            component.Update();
    }
}