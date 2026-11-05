namespace GalaxEngine;

public class Collider : EngineComponent
{
    public CollisionResponse Response { get; set; } = CollisionResponse.Block;
    private List<Vector2>? points;
    private float? Radius { get; set; }
    public bool IsCircle => Radius is not null || (points is null && GetComponent<Polygon>() is null && GetComponent<Circle>() is not null);
    public float? WorldRadius => (Radius ?? GetComponent<Circle>()?.Radius ?? 0) * Transform.Scale.X;

    public List<Vector2> Points
    {
        get => points ?? GetComponent<Polygon>()?.Points ?? [];
        set => points = value;
    }
}