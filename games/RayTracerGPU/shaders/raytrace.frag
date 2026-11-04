#version 330

in vec2 fragTexCoord;
out vec4 finalColor;

uniform vec3 cameraOrigin;
uniform float time;
uniform vec3 lightPosition;
struct Triangle {
    vec3 a;
    vec3 b;
    vec3 c;
    vec3 color;
};
const int TRIANGLE_COUNT = 2;
const Triangle triangles[TRIANGLE_COUNT] = Triangle[](
    Triangle(vec3(-0.6, -0.3, 0.5), vec3(0.4, -0.3, 0.5), vec3(-0.1, 0.7, 0.5), vec3(0.2, 0.4, 1.0)),
    Triangle(vec3(-0.5, -0.5, 0.0), vec3(0.5, -0.5, 0.0), vec3(0.0, 0.5, 0.0), vec3(1.0, 0.5, 0.0))
);
const vec3 background = vec3(0.1);

float intersectPlane(vec3 origin, vec3 dir, vec3 normal, float d)
{
    return (-d - dot(origin, normal)) / dot(dir, normal);
}
bool contains(vec3 p, vec3 a, vec3 b, vec3 c)
{
    float area = length(cross(b - a, c - a));
    float a1 = length(cross(b - p, c - p)) / area;
    float a2 = length(cross(a - p, c - p)) / area;
    float a3 = length(cross(a - p, b - p)) / area;
    return a1 + a2 + a3 <= 1.001;
}
vec3 rotateY(vec3 v, float angle)
{
    float c = cos(angle), s = sin(angle);
    return vec3(
        c * v.x + s * v.z,
        v.y,
        -s * v.x + c * v.z
    );
}
bool tryHit(vec3 origin, vec3 dir, out float closest, out vec3 hitNormal, out vec3 hitColor)
{
    closest = 1e30;
    hitNormal = vec3(0.0);
    hitColor = vec3(0.0);
    for (int i = 0; i < TRIANGLE_COUNT; i++)
    {
        vec3 a = rotateY(triangles[i].a, time);
        vec3 b = rotateY(triangles[i].b, time);
        vec3 c = rotateY(triangles[i].c, time);
        vec3 normal = normalize(cross(a - b, c - b));
        float d = -dot(normal, a);
        float t = intersectPlane(origin, dir, normal, d);
        if(isinf(t) || isnan(t) || t < 0.0 || abs(dot(dir, normal)) < 1e-6 || t >= closest)
            continue;
        
        vec3 hit = origin + dir * t;
        if(!contains(hit, a, b, c))
            continue;
        
        closest = t;
        hitNormal = normal;
        hitColor = triangles[i].color;
    }
    return closest < 1e30;
}
void main()
{
    float closest;
    vec3 hitNormal;
    vec3 hitColor;
    vec2 p = fragTexCoord * 2.0 - 1.0;
    p.y = -p.y;
    vec3 rayDir = normalize(vec3(p, 0.0) - cameraOrigin);
    if(!tryHit(cameraOrigin, rayDir, closest, hitNormal, hitColor))
    {
        finalColor = vec4(background, 1.0);
        return;
    }
    vec3 hit = cameraOrigin + rayDir * closest;
    vec3 toLightFull = lightPosition - hit;
    vec3 toLight = normalize(toLightFull);
    float distance = length(toLightFull);

    float diffuse = max(dot(hitNormal, toLight), 0.0);
    float falloff = 1.0 / (1.0 + 0.3 * distance * distance);
    float ambient = 0.15;
    
    float lighting = ambient + diffuse * falloff;
    finalColor = vec4(hitColor * lighting, 1.0);
}
