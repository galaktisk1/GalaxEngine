namespace RayTracer;
using GalaxEngine;
class Program
{
    static void Main(string[] args)
    {
        var a = new Vector3(1, 2, 3);
        var c = Vector3.Cross(a, new Vector3(4, 5, 6));
        Console.WriteLine(c);
        Console.WriteLine(Vector3.Dot(a, c));
    }
}
