namespace GalaxEngine;

public abstract class Scene
{
    public List<SceneObject> SceneObjects = new List<SceneObject>();
    public void Instantiate(SceneObject sceneObject)
    {
        SceneObjects.Add(sceneObject);
    }
    public virtual void Start() {
        foreach (var sceneObject in SceneObjects)
            sceneObject.Start();
    }
    public virtual void Update() {
        foreach (var sceneObject in SceneObjects)
            sceneObject.Update();
    }
    public virtual void Draw() {
        foreach (var sceneObject in SceneObjects)
            if (sceneObject is GameObject gameObject)
                gameObject.Draw();
    }
}