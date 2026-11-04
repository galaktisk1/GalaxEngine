namespace SpaceShooter.Sandbox;

using GalaxEngine;
using Raylib_cs;
public class HoverHighlight : Component
{
    public Color HoverColor = Color.Yellow;

    private Polygon polygon = null;
    private Circle circle = null;
    private Color normalColor;
    private bool hovered;

    public override void Start()
    {
        polygon = GetComponent<Polygon>();
        circle = GetComponent<Circle>();
        normalColor = polygon?.FillColor ?? Color.Blank;
        if (circle is not null)
            normalColor = circle.FillColor;
    }
    public override void OnMouseOver()
    {
        hovered = true;
    }

    public override void Update()
    {
        if (polygon is not null)
            polygon.FillColor = hovered ? HoverColor : normalColor;
        if (circle is not null)
            circle.FillColor = hovered ? HoverColor : normalColor;
        hovered = false;
    }
}