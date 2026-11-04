namespace GalaxEngine;

public abstract class Scene
{
    public List<SceneObject> SceneObjects = new List<SceneObject>();
    private readonly List<SceneObject> pending = new List<SceneObject>();
    
    // Buffer used to iterate over pending scene objects safely
    // to avoid modification during iteration
    private readonly List<SceneObject> iterationBuffer = new();
    public void Instantiate(SceneObject sceneObject)
    {
        pending.Add(sceneObject);
    }
    public T Instantiate<T>(T gameObject, Vector2 position) where T : GameObject
    {
        gameObject.Transform.Position = position;
        Instantiate(gameObject);
        return gameObject;
    }
    public virtual void Update() {
        iterationBuffer.Clear();
        iterationBuffer.AddRange(pending);
        pending.Clear();
        foreach (var sceneObject in iterationBuffer)
        {
            SceneObjects.Add(sceneObject);
            sceneObject.Start();
        }
        foreach (var sceneObject in SceneObjects)
            sceneObject.Update();

        RemoveDestroyed();
    }
    public virtual void Draw() {
        foreach (var sceneObject in SceneObjects)
            if (sceneObject is GameObject gameObject)
                gameObject.Draw();
    }
    private void RemoveDestroyed()
    {
        foreach (var sceneObject in SceneObjects)
        {
            if (!sceneObject.MarkForDestroy) continue;
            sceneObject.SetParent(null);
            foreach (var component in sceneObject.Components)
                component.OnDestroy();
        }
        SceneObjects.RemoveAll(o => o.MarkForDestroy);
    }

    internal void Unload()
    {
        foreach (var sceneObject in SceneObjects)
        {
            sceneObject.SetParent(null);
            foreach (var component in sceneObject.Components)
                component.OnDestroy();
        }
        SceneObjects.Clear();
        pending.Clear();
    }
}