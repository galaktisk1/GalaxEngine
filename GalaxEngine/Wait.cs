namespace GalaxEngine;
public abstract class Wait
{
    public abstract bool KeepWaiting { get; }
}
public class WaitForSeconds : Wait
{
    private readonly float endTime;
    public WaitForSeconds(float seconds)
    {
        endTime = Time.TimeSinceStart + seconds;
    }
    public override bool KeepWaiting => Time.TimeSinceStart < endTime;
}
public class WaitForSecondsRealtime : Wait
{
    private readonly float endTime;
    public WaitForSecondsRealtime(float seconds)
    {
        endTime = Time.UnscaledTime + seconds;
    }
    public override bool KeepWaiting => Time.UnscaledTime < endTime;
}

public class WaitUntil : Wait
{
    private readonly Func<bool> condition;
    public WaitUntil(Func<bool> condition)
    {
        this.condition = condition;
    }
    public override bool KeepWaiting => !condition();
}