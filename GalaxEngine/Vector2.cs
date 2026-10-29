namespace GalaxEngine;
public struct Vector2
{
    public float x { get; set; }
    public float y { get; set; }

    public float this[int index]
    {
        get 
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                default: throw new IndexOutOfRangeException("Invalid index for Vector2");
            }
        }
        set
        {
            switch (index)
            {
                case 0: x = value; break;
                case 1: y = value; break;
                default: throw new IndexOutOfRangeException("Invalid index for Vector2");
            }
        }
    }
    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    // Properties for common vector operations
    public float Magnitude => MathF.Sqrt(x * x + y * y);
    public float SqrMagnitude => x * x + y * y;
    public Vector2 Normalized => Magnitude == 0 ? Zero : this / Magnitude;
    public static Vector2 Zero => new Vector2(0, 0);
    public static Vector2 One => new Vector2(1, 1);
    public static Vector2 Up => new Vector2(0, -1);
    public static Vector2 Down => new Vector2(0, 1);
    public static Vector2 Left => new Vector2(-1, 0);
    public static Vector2 Right => new Vector2(1, 0);



    // Operator overloads for vector arithmetic
    public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
    public static Vector2 operator -(Vector2 v) => new Vector2(-v.x, -v.y);
    public static Vector2 operator *(Vector2 v, float scalar) => new Vector2(v.x * scalar, v.y * scalar);
    public static Vector2 operator /(Vector2 v, float scalar) => new Vector2(v.x / scalar, v.y / scalar);

    // this is here so raylib can implicitly convert Vector2 to System.Numerics.Vector2
    public static implicit operator System.Numerics.Vector2(Vector2 v) => new System.Numerics.Vector2(v.x, v.y);

}