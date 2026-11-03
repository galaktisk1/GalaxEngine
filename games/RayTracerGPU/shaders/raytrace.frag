#version 330

in vec2 fragTexCoord;
out vec4 finalColor;

uniform vec3 cameraOrigin;
uniform float time;
const vec3 A = vec3(-0.5, -0.5, 0.0);
const vec3 B = vec3(0.5, -0.5, 0.0);
const vec3 C = vec3(0.0, 0.5, 0.0);
const vec3 triColor = vec3(1.0, 0.5, 0.0);
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
void main()
{
    vec3 a = rotateY(A, time);
    vec3 b = rotateY(B, time);
    vec3 c = rotateY(C, time);
    float x = fragTexCoord.x * 2.0 - 1.0;
    float y = -(fragTexCoord.y * 2.0 - 1.0);
    vec3 target = vec3(x, y, 0.0);
    vec3 dir = normalize(target - cameraOrigin);
    vec3 normal = normalize(cross(a - b, c - b));
    float d = -dot(normal, a);
    float t = intersectPlane(cameraOrigin, dir, normal, d);
    if(isinf(t) || isnan(t) || t < 0.0 || abs(dot(dir, normal)) < 1e-6)
    {
        finalColor = vec4(background, 1.0);
        return;
    }
    vec3 hit = cameraOrigin + dir * t;
    if(contains(hit, a, b, c))
        finalColor = vec4(triColor, 1.0);
    else
        finalColor = vec4(background, 1.0);
}
