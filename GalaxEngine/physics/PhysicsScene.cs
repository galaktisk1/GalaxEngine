namespace GalaxEngine;

public class PhysicsScene
{
    public Scene Scene { get; }
    private GameObject? lastMouseOverObject;

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
        GameObject? hovered = null;
        foreach (var collider in Scene.FindComponents<Collider>())
        {
            if (collider.Owner is not GameObject gameObject) continue;
            var point = gameObject.Layer == Engine.UILayer ? Input.MousePosition : Input.MouseWorldPosition;
            if (Collisions.ContainsPoint(point, gameObject))
                hovered = gameObject;
        }

        if (hovered != lastMouseOverObject)
        {
            lastMouseOverObject?.BroadcastMessage(c => c.OnMouseExit());
            hovered?.BroadcastMessage(c => c.OnMouseEnter());
        }
        hovered?.BroadcastMessage(c => c.OnMouseOver());
        lastMouseOverObject = hovered;
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
                if (one.Owner is not GameObject goA) continue;
                if (two.Owner is not GameObject goB) continue;
                if (goA == goB) continue;
                // if either collider is block or overlap, we need to check for collisions
                var response = one.Response < two.Response ? one.Response : two.Response;
                // Skip collision check if either collider is set to ignore
                if (response == CollisionResponse.Ignore) continue;

                var goARigbod = goA.GetComponent<Rigidbody>();
                var goBRigbod = goB.GetComponent<Rigidbody>();
                if (goARigbod == null && goBRigbod == null) continue;

                if (Collisions.PolygonPolygon(goA, goB, out var mtv))
                {
                    if (response == CollisionResponse.Block)
                    {
                        goA.BroadcastMessage(c => c.OnCollisionStay(goB, mtv));
                        goB.BroadcastMessage(c => c.OnCollisionStay(goA, -mtv));
                        if (goARigbod != null && goBRigbod != null)
                        {
                            goA.Transform.Position += mtv * 0.5f;
                            goB.Transform.Position -= mtv * 0.5f;
                        }
                        else if (goARigbod != null)
                            goA.Transform.Position += mtv;
                        else if (goBRigbod != null)
                            goB.Transform.Position -= mtv;
                    }
                    else // response == CollisionResponse.Overlap
                    {
                        goA.BroadcastMessage(c => c.OnOverlapStay(goB, mtv));
                        goB.BroadcastMessage(c => c.OnOverlapStay(goA, -mtv));
                    }
                }
            }
        }
    }
}