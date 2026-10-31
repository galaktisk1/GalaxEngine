namespace GalaxEngine;
// scene object is base class for all objects in a scene
// GameObject and LogicObject will inherit from this class
public abstract class SceneObject
{
    public IReadOnlyList<Component> Components => components;
    private readonly List<Component> components = new List<Component>();
    public bool MarkForDestroy { get; private set; }
    public SceneObject? Parent { get; private set; }
    private readonly List<SceneObject> children = new List<SceneObject>();
    public IReadOnlyList<SceneObject> Children => children;
    public void AddComponent(Component component)
    {
        components.Add(component);
        component.Owner = this;
    }
    public T? GetComponent<T>() where T : Component
    {
        foreach (var component in components)
        {
            if (component is T found)
                return found;
        }
        return null;
    }
    public void SetParent(SceneObject? newParent)
    {
        if (Parent == newParent) return;
        for (var p = newParent; p != null; p = p.Parent)
            if (p == this)
                throw new InvalidOperationException("Cannot set parent to a child of this object.");
        Parent?.children.Remove(this);
        Parent = newParent;
        Parent?.children.Add(this);
    }
    internal virtual void Start() {
        foreach (var component in components.ToList())
        {
            if (component.HasStarted) continue;
            component.HasStarted = true;
            component.Start();
        }
    }
    internal virtual void Update() {
        // something to know for later, this doesnt scale well
        // its fine for so many components, 
        // but could become a performance issue with a large number of components
        Start();
        foreach (var component in components.ToList())
            component.Update();
        // this is seperate from regular Update
        foreach (var component in components.ToList())
            component.UpdateCoroutines();
    }
    public void Destroy()
    {
        if (MarkForDestroy) return;
        MarkForDestroy = true;
        foreach (var child in children)
            child.Destroy();
    }
}