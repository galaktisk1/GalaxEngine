using GalaxEngine;
namespace SpaceShooter;
public class SceneTimerController : Component
{
    private float startTime;
    private TextLabel label = null!;

    public override void Start()
    {
        startTime = Time.TimeSinceStart;
        label = GetComponent<TextLabel>()!;
    }

    public override void Update()
    {
        float sceneTime = MathF.Floor((Time.TimeSinceStart - startTime) * 100) / 100;
        int minutes = (int)MathF.Floor(sceneTime / 60);
        float seconds = sceneTime - minutes * 60;
        label.Text = $"{minutes}:{seconds:F2}";
    }
}
