using GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace SpaceShooter;
public class DrawComponent : Component
{
    public override void Draw()
    {
        var updater = Owner.GetComponent<UpdateComponent>();
        float cx = updater.Position.X, cy = updater.Position.Y;
        DrawTriangle(new Vector2(cx, cy - 150), new Vector2(cx - 100, cy + 150), new Vector2(cx + 100, cy + 150), new Color(64, 224, 208, 255));
    }
}