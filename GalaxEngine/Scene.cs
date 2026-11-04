namespace GalaxEngine;

public abstract class Scene
{
    public List<SceneObject> SceneObjects = new List<SceneObject>();
    private readonly List<SceneObject> pending = new List<SceneObject>();

    // Buffer used to iterate over pending scene objects safely
    // to avoid modification during iteration
    private readonly List<SceneObject> iterationBuffer = new();
    public Camera MainCamera { get; }

    protected Scene()
    {
        var cameraObject = new GameObject("MainCamera", ["MainCamera"]);
        MainCamera = cameraObject.AddComponent<Camera>();
        Instantiate(cameraObject);
    }


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

    public SceneObject? Find(string name)
    {
        foreach (var sceneObject in SceneObjects)
            if (sceneObject.Name == name)
                return sceneObject;
        return null;
    }
    public List<SceneObject> FindWithTag(string tag)
    {
        var results = new List<SceneObject>();
        foreach (var sceneObject in SceneObjects)
            if (sceneObject.Tags.Contains(tag))
                results.Add(sceneObject);
        return results;
    }

    public List<T> FindComponents<T>() where T : Component
    {
        var found = new List<T>();
        foreach (var sceneObject in SceneObjects)
            foreach (var component in sceneObject.Components)
                if (component is T matched)
                    found.Add(matched);
        return found;
    }

}