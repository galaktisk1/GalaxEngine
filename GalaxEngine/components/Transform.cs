namespace GalaxEngine;
using Matrix3x2 = System.Numerics.Matrix3x2;
public class Transform : EngineComponent
{
    public Vector2 Position = Vector2.Zero;
    public float Rotation = 0;
    public Vector2 Scale = Vector2.One;

    public Transform? Parent
    {
        get
        {
            var current = Owner?.Parent;
            while (current != null)
            {
                if (current is GameObject go)
                    return go.Transform;
                current = current.Parent;
            }
            return null;
        }
    }
    public Matrix3x2 LocalMatrix => 
        Matrix3x2.CreateScale(Scale) *
        Matrix3x2.CreateRotation(Rotation) *
        Matrix3x2.CreateTranslation(Position);
    public Matrix3x2 WorldMatrix => Parent is Transform parent 
        ? LocalMatrix * parent.WorldMatrix
        : LocalMatrix;

    public Vector2 WorldPosition => WorldMatrix.Translation;
    
    private Matrix3x2 m => WorldMatrix;
    public float WorldRotation => (float)Math.Atan2(m.M21, m.M11);
}