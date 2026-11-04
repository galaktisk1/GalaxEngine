namespace GalaxEngine;
// scene object is base class for all objects in a scene
// GameObject and LogicObject will inherit from this class
public abstract class SceneObject
{
    public string Name { get; set; } = "";
    public HashSet<string> Tags { get; } = new();
    
    public bool MarkForDestroy { get; private set; }
    
    public IReadOnlyList<Component> Components => components;
    private readonly List<Component> components = new List<Component>();
    
    public SceneObject? Parent { get; private set; }
    private readonly List<SceneObject> children = new List<SceneObject>();
    public IReadOnlyList<SceneObject> Children => children;
    
    // Buffer used to iterate over components safely
    // to avoid modification during iteration
    private readonly List<Component> iterationBuffer = new();

    protected SceneObject(string name = "", string[]? tags = null)
    {
        Name = name;
        foreach (var tag in tags ?? [])
            Tags.Add(tag);
    }
    public T AddComponent<T>(T component, Action<T>? configure = null) where T : Component
    {
        // This is for the AddComponent(new Component()) syntax
        if (component.Owner != null)
            throw new InvalidOperationException("Component already has an owner.");
        component.Owner = this;
        // equivalent to Object.assign(component, configure) in JavaScript
        configure?.Invoke(component);
        components.Add(component);
        return component;
    }

    public T AddComponent<T>(Action<T>? configure = null) where T : Component, new()
        // This is for the AddComponent<Component>() syntax
        => AddComponent(new T(), configure);
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

    public static SceneObject? Find(string name) => SceneManager.CurrentScene.Find(name);
    public static List<SceneObject> FindWithTag(string tag) => SceneManager.CurrentScene.FindWithTag(tag);
    public static List<T> FindComponents<T>() where T : Component => SceneManager.CurrentScene.FindComponents<T>();
}

public class LogicObject : SceneObject
{
    public LogicObject(string name = "", string[]? tags = null) : base(name, tags) { }
}

public class GameObject : SceneObject
{
    public Transform Transform { get; }
    public string Layer { get; set; }
    public GameObject(string name = "", string[]? tags = null, string layer = "default") : base(name, tags)
    {
        Layer = layer;
        Transform = new Transform();
        AddComponent(Transform);
    }
    public virtual void Draw()
    {
        foreach (var component in Components)
            component.Draw();
    }
}