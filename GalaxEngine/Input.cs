using Raylib_cs;
using static Raylib_cs.Raylib;
namespace GalaxEngine;
public static class Input
{
    public static List<KeyboardKey> KeysDown = new List<KeyboardKey>();
    public static List<KeyboardKey> KeysDownThisFrame = new List<KeyboardKey>();
    public static List<KeyboardKey> KeysUpThisFrame = new List<KeyboardKey>();
    public static void Update()
    {
        KeysDownThisFrame.Clear();
        KeysUpThisFrame.Clear();
        KeysDown.Clear();
        foreach (KeyboardKey key in Enum.GetValues(typeof(KeyboardKey)))
        {
            if (IsKeyDown(key))
            {
                KeysDown.Add(key);
                if (IsKeyPressed(key))
                    KeysDownThisFrame.Add(key);
            }
            if (IsKeyReleased(key))
                KeysUpThisFrame.Add(key);
            
        }
    }
}