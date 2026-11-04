namespace GalaxEngine;

public class Collider : EngineComponent
{
    private List<Vector2>? points;

    public List<Vector2> Points
    {
        get => points ?? GetComponent<Polygon>()?.Points ?? [];
        set => points = value;
    }
}