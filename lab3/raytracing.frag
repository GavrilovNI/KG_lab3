#version 430

out vec4 FragColor;
in vec3 glPosition;

struct SCamera
{
	vec3 Position;
	vec3 Direction;
	vec3 Up;
	vec3 Side;
	vec2 Scale;
};
struct SMaterial
{
	vec3 Color;
	vec4 LightCoeffs;
	float ReflectionPercent;
	float RefractionPercent;
	float RefractionCoef;
};
struct SSphere
{
	vec3 Position;
	vec3 Scale;
	vec3 Right,Up,Forward;

	float Radius;
	int MaterialID;
};
struct STriangle
{
	vec3 v1;
	vec3 v2;
	vec3 v3;
	int MaterialID;
};

const int Light_Directional = 0;
const int Light_Point = 1;

struct SLight
{
	vec3 Position;
	vec3 Color;
	float Intensity;

	int Type;
	vec3 Direction;
	float Range;

};

uniform int maxDepth;
uniform SCamera uCamera;
uniform int materialsCount;
uniform SMaterial materials[30];
uniform int spheresCount;
uniform SSphere spheres[30];
uniform int trianglesCount;
uniform STriangle triangles[30];
uniform int lightsCount;
uniform SLight lights[30];

const float EPSILON = 0.001;
const float BIG = 1000000.0;
const int DIFFUSE = 0;
const int REFLECTION = 1;
const int REFRACTION = 2;


struct SRay
{
	vec3 Origin;
	vec3 Direction;
};

struct STracingRay
{
	SRay ray;
	float coef;
	int depth;
};

/* Stack */

struct Stack
{
    int count;
    STracingRay ar[100];
};

Stack st[2];

void clear(int num) { st[num].count=0; }
bool isEmpty(int num) { return (st[num].count == 0); }
bool isFull(int num) { return (st[num].count == 100); }
void push(int num, STracingRay ray)
{
	if(st[num].count<100)
		st[num].ar[st[num].count++] = ray;
}
STracingRay pop(int num)
{
	if(st[num].count==0) return st[num].ar[0];
	else return st[num].ar[--st[num].count];
}


struct SIntersection
{
	float Distance;
	vec3 Point;
	vec3 Normal;

	int ObjectID;

	SMaterial Material;
};

SRay GenerateRay()
{

	vec2 coords = glPosition.xy * uCamera.Scale;

	//Perspective
	vec3 origin = uCamera.Position;
	vec3 direction = uCamera.Direction + uCamera.Side * coords.x + uCamera.Up * coords.y;

	//Orthographic
	//vec3 origin = uCamera.Position + uCamera.Side * coords.x + uCamera.Up * coords.y;
	//vec3 direction = uCamera.Direction;

	return SRay	(origin, normalize(direction));
}

bool IntersectSphere (SRay ray, SSphere sphere, float maxDistance, out SIntersection intersect )
{
	vec3 backDirToSphere = ray.Origin-sphere.Position;
	float B = dot ( ray.Direction, backDirToSphere );
	float C = dot ( backDirToSphere, backDirToSphere ) - sphere.Radius * sphere.Radius;
	float D = B * B - C;

	

	if(D<=0.0) return false;

	D = sqrt ( D );
	float t1 = ( -B - D );
	float t2 = ( -B + D );

	if(t1 < 0 && t2 < 0) return false;

	float dist;
	if(min(t1, t2) < 0) dist = max(t1,t2);
	else dist = min(t1, t2);

	if(dist>maxDistance) return false;

	intersect.Distance = dist;
	intersect.Point = ray.Origin + ray.Direction * dist;
	intersect.Normal = normalize ( intersect.Point - sphere.Position );
	if(distance(sphere.Position, ray.Origin)<sphere.Radius)
		intersect.Normal = -intersect.Normal;
	intersect.Material = materials[sphere.MaterialID];
	
	return true;
}

bool IntersectTriangle (SRay ray, STriangle triangle, float maxDistance, out SIntersection intersect )
{
	vec3 A = triangle.v2 - triangle.v1;
	vec3 B = triangle.v3 - triangle.v1;
	vec3 N = cross(A, B);

	float NdotRayDirection = dot(N, ray.Direction);
	if (abs(NdotRayDirection) < 0.001)
		return false;

	float d = dot(N, triangle.v1);
	float t = -(dot(N, ray.Origin) - d) / NdotRayDirection;

	if (t < 0)
		return false;

	vec3 P = ray.Origin + t * ray.Direction;

	vec3 C;

	vec3 edge1 = triangle.v2 - triangle.v1;
	vec3 VP1 = P - triangle.v1;
	C = cross(edge1, VP1);
	if (dot(N, C) < 0)
		return false;

	vec3 edge2 = triangle.v3 - triangle.v2;
	vec3 VP2 = P - triangle.v2;
	C = cross(edge2, VP2);
	if (dot(N, C) < 0)
		return false;

	vec3 edge3 = triangle.v1 - triangle.v3;
	vec3 VP3 = P - triangle.v3;
	C = cross(edge3, VP3);
	if (dot(N, C) < 0)
		return false;

	float dist = t;

	if(dist>maxDistance)
	{
		return false;
	}

	intersect.Distance = dist;
	intersect.Point = ray.Origin + ray.Direction * dist;
	intersect.Normal = normalize(cross(triangle.v1 - triangle.v2, triangle.v3 - triangle.v2));
	if(dot(intersect.Normal, ray.Direction)>0)
		intersect.Normal=-intersect.Normal;

	intersect.Material = materials[triangle.MaterialID];

	return true;
}
bool Intersect(SRay ray, int objectID, float maxDistance, out SIntersection intersect )
{
	if(objectID<spheresCount)
	{
		if(IntersectSphere (ray, spheres[objectID], maxDistance, intersect))
		{
			intersect.ObjectID=objectID;
			return true;
		}
	}
	else
	{
		if(IntersectTriangle (ray, triangles[objectID-spheresCount], maxDistance, intersect))
		{
			intersect.ObjectID=objectID;
			return true;
		}
	}
	return false;
}

