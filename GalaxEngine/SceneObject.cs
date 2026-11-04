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
    // Buffer used to iterate over components safely
    // to avoid modification during iteration
    private readonly List<Component> iterationBuffer = new();
    
    public void AddComponent(Component component)
    {
        if (component.Owner != null)
            throw new InvalidOperationException("Component already has an owner.");
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
        iterationBuffer.Clear();
        iterationBuffer.AddRange(components);
        foreach (var component in iterationBuffer)
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
        iterationBuffer.Clear();
        iterationBuffer.AddRange(components);
        foreach (var component in iterationBuffer)
            component.Update();
        // this is seperate from regular Update
        foreach (var component in iterationBuffer)
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

public class LogicObject : SceneObject
{
    // no specific code yet
}

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