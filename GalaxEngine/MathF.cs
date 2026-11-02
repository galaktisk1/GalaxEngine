public static class MathF
{
    public static float Sqrt(float value)
    {
        return (float)System.Math.Sqrt(value);
    }
    
    public static float Abs(float value)
    {
        return (float)System.Math.Abs(value);
    }
    
    public static float Min(float a, float b)
    {
        return (float)System.Math.Min(a, b);
    }
    
    public static float Max(float a, float b)
    {
        return (float)System.Math.Max(a, b);
    }
    
    public static float Clamp(float value, float min, float max)
    {
        return Max(min, Min(max, value));
    }
    public static float DegToRad(float degrees)
    {
        return degrees * ((float)System.Math.PI / 180.0f);
    }
    public static float RadToDeg(float radians)
    {
        return radians * (180.0f / (float)System.Math.PI);
    }

    public static float Atan2(float y, float x)
    {
        return (float)System.Math.Atan2(y, x);
    }
    public static float Sin(float value)
    {
        return (float)System.Math.Sin(value);
    }

    public static float Cos(float value)
    {
        return (float)System.Math.Cos(value);
    }
    
    public static float Tan(float value)
    {
        return (float)System.Math.Tan(value);
    }
}