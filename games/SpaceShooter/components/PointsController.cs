using GalaxEngine;
namespace SpaceShooter;
public class PointsController : Component
{
    private TextLabel label = null!;

    public override void Start()
    {
        label = GetComponent<TextLabel>()!;
    }

    public override void Update()
    {
        label.Text = $"{Globals.Points} points";
    }

    public void UpdatePoints(int delta)
    {
        Globals.Points += delta;
    }
}
