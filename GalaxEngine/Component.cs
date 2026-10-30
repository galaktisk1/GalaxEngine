namespace GalaxEngine;
using System.Collections;
public abstract class Component
{
    public SceneObject Owner = null!;
    private readonly List<IEnumerator> coroutines = new List<IEnumerator>();

    public Transform Transform => Owner is GameObject gameObject 
    ? gameObject.Transform
    : throw new InvalidOperationException($"{GetType().Name} needs a GameObject, but it's on a LogicObject");
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
        foreach (var routine in coroutines.ToList())
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
}