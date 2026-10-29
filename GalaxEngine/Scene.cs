namespace GalaxEngine;

public abstract class Scene
{
    public List<SceneObject> SceneObjects = new List<SceneObject>();
    private readonly List<SceneObject> pending = new List<SceneObject>();
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
        var toStart = pending.ToList(); // Copy and clear first: a Start() may Instantiate, which adds to pending.
        pending.Clear();
        foreach (var sceneObject in toStart)
        {
            SceneObjects.Add(sceneObject);
            sceneObject.Start();
        }
        foreach (var sceneObject in SceneObjects)
            sceneObject.Update();
    }
    public virtual void Draw() {
        foreach (var sceneObject in SceneObjects)
            if (sceneObject is GameObject gameObject)
                gameObject.Draw();
    }
}