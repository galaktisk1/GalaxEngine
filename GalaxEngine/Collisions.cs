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

    public static bool PointInPolygon(Vector2 point, GameObject obj)
    {
        var corners = WorldPoints(obj);
        if (corners.Length < 3)
            return false;
        for (int i = 0; i < corners.Length; i++)
        {
            var edge = corners[(i + 1) % corners.Length] - corners[i];
            var axis = Vector2.Perpendicular(edge);
            float p = Vector2.Dot(point, axis);
            float min = float.MaxValue, max = float.MinValue;
            foreach (var corner in corners)
            {
                float projection = Vector2.Dot(corner, axis);
                if (projection < min) min = projection;
                if (projection > max) max = projection;
            }
            if (p < min || p > max)
                return false;
        }
        return true;
    }
    public static bool PointInCircle(Vector2 point, Vector2 center, float radius)
        => (point - center).SqrMagnitude <= radius * radius;
}