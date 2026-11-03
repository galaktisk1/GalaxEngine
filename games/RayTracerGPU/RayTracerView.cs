namespace RayTracerGPU;
using GalaxEngine;
using Raylib_cs;
using static Raylib_cs.Raylib;

class RayTracerView : Component
{
    private List<Triangle> baseTriangle = [];
    private readonly List<Triangle> floorTriangles =
[
    new Triangle(new Vector3(-2, -0.6f, -1), new Vector3(2, -0.6f, -1), new Vector3(2, -0.6f, 3), new Vector3(0.5f, 0.5f, 0.5f), 0.5f),
    new Triangle(new Vector3(-2, -0.6f, -1), new Vector3(2, -0.6f, 3), new Vector3(-2, -0.6f, 3), new Vector3(0.5f, 0.5f, 0.5f), 0.5f),
];
    const int Width = 400;
    const int Height = 400;
    private Texture2D texture;
    private Tracer tracer = new Tracer(Width, Height);
    private Shader shader;
    private int cameraLoc;
    private int timeLoc;
    
    public override void Start()
    {
        var image = GenImageColor(Width, Height, Color.Black);
        texture = LoadTextureFromImage(image);
        UnloadImage(image);
        baseTriangle = tracer.Triangles;
        shader = LoadShader(null, Path.Combine(AppContext.BaseDirectory, "shaders", "raytrace.frag"));
        cameraLoc = GetShaderLocation(shader, "cameraOrigin");
        timeLoc = GetShaderLocation(shader, "time");
    }

    public override void Draw()
    {
        SetShaderValue(shader, cameraLoc, (System.Numerics.Vector3)new Vector3(0, 0, -3), ShaderUniformDataType.Vec3);
        SetShaderValue(shader, timeLoc, Time.TimeSinceStart, ShaderUniformDataType.Float);
        BeginShaderMode(shader);
        DrawTexture(texture, 0, 0, Color.White);
        EndShaderMode();
    }

    public override void OnDestroy()
    {
        Console.WriteLine("Destroying RayTracerView");
        UnloadTexture(texture);
        UnloadShader(shader);
    }
}