using GalaxEngine;
namespace Platformer;
public static class Assets
{
    public static List<Vector2> Triangle => [new(0, -1), new(1, 1), new(-1, 1)];
    public static List<Vector2> Square => [new(1, 1), new(-1, 1), new(-1, -1), new(1, -1)];
}
