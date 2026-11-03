namespace GalaxEngine;
public struct Vector3
{
    public float X;
    public float Y;
    public float Z;

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString() {return $"({X}, {Y}, {Z})";}
    public Vector3 Normalized 
    {
        get
        {
            float m = Magnitude;
            return m == 0 ? Zero : this / m;
        }
    }
    

    // shortcuts for common vectors
    public static Vector3 Zero => new Vector3(0, 0, 0);
    public static Vector3 One => new Vector3(1, 1, 1);
    public static Vector3 UnitX => new Vector3(1, 0, 0);
    public static Vector3 UnitY => new Vector3(0, 1, 0);
    public static Vector3 UnitZ => new Vector3(0, 0, 1);
    public static Vector3 Up => new Vector3(0, 1, 0);
    public static Vector3 Down => new Vector3(0, -1, 0);
    public static Vector3 Left => new Vector3(-1, 0, 0);
    public static Vector3 Right => new Vector3(1, 0, 0);
    public static Vector3 Forward => new Vector3(0, 0, 1);
    public static Vector3 Backward => new Vector3(0, 0, -1);
    public float Magnitude => MathF.Sqrt(X * X + Y * Y + Z * Z);

    // operators for vector arithmetic
    public static Vector3 operator +(Vector3 a, Vector3 b) {return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);}
    public static Vector3 operator -(Vector3 a, Vector3 b) {return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);}
    public static Vector3 operator *(Vector3 a, float b) {return new Vector3(a.X * b, a.Y * b, a.Z * b);}
    public static Vector3 operator /(Vector3 a, float b) {return new Vector3(a.X / b, a.Y / b, a.Z / b);}
    public static Vector3 operator -(Vector3 v) => new Vector3(-v.X, -v.Y, -v.Z);
    public static float Dot(Vector3 a, Vector3 b){return a.X * b.X + a.Y * b.Y + a.Z * b.Z;}
    public static Vector3 Cross(Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.Y * b.Z - a.Z * b.Y,
            a.Z * b.X - a.X * b.Z,
            a.X * b.Y - a.Y * b.X
        );
    }


    // this is here so raylib can implicitly convert Vector3 to System.Numerics.Vector3
    public static implicit operator System.Numerics.Vector3(Vector3 v) => new System.Numerics.Vector3(v.X, v.Y, v.Z);
    // exactly the same thing, but in the opposite direction
    public static implicit operator Vector3(System.Numerics.Vector3 v) => new Vector3(v.X, v.Y, v.Z);

}