bool Raytrace ( SRay ray, float dist, inout SIntersection intersect )
{
	bool result = false;
	intersect.Distance = dist;

	for(int i = 0; i < spheresCount+trianglesCount; i++)
	{
		if( Intersect(ray, i, intersect.Distance, intersect ))
		{
			result = true;
		}
	}

	return result;
}

float Shadow(SIntersection intersect, int lightNum)
{
	float shadowing = 1.0;

	vec3 direction;
	float distanceLight;

	vec3 rayOrigin = intersect.Point + intersect.Normal * EPSILON;

	if(lights[lightNum].Type == Light_Directional)
	{
		direction = -lights[lightNum].Direction;
		distanceLight = BIG;
	}
	else if(lights[lightNum].Type == Light_Point)
	{
		direction = normalize(lights[lightNum].Position - intersect.Point);
		distanceLight = distance(lights[lightNum].Position, rayOrigin);
	}

	SRay shadowRay = SRay(rayOrigin, direction);

	SIntersection shadowIntersect;

	if(Raytrace(shadowRay, distanceLight, shadowIntersect))
	{
	//shadowing = 0.0;
		if(shadowIntersect.ObjectID!=intersect.ObjectID)
		{
			shadowing = 0.0;
		}
		else
		{
			//shadowing = 0.0;
		}
	}
	return shadowing;
}

vec3 Phong (SIntersection intersect)
{
	SMaterial material = intersect.Material;

	vec3 result = material.LightCoeffs.x * material.Color;

	for(int i=0; i<lightsCount; ++i)
	{
		vec3 lightDir;
		if(lights[i].Type == Light_Directional)
		{
			lightDir = normalize (-lights[i].Direction);
		}
		else if(lights[i].Type == Light_Point)
		{
			lightDir = normalize ( lights[i].Position - intersect.Point );
		}

		
		float diffuse = dot(lightDir, intersect.Normal);
		vec3 view = normalize(intersect.Point - uCamera.Position);
		vec3 reflected= reflect( view, intersect.Normal );
		float specular = pow(max(dot(reflected, lightDir), 0.0), material.LightCoeffs.w);



		float shadow = Shadow(intersect, i);

		vec3 res = (material.LightCoeffs.y * lights[i].Color * diffuse *shadow);

		if(lights[i].Type == Light_Point)
		{
			float dist = distance(lights[i].Position, intersect.Point);
			if(dist>lights[i].Range)
			{
				res=vec3(0,0,0);			
			}
			else if(lights[i].Range>0)
			{
				res*=pow(1-dist/lights[i].Range,2);
			}
		}

		result += res*lights[i].Intensity+
				  material.LightCoeffs.z * specular * lights[i].Color*lights[i].Intensity;
	}

	return result;
}

vec3 GetColor(SRay firstRay)
{
	vec3 result = vec3(0,0,0);

	clear(0);
	push(0, STracingRay(firstRay, 1.0, maxDepth));

	//int depth = maxDepth;
	while(!isEmpty(0))
	{
		//depth--;
		STracingRay trRay = pop(0);

		if(trRay.depth<=0)
			continue;

		SIntersection intersect;
		if (Raytrace(trRay.ray, BIG, intersect))
		{
			SMaterial material = intersect.Material;
			
			float refraction = max(0.0,min(material.RefractionPercent,1.0));

			if(refraction>0.0)
			{
				vec3 refractDirection = refract(trRay.ray.Direction, intersect.Normal, material.RefractionCoef);
				SRay nRay = SRay(intersect.Point + refractDirection * EPSILON, refractDirection);

				if(dot(refractDirection, intersect.Normal)<0)
				{
					push(0, STracingRay(nRay, trRay.coef*refraction, trRay.depth-1));
				}
				else
				{
					//push(0, STracingRay(nRay, trRay.coef*refraction));
					refraction=0;
				}

				
			}
			float reflection = max(0.0,min(material.ReflectionPercent,1.0-refraction));
			if(reflection>0.0)
			{
				vec3 reflectDirection = reflect(trRay.ray.Direction, intersect.Normal);
				SRay nRay = SRay(intersect.Point + reflectDirection * EPSILON, reflectDirection);

				push(0, STracingRay(nRay, trRay.coef*reflection, trRay.depth-1));
			}

			result+=(1.0-reflection-refraction)*trRay.coef*Phong(intersect);
		}
	}

	return result;

}

void main (void)
{
	SRay ray = GenerateRay();
	FragColor = vec4 (GetColor(ray), 1.0);
}