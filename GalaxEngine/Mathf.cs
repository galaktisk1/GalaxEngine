namespace GalaxEngine;
public static class Mathf
{
    public const float Rad2Deg = 180.0f / (float)Math.PI;
    public const float Deg2Rad = (float)Math.PI / 180.0f;
    public static float Clamp(float value, float min, float max)
    {
        return MathF.Max(min, MathF.Min(max, value));
    }
    public static float DegToRad(float degrees) => degrees * Deg2Rad;
    public static float RadToDeg(float radians) => radians * Rad2Deg;
    public static int Sign(float value)
    {
        return value < 0 ? -1 : 1;
    }
}