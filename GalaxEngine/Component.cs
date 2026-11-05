namespace GalaxEngine;
using System.Collections;
public abstract class Component
{
    public SceneObject Owner { get; internal set; } = null!;
    public bool HasStarted { get; internal set; }
    public bool MarkForDestroy { get; internal set; }
    private readonly List<IEnumerator> coroutines = new List<IEnumerator>();
    private readonly List<IEnumerator> coroutineBuffer = new();

    public GameObject GameObject => Owner as GameObject 
    ?? throw new InvalidOperationException($"{GetType().Name} needs a GameObject, but it's on a LogicObject");
    public Transform Transform => Owner is GameObject gameObject
    ? gameObject.Transform
    : throw new InvalidOperationException($"{GetType().Name} needs a GameObject, but it's on a LogicObject");
    public T? GetComponent<T>() where T : Component => Owner.GetComponent<T>();
    public IEnumerator StartCoroutine(IEnumerator routine)
    {
        if (routine.MoveNext())
            coroutines.Add(routine);
        return routine;
    }
    public void StopCoroutine(IEnumerator routine)
    {
        coroutines.Remove(routine);
    }
    public void StopAllCoroutines()
    {
        coroutines.Clear();
    }
    internal void UpdateCoroutines()
    {
        coroutineBuffer.Clear();
        coroutineBuffer.AddRange(coroutines);
        foreach (var routine in coroutineBuffer)
        {
            if (!coroutines.Contains(routine)) continue;
            if (routine.Current is Wait wait && wait.KeepWaiting) continue;
            if (!routine.MoveNext())
                coroutines.Remove(routine);
        }
    }

    public static void Destroy(Component? component)
    {
        if (component == null || component.MarkForDestroy)
        {
            Console.WriteLine("Component is null or already marked for destroy.");
            return;
        }
        if (component is Transform)
        {
            Console.WriteLine("Cannot destroy Transform component.");
            return;
        }
        component.MarkForDestroy = true;
    }
    // these empty virtual methods function the same as javascripts ?. operator
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Draw() { }
    public virtual void OnDestroy() { }
    public virtual void OnMouseEnter() { }
    public virtual void OnMouseOver() { }
    public virtual void OnMouseExit() { }
    public virtual void OnCollisionEnter(GameObject other, Vector2 mtv) { }
    public virtual void OnCollisionStay(GameObject other, Vector2 mtv) { }
    public virtual void OnCollisionExit(GameObject other, Vector2 mtv) { }
    public virtual void OnOverlapEnter(GameObject other) { }
    public virtual void OnOverlapStay(GameObject other, Vector2 mtv) { }
    public virtual void OnOverlapExit(GameObject other) { }
}

// Below are descriptions for specialized engine-level component types
public abstract class GameplayComponent : Component
{
    private protected GameplayComponent() { }
}

public abstract class EngineComponent : Component
{
    private protected EngineComponent() { }
}