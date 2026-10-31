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
    
    public string? FontPath { get; set; } = null;
    private Font font;

    public override void Start()
    {
        if (FontPath != null)
            font = Fonts.Get(FontPath, FontSize);
        else
            font = GetFontDefault();
    }

    public override void Draw()
    {
        int x = (int)Transform.WorldPosition.X;
        int y = (int)Transform.WorldPosition.Y - FontSize;
        switch (Align)
        {
            case TextAlign.Center:
                x -= (int)(MeasureTextEx(font, Text, FontSize, 1).X / 2);
                break;
            case TextAlign.Right:
                x -= (int)MeasureTextEx(font, Text, FontSize, 1).X;
                break;
        }
        DrawTextEx(font, Text, new Vector2(x, y), FontSize, 1, Color);
    }
}