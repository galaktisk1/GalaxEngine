using Raylib_cs;
using static Raylib_cs.Raylib;
namespace GalaxEngine;
public static class Input
{
    private static readonly List<KeyboardKey> KeysDown = new List<KeyboardKey>();
    private static readonly List<KeyboardKey> KeysDownThisFrame = new List<KeyboardKey>();
    private static readonly List<KeyboardKey> KeysUpThisFrame = new List<KeyboardKey>();
    internal static void Update()
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
    public static bool GetKey(KeyboardKey key) => KeysDown.Contains(key);
    public static bool GetKeyDown(KeyboardKey key) => KeysDownThisFrame.Contains(key);
    public static bool GetKeyUp(KeyboardKey key) => KeysUpThisFrame.Contains(key);
}