class Vector2
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }
    public static implicit operator System.Numerics.Vector2(Vector2 v) => new System.Numerics.Vector2(v.X, v.Y);
    
}