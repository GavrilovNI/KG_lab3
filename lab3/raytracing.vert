#version 430

in vec3 vPosition;
out vec3 glPosition;

attribute float scale;

void main (void)
{
	gl_Position = vec4(vPosition, 1.0/scale);
	glPosition = vPosition;
}