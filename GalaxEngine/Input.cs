using Raylib_cs;
using static Raylib_cs.Raylib;
namespace GalaxEngine;
public static class Input
{
    private static readonly KeyboardKey[] AllKeys = Enum.GetValues<KeyboardKey>();
    private static readonly List<KeyboardKey> KeysDown = new List<KeyboardKey>();
    private static readonly List<KeyboardKey> KeysDownThisFrame = new List<KeyboardKey>();
    private static readonly List<KeyboardKey> KeysUpThisFrame = new List<KeyboardKey>();

    private static readonly MouseButton[] AllButtons = Enum.GetValues<MouseButton>();
    private static readonly List<MouseButton> ButtonsDown = new List<MouseButton>();
    private static readonly List<MouseButton> ButtonsDownThisFrame = new List<MouseButton>();
    private static readonly List<MouseButton> ButtonsUpThisFrame = new List<MouseButton>();
    public static Vector2 MousePosition { get; private set; }
    public static Vector2 MouseWorldPosition => Camera.Main.ScreenToWorld(MousePosition);
    public static Vector2 MouseDelta { get; private set; }
    public static float MouseWheel { get; private set; }
    internal static void Update()
    {
        // Update keyboard input states
        KeysDownThisFrame.Clear();
        KeysUpThisFrame.Clear();
        KeysDown.Clear();
        foreach (KeyboardKey key in AllKeys)
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
        
        MouseDelta = GetMouseDelta();
        MousePosition = GetMousePosition();
        MouseWheel = GetMouseWheelMove();
        
        // Update mouse input states
        ButtonsDownThisFrame.Clear();
        ButtonsUpThisFrame.Clear();
        ButtonsDown.Clear();
        foreach (MouseButton button in AllButtons)
        {
            if (IsMouseButtonDown(button))
            {
                ButtonsDown.Add(button);
                if (IsMouseButtonPressed(button))
                    ButtonsDownThisFrame.Add(button);
            }
            if (IsMouseButtonReleased(button))
                ButtonsUpThisFrame.Add(button);
        }
    }
    public static bool GetKey(KeyboardKey key) => KeysDown.Contains(key);
    public static bool GetKeyDown(KeyboardKey key) => KeysDownThisFrame.Contains(key);
    public static bool GetKeyUp(KeyboardKey key) => KeysUpThisFrame.Contains(key);
    public static bool GetMouseButton(MouseButton button) => ButtonsDown.Contains(button);
    public static bool GetMouseButtonDown(MouseButton button) => ButtonsDownThisFrame.Contains(button);
    public static bool GetMouseButtonUp(MouseButton button) => ButtonsUpThisFrame.Contains(button);
}