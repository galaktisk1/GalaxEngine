namespace GalaxEngine;

public class PhysicsScene
{
    public Scene Scene { get; }
    private GameObject? lastMouseOverObject;
    private float accumulator;
    private const int MaxStepsPerFrame = 5;
    public float PixelsPerMeter { get; set; } = 50;
    public float GravityStrength { get; set; } = 9.81f;
    public float FloorGravity => GravityStrength * PixelsPerMeter;

    public PhysicsScene(Scene scene)
    {
        Scene = scene;
    }

    internal void Simulate()
    {
        UpdateMouseOver();
        accumulator += Time.DeltaTime;
        int steps = 0;
        while (accumulator >= Time.FixedDeltaTime && steps < MaxStepsPerFrame)
        {
            foreach (var sceneObject in Scene.SceneObjects)
                sceneObject.FixedUpdate();
            foreach (var rigidbody in Scene.FindComponents<Rigidbody>())
                rigidbody.Integrate(Time.FixedDeltaTime, FloorGravity);
            UpdateCollisions();
            accumulator -= Time.FixedDeltaTime;
            steps++;
        }
        if (steps == MaxStepsPerFrame)
            accumulator = 0;
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
                    // BLOCK BRANCH
                    if (response == CollisionResponse.Block)
                    {
                        goA.BroadcastMessage(c => c.OnCollisionStay(goB, mtv));
                        goB.BroadcastMessage(c => c.OnCollisionStay(goA, -mtv));

                        float invA = goARigbod?.InverseMass ?? 0f;
                        float invB = goBRigbod?.InverseMass ?? 0f;
                        float invSum = invA + invB;
                        // push out of the overlap, lighter body moves more
                        goA.Transform.Position += mtv * (invA / invSum);
                        goB.Transform.Position -= mtv * (invB / invSum);

                        // exchange momentum along the collision normal
                        var n = mtv.Normalized;
                        var vA = goARigbod?.Velocity ?? Vector2.Zero;
                        var vB = goBRigbod?.Velocity ?? Vector2.Zero;
                        float approach = Vector2.Dot(vA - vB, n);
                        if (approach < 0)
                        {
                            float e = MathF.Max(goARigbod?.Bounciness ?? 0f, goBRigbod?.Bounciness ?? 0f);
                            float impulse = -(1 + e) * approach / invSum;
                            if (goARigbod != null) goARigbod.Velocity += n * (impulse * invA);
                            if (goBRigbod != null) goBRigbod.Velocity -= n * (impulse * invB);
                        }
                    }
                    else // OVERLAP BRANCH
                    {
                        goA.BroadcastMessage(c => c.OnOverlapStay(goB, mtv));
                        goB.BroadcastMessage(c => c.OnOverlapStay(goA, -mtv));
                    }
                }
            }
        }
    }
}