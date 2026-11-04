namespace GalaxEngine;

public struct Vector2
{
    // Components
    public float X { get; set; }
    public float Y { get; set; }

    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return X;
                case 1: return Y;
                default: throw new IndexOutOfRangeException("Invalid index for Vector2");
            }
        }
        set
        {
            switch (index)
            {
                case 0: X = value; break;
                case 1: Y = value; break;
                default: throw new IndexOutOfRangeException("Invalid index for Vector2");
            }
        }
    }
    // Constructor
    public Vector2(float x, float y)
    {
        this.X = x;
        this.Y = y;
    }
    // Properties for common vector operations
    public float Magnitude => MathF.Sqrt(X * X + Y * Y);
    public float SqrMagnitude => X * X + Y * Y;
    public Vector2 Normalized
    {
        get
        {
            float m = Magnitude;
            return m == 0 ? Zero : this / m;
        }
    }
    // Shorthand constants for common vectors
    public static Vector2 Zero => new Vector2(0, 0);
    public static Vector2 One => new Vector2(1, 1);
    public static Vector2 Up => new Vector2(0, -1);
    public static Vector2 Down => new Vector2(0, 1);
    public static Vector2 Left => new Vector2(-1, 0);
    public static Vector2 Right => new Vector2(1, 0);

    // Static methods for vector operations
    public static float Dot(Vector2 a, Vector2 b) => a.X * b.X + a.Y * b.Y;
    public static float Cross(Vector2 a, Vector2 b) => a.X * b.Y - a.Y * b.X;
    public static Vector2 Perpendicular(Vector2 v) => new Vector2(-v.Y, v.X);
    public static float Distance(Vector2 a, Vector2 b) => (a - b).Magnitude;
    public static Vector2 Max(Vector2 a, Vector2 b) => new Vector2(MathF.Max(a.X, b.X), MathF.Max(a.Y, b.Y));
    public static Vector2 Min(Vector2 a, Vector2 b) => new Vector2(MathF.Min(a.X, b.X), MathF.Min(a.Y, b.Y));
    public static Vector2 Lerp(Vector2 a, Vector2 b, float t) { t = Mathf.Clamp(t, 0, 1); return a + (b - a) * t; }
    public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t) => a + (b - a) * t;
    public static Vector2 Reflect(Vector2 v, Vector2 normal) => v - 2 * Dot(v, normal) * normal;
    public static Vector2 Scale(Vector2 a, Vector2 b) => new Vector2(a.X * b.X, a.Y * b.Y);
    public static Vector2 ClampMagnitude(Vector2 v, float maxLength)
    {
        float sqrMag = v.SqrMagnitude;
        if (sqrMag > maxLength * maxLength)
        {
            float mag = MathF.Sqrt(sqrMag);
            return v / mag * maxLength;
        }
        return v;
    }
    public static float Angle(Vector2 a, Vector2 b)
    {
        float denominator = a.SqrMagnitude * b.SqrMagnitude;
        if (denominator == 0) return 0;
        float magProduct = MathF.Sqrt(denominator);
        float dot = Mathf.Clamp(Dot(a, b) / magProduct, -1, 1);
        return Mathf.RadToDeg(MathF.Acos(dot));
    }
    public static float SignedAngle(Vector2 a, Vector2 b)
    {
        float unsignedAngle = Angle(a, b);
        float sign = Mathf.Sign(Cross(a, b));
        return unsignedAngle * sign;
    }
    public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
    {
        Vector2 toVector = target - current;
        float dist = toVector.Magnitude;
        if (dist <= maxDistanceDelta || dist == 0f)
            return target;
        return current + toVector * (maxDistanceDelta / dist);
    }
    public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
    {
        smoothTime = MathF.Max(0.0001f, smoothTime);
        float omega = 2.0f / smoothTime;
        float x = omega * deltaTime;
        float exp = 1.0f / (1.0f + x + 0.48f * x * x + 0.235f * x * x * x);

        Vector2 change = current - target;
        Vector2 originalTo = target;

        float maxChange = maxSpeed * smoothTime;
        change = ClampMagnitude(change, maxChange);
        target = current - change;

        Vector2 temp = (currentVelocity + omega * change) * deltaTime;
        currentVelocity = (currentVelocity - omega * temp) * exp;
        Vector2 output = target + (change + temp) * exp;

        if (Dot(originalTo - current, output - originalTo) > 0)
        {
            output = originalTo;
            currentVelocity = (output - originalTo) / deltaTime;
        }

        return output;
    }

    // Instance methods for vector arithmetic and properties
    public void Normalize() => this = Normalized;
    public override string ToString() => $"({X}, {Y})";



    // Operator overloads for vector arithmetic
    public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
    public static Vector2 operator -(Vector2 v) => new Vector2(-v.X, -v.Y);
    public static Vector2 operator *(Vector2 v, float scalar) => new Vector2(v.X * scalar, v.Y * scalar);
    public static Vector2 operator *(float scalar, Vector2 v) => new Vector2(v.X * scalar, v.Y * scalar);
    public static Vector2 operator /(Vector2 v, float scalar) => new Vector2(v.X / scalar, v.Y / scalar);

    // this is here so raylib can implicitly convert Vector2 to System.Numerics.Vector2
    public static implicit operator System.Numerics.Vector2(Vector2 v) => new System.Numerics.Vector2(v.X, v.Y);
    // exactly the same thing, but in the opposite direction
    public static implicit operator Vector2(System.Numerics.Vector2 v) => new Vector2(v.X, v.Y);

}