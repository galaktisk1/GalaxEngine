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
    private int lightLoc;
    private int lightRangeLoc;
    private int lightIntensityLoc;
    
    public override void Start()
    {
        var image = GenImageColor(Width, Height, Color.Black);
        texture = LoadTextureFromImage(image);
        UnloadImage(image);
        baseTriangle = tracer.Triangles;
        shader = LoadShader(null, Path.Combine(AppContext.BaseDirectory, "shaders", "raytrace.frag"));
        // Get shader locations for uniforms
        cameraLoc = GetShaderLocation(shader, "cameraOrigin");
        timeLoc = GetShaderLocation(shader, "time");
        lightLoc = GetShaderLocation(shader, "lightPosition");
        lightRangeLoc = GetShaderLocation(shader, "lightRange");
        lightIntensityLoc = GetShaderLocation(shader, "lightIntensity");
    }

    public override void Draw()
    {
        // Define uniform values for the shader
        var cameraPosition = new Vector3(0, 1, -3);
        var time = Time.TimeSinceStart;
        var lightPosition = new Vector3(1, 0, -1);
        var lightIntensity = 3f;
        var lightRange = 8f;
        // Set shader values for uniforms
        SetShaderValue(shader, cameraLoc, (System.Numerics.Vector3)cameraPosition, ShaderUniformDataType.Vec3);
        SetShaderValue(shader, timeLoc, time, ShaderUniformDataType.Float);
        SetShaderValue(shader, lightLoc, (System.Numerics.Vector3)lightPosition, ShaderUniformDataType.Vec3);
        SetShaderValue(shader, lightRangeLoc, lightRange, ShaderUniformDataType.Float);
        SetShaderValue(shader, lightIntensityLoc, lightIntensity, ShaderUniformDataType.Float);
        
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