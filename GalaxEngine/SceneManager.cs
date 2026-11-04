namespace GalaxEngine;
public static class SceneManager {
    public static Scene CurrentScene { get; private set; } = null!;

    private static Func<Scene>? nextScene;

    internal static void Update() {
        if (nextScene != null) {
            if (CurrentScene != null)
                CurrentScene.Unload();
            CurrentScene = nextScene();
            nextScene = null;
            GC.Collect();
        }
    }

    public static void LoadScene<T>(bool additive = false) where T : Scene, new() {
        if (!additive) {
            nextScene = () => new T();
        }
        else {
            var tempScene = new T();
            foreach (var gameObject in tempScene.SceneObjects) {
                CurrentScene!.SceneObjects.Add(gameObject);
            }
        }
    }
}