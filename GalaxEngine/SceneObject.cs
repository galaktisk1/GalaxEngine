namespace GalaxEngine;
using System.Reflection;
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
    private static readonly Dictionary<(Type, string), MethodInfo?> methodCache = new();

    protected SceneObject(string name = "", string[]? tags = null)
    {
        Name = name;
        foreach (var tag in tags ?? [])
            Tags.Add(tag);
    }
    // This is for the AddComponent(new Component()) syntax
    public T AddComponent<T>(T component, Action<T>? configure = null) where T : Component
    {

        if (component.Owner != null)
            throw new InvalidOperationException("Component already has an owner.");
        component.Owner = this;
        // equivalent to Object.assign(component, configure) in JavaScript
        configure?.Invoke(component);
        components.Add(component);
        return component;
    }

    // This is for the AddComponent<Component>() syntax
    public T AddComponent<T>(Action<T>? configure = null) where T : Component, new()
    {
        return AddComponent(new T(), configure);
    }
    // This is for the GetComponent<Component>() syntax
    public T? GetComponent<T>() where T : Component
    {
        foreach (var component in components)
        {
            if (component is T found)
                return found;
        }
        return null;
    }
    // This is for the GetComponent(typeof(Component)) syntax
    public Component? GetComponent(Type type)
    {
        foreach (var component in components)
        {
            if (type.IsInstanceOfType(component))
                return component;
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

    // Broadcasts a message to all components by invoking the given action.
    // This is for the BroadcastMessage(c => c.Method()) syntax
    public void BroadcastMessage(Action<Component> message)
    {
        for (int i = 0; i < components.Count; i++)
            message(components[i]);
    }

    // Broadcasts a message to all components by invoking a method with the given name and arguments.
    // This is for the BroadcastMessage("Method", args) syntax
    public void BroadcastMessage(string methodName, params object[] args)
    {
        for (int i = 0; i < components.Count; i++)
        {
            var component = components[i];
            var key = (component.GetType(), methodName);
            if (!methodCache.TryGetValue(key, out var method))
            {
                method = key.Item1.GetMethod(methodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                methodCache[key] = method;
            }
            method?.Invoke(component, args);
        }
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
    internal virtual void Update()
    {
        Start();
        iterationBuffer.Clear();
        iterationBuffer.AddRange(components);
        foreach (var component in iterationBuffer)
            component.Update();
        // this is seperate from regular Update
        foreach (var component in iterationBuffer)
            component.UpdateCoroutines();
    }
    internal virtual void FixedUpdate()
    {
        Start();
        iterationBuffer.Clear();
        iterationBuffer.AddRange(components);
        foreach (var component in iterationBuffer)
            component.FixedUpdate();
    }
    public void Destroy()
    {
        if (MarkForDestroy) return;
        MarkForDestroy = true;
        foreach (var child in children)
            child.Destroy();
    }

    internal void RemoveDestroyedComponents()
    {
        for (int i = components.Count - 1; i >= 0; i--)
        {
            if (components[i].MarkForDestroy)
            {
                components[i].OnDestroy();
                components.RemoveAt(i);
            }
        }
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