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
        UpdateCollisions();
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
    private void UpdateCollisions()
    {
        var colliders = Scene.FindComponents<Collider>();
        for (int i = 0; i < colliders.Count; i++)
        {
            for (int j = i + 1; j < colliders.Count; j++)
            {
                var one = colliders[i];
                var two = colliders[j];
                if (one.Owner is not GameObject gameObjectA) continue;
                if (two.Owner is not GameObject gameObjectB) continue;
                if (gameObjectA == gameObjectB) continue;

                var goA = gameObjectA;
                var goB = gameObjectB;

                var goARigbod = goA.GetComponent<Rigidbody>();
                var goBRigbod = goB.GetComponent<Rigidbody>();

                if (Collisions.PolygonPolygon(gameObjectA, gameObjectB, out var mtv))
                {
                    foreach (var component in gameObjectA.Components)
                        component.OnCollisionStay(gameObjectB, mtv);
                    foreach (var component in gameObjectB.Components)
                        component.OnCollisionStay(gameObjectA, -mtv);
                    
                    if (goARigbod != null)
                        goA.Transform.Position += mtv;
                    if (goBRigbod != null)
                        goB.Transform.Position -= mtv;
                }
            }
        }
    }
}
