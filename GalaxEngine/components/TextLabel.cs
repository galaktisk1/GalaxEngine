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

    public float? MaxWidth { get; set; } = null;
    public float LineSpacing { get; set; } = 2;
    private List<string> WrapLines(float maxWidth)
    {
        List<string> lines = new List<string>();
        string current = "";
        foreach (var word in Text.Split(' '))
        {
            var candidate = current == "" ? word : current + " " + word;
            if (current != "" && MeasureTextEx(font, candidate, FontSize, LineSpacing).X > maxWidth)
            {
                lines.Add(current);
                current = word;
            }
            else
            {
                current = candidate;
            }
        }
        if (current != "")
            lines.Add(current);
        return lines;
    }

    public override void Start()
    {
        if (FontPath != null)
            font = Fonts.Get(FontPath, FontSize);
        else
            font = GetFontDefault();
    }

    public override void Draw()
    {
        var lines = MaxWidth is float max ? WrapLines(max) : new List<string>() { Text };
        float baseX = Transform.WorldPosition.X;
        float y = Transform.WorldPosition.Y - FontSize;
        foreach (var line in lines)
        {
            float x = baseX;
            switch (Align)
            {
                case TextAlign.Center:
                    x -= MeasureTextEx(font, line, FontSize, 1).X / 2;
                    break;
                case TextAlign.Right:
                    x -= MeasureTextEx(font, line, FontSize, 1).X;
                    break;
            }
            DrawTextEx(font, line, new Vector2(x, y), FontSize, LineSpacing, Color);
            y += FontSize + LineSpacing;
        }
    }
}