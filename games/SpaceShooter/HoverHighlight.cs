namespace SpaceShooter;

using GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;
public class HoverHighlight : Component
{
    public Color HoverColor = Color.Yellow;

    private Polygon polygon = null!;
    private Color normalColor;

    public override void Start()
    {
        polygon = GetComponent<Polygon>()!;
        normalColor = polygon.FillColor;
    }

    public override void Update()
    {
        bool hovered = Collisions.PointInPolygon(Input.MouseWorldPosition, GameObject);
        polygon.FillColor = hovered ? HoverColor : normalColor;
    }
}