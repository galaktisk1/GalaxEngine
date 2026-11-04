namespace GalaxEngine;
using System.Collections;
public abstract class Component
{
    public SceneObject Owner { get; internal set; } = null!;
    public bool HasStarted { get; internal set; }
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

    // these empty virtual methods function the same as javascripts ?. operator
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Draw() { }
    public virtual void OnDestroy() { }
    public virtual void OnMouseOver() { }
    public virtual void OnCollisionStay(GameObject other, Vector2 mtv) { }
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