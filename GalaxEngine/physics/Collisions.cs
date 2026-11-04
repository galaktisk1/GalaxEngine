namespace GalaxEngine;

public static class Collisions
{
    private static Vector2[] WorldPoints(GameObject obj)
    {
        var collider = obj.GetComponent<Collider>();
        if (collider is null)
            return [];
        var polygon = collider.Points;
        var matrix = obj.Transform.WorldMatrix;
        var points = new Vector2[polygon.Count];
        for (int i = 0; i < points.Length; i++)
            points[i] = System.Numerics.Vector2.Transform(polygon[i], matrix);
        return points;
    }
    private static void Project(Vector2[] points, Vector2 axis, out float min, out float max)
    {
        min = float.MaxValue;
        max = float.MinValue;
        foreach (var point in points)
        {
            float projection = Vector2.Dot(point, axis);
            if (projection < min) min = projection;
            if (projection > max) max = projection;
        }
    }

    // Point in polygon and circle checks
    public static bool PointInPolygon(Vector2 point, GameObject obj)
    {
        var corners = WorldPoints(obj);
        if (corners.Length < 3)
            return false;
        for (int i = 0; i < corners.Length; i++)
        {
            var edge = corners[(i + 1) % corners.Length] - corners[i];
            edge = edge.Normalized;
            var axis = Vector2.Perpendicular(edge);
            float p = Vector2.Dot(point, axis);
            Project(corners, axis, out float min, out float max);
            if (p < min || p > max)
                return false;
        }
        return true;
    }
    public static bool PointInCircle(Vector2 point, Vector2 center, float radius)
        => (point - center).SqrMagnitude <= radius * radius;

    public static bool ContainsPoint(Vector2 point, GameObject obj)
    {
        var collider = obj.GetComponent<Collider>();
        if (collider is null)
            return false;

        if (collider.IsCircle)
            return PointInCircle(point, obj.Transform.Position, collider.WorldRadius ?? 0);

        return PointInPolygon(point, obj);
    }

    // Polygon and Polygon collision checks
    private static bool HasGap(Vector2[] edgesFrom, Vector2[] a, Vector2[] b, ref float bestOverlap, ref Vector2 bestAxis)
    {
        for (int i = 0; i < edgesFrom.Length; i++)
        {
            var edge = edgesFrom[(i + 1) % edgesFrom.Length] - edgesFrom[i];
            edge = edge.Normalized;
            var axis = Vector2.Perpendicular(edge);
            Project(a, axis, out float aMin, out float aMax);
            Project(b, axis, out float bMin, out float bMax);
            if (bMax < aMin || aMax < bMin)
                return true;
            float overlap = MathF.Min(bMax - aMin, aMax - bMin);
            if (overlap < bestOverlap)
            {
                bestOverlap = overlap;
                bestAxis = axis;
            }
        }
        return false;
    }

    public static bool PolygonPolygon(GameObject a, GameObject b, out Vector2 mtv)
    {
        var aCorners = WorldPoints(a);
        var bCorners = WorldPoints(b);

        float bestOverlap = float.MaxValue;
        Vector2 bestAxis = Vector2.Zero;
        var one = HasGap(aCorners, aCorners, bCorners, ref bestOverlap, ref bestAxis);
        var two = HasGap(bCorners, aCorners, bCorners, ref bestOverlap, ref bestAxis);
        if (one || two)
        {
            mtv = Vector2.Zero;
            return false;
        }
        mtv = bestAxis * bestOverlap;
        if (Vector2.Dot(a.Transform.WorldPosition - b.Transform.WorldPosition, mtv) < 0)
            mtv = -mtv;
        return true;
    }
}