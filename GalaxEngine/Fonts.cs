namespace GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;
public static class Fonts
{
    private static readonly Dictionary<(string, int), Font> loaded = new Dictionary<(string, int), Font>();

    public static Font Get(string path, int size)
    {
        path = Path.Combine(AppContext.BaseDirectory, path);
        if (!File.Exists(path))
            throw new FileNotFoundException($"Font file not found: {path}");
        
        if (loaded.TryGetValue((path, size), out var font))
            return font;
        
        font = LoadFontEx(path, size, null, 0);
        SetTextureFilter(font.Texture, TextureFilter.Bilinear);
        loaded[(path, size)] = font;
        return font;
    }
    internal static void UnloadAll()
    {
        foreach (var font in loaded.Values)
        {
            UnloadFont(font);
        }
        loaded.Clear();
    }
}