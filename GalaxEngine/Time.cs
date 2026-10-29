namespace GalaxEngine;
using static Raylib_cs.Raylib;
public static class Time
{
    public static float DeltaTime { get; private set; }
    public static float UnscaledDeltaTime { get; private set; }
    public static float UnscaledTime { get; private set; }
    public static float TimeSinceStart { get; private set; }
    public static float TimeScale { get; set; } = 1f;
    internal static void Update()
    {
        UnscaledDeltaTime = MathF.Min(GetFrameTime(), 0.1f);
        DeltaTime = UnscaledDeltaTime * TimeScale;
        TimeSinceStart += DeltaTime;
        UnscaledTime += UnscaledDeltaTime;
    }
}