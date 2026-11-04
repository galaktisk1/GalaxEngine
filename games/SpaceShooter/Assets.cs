using GalaxEngine;
namespace SpaceShooter;
public static class Assets
{
    // a new list on every read, so polygons never share (and accidentally edit) the same points
    public static List<Vector2> Triangle => [new(0, -1), new(-1, 1), new(1, 1)];
}
