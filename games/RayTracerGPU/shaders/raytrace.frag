#version 330

in vec2 fragTexCoord;
out vec4 finalColor;

uniform vec3 cameraOrigin;

void main()
{
    float x = fragTexCoord.x * 2.0 - 1.0;
    float y = -(fragTexCoord.y * 2.0 - 1.0);
    vec3 target = vec3(x, y, 0.0);
    vec3 dir = normalize(target - cameraOrigin);
    finalColor = vec4(dir * 0.5 + 0.5, 1.0);
}