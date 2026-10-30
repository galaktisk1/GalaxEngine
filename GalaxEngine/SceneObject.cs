namespace GalaxEngine;
// scene object is base class for all objects in a scene
// GameObject and LogicObject will inherit from this class
public abstract class SceneObject
{
    public List<Component> Components = new List<Component>();
    public bool MarkForDestroy { get; private set; }
    public SceneObject? Parent { get; private set; }
    private readonly List<SceneObject> children = new List<SceneObject>();
    public IReadOnlyList<SceneObject> Children => children;
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
    public virtual void Start() {
        foreach (var component in Components)
            component.Start();
    }
    public virtual void Update() {
        foreach (var component in Components)
            component.Update();
        // this is seperate from regular Update
        foreach (var component in Components)
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