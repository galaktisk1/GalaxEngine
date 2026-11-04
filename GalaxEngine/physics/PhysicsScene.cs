namespace GalaxEngine;
public class PhysicsScene
{
    public Scene Scene { get; }

    public PhysicsScene(Scene scene)
    {
        Scene = scene;
    }

    internal void Step()
    {
        UpdateMouseOver();
    }

    private void UpdateMouseOver()
    {
        foreach (var collider in Scene.FindComponents<Collider>())
        {
            if (collider.Owner is not GameObject gameObject) continue;

            var point = gameObject.Layer == Engine.UILayer ? Input.MousePosition : Input.MouseWorldPosition;
            if (!Collisions.ContainsPoint(point, gameObject)) continue;

            foreach (var component in gameObject.Components)
                component.OnMouseOver();
        }
    }
}
