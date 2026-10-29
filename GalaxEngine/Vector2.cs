namespace GalaxEngine;
public class Vector2
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    // this is here so raylib can implicitly convert Vector2 to System.Numerics.Vector2
    public static implicit operator System.Numerics.Vector2(Vector2 v) => new System.Numerics.Vector2(v.X, v.Y);

}