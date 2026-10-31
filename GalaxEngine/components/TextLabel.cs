namespace GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;
public enum TextAlign { Left, Center, Right }
public class TextLabel : EngineComponent
{
    public string Text { get; set; } = "[BLANK]";
    public Color Color { get; set; } = Color.Black;
    public int FontSize { get; set; } = 10;
    public TextAlign Align { get; set; } = TextAlign.Left;
    

    public override void Draw()
    {
        int x = (int)Transform.WorldPosition.X;
        int y = (int)Transform.WorldPosition.Y - FontSize;
        switch (Align)
        {
            case TextAlign.Center:
                x -= MeasureText(Text, FontSize) / 2;
                break;
            case TextAlign.Right:
                x -= MeasureText(Text, FontSize);
                break;
        }
        DrawText(Text, x, y, FontSize, Color);
    }
}