namespace GalaxEngine;
public static class Mathf
{
    public static float Clamp(float value, float min, float max)
    {
        return MathF.Max(min, MathF.Min(max, value));
    }
    public static float DegToRad(float degrees)
    {
        return degrees * ((float)System.Math.PI / 180.0f);
    }
    public static float RadToDeg(float radians)
    {
        return radians * (180.0f / (float)System.Math.PI);
    }
}