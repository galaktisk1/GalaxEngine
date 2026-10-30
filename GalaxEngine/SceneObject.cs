namespace GalaxEngine;
// scene object is base class for all objects in a scene
// GameObject and LogicObject will inherit from this class
public abstract class SceneObject
{
    public List<Component> Components = new List<Component>();
    public void AddComponent(Component component)
    {
        Components.Add(component);
        component.Owner = this;
    }
    public T? GetComponent<T>() where T : Component
    {
        foreach (var component in Components)
        {
            if (component is T found)
                return found;
        }
        return null;
    }
    public virtual void Start() {
        foreach (var component in Components)
            component.Start();
    }
    public virtual void Update() {
        foreach (var component in Components)
        {
            component.Update();
        }
        // this is seperate from regular Update
        foreach (var component in Components)
            component.UpdateCoroutines();
    }
}