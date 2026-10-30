namespace GalaxEngine;
using Matrix3x2 = System.Numerics.Matrix3x2;
public class Transform : EngineComponent
{
    public Vector2 Position = Vector2.Zero;
    public float Rotation = 0;
    public Vector2 Scale = Vector2.One;

    public Matrix3x2 LocalMatrix => 
        Matrix3x2.CreateScale(Scale) *
        Matrix3x2.CreateRotation(Rotation) *
        Matrix3x2.CreateTranslation(Position);
}