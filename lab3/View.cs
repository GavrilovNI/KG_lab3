using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace lab3
{
    class SObject
    {
        public enum Object3DType
        {
            Plane,
            Cube,
            STetrahedron
        }

        /*public static Vector3 ToEuler(Quaternion q)
        {
            Vector3 pitchYawRoll = new Vector3();
            double sqw = q.W * q.W;
            //double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;
            pitchYawRoll.Y = (float)Math.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1 - 2f * (sqz + sqw));     // Yaw 
            pitchYawRoll.X = (float)Math.Asin(2f * (q.X * q.Z - q.W * q.Y));                             // Pitch 
            pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1 - 2f * (sqy + sqz));      // Roll 
            return pitchYawRoll;
        }
        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = (float)Math.Sin((double)rollOver2);
            float cosRollOver2 = (float)Math.Cos((double)rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = (float)Math.Sin((double)pitchOver2);
            float cosPitchOver2 = (float)Math.Cos((double)pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = (float)Math.Sin((double)yawOver2);
            float cosYawOver2 = (float)Math.Cos((double)yawOver2);
            Quaternion result = new Quaternion();
            result.X = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.Y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.Z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.W = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;
        }*/

        public static void GetLocalVectors(Vector3 rotation, out Vector3 lright, out Vector3 lup, out Vector3 lforward)
        {
            Vector3 right = new Vector3(1, 0, 0);
            Vector3 up = new Vector3(0, 1, 0);
            Vector3 forward = new Vector3(0, 0, 1);

            Vector3 nright = Vector3.Transform(right, Quaternion.FromAxisAngle(right, (float)(rotation.X * Math.PI / 180)));
            Vector3 nup = Vector3.Transform(up, Quaternion.FromAxisAngle(right, (float)(rotation.X * Math.PI / 180)));
            Vector3 nforward = Vector3.Transform(forward, Quaternion.FromAxisAngle(right, (float)(rotation.X * Math.PI / 180)));

            nright = Vector3.Transform(nright, Quaternion.FromAxisAngle(up, (float)(rotation.Y * Math.PI / 180)));
            nup = Vector3.Transform(nup, Quaternion.FromAxisAngle(up, (float)(rotation.Y * Math.PI / 180)));
            nforward = Vector3.Transform(nforward, Quaternion.FromAxisAngle(up, (float)(rotation.Y * Math.PI / 180)));

            nright = Vector3.Transform(nright, Quaternion.FromAxisAngle(nforward, (float)(rotation.Z * Math.PI / 180)));
            nup = Vector3.Transform(nup, Quaternion.FromAxisAngle(nforward, (float)(rotation.Z * Math.PI / 180)));
            nforward = Vector3.Transform(nforward, Quaternion.FromAxisAngle(nforward, (float)(rotation.Z * Math.PI / 180)));

            lright = nright.Normalized();
            lup = nup.Normalized();
            lforward = nforward.Normalized();
        }

        /*
        public static SObject Create3DObject(Object3DType object3DType, Vector3 position, Vector3 rotation, Vector3 scale, int materialID)
        {
            switch (object3DType)
            {
                case Object3DType.Plane:
                    return new SPlane(position, rotation, scale, materialID);
                case Object3DType.Cube:
                    return new SCube(position, rotation, scale, materialID);
                case Object3DType.STetrahedron:
                    return new STetrahedron(position, rotation, scale, materialID);
                default:
                    return null;
            }
        }
        */
    }

    interface ISendableToShader
    {
        void SendToShader(int programID, string name);
    }
    class SCamera : SObject, ISendableToShader
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector2 scale;



        public SCamera() : this(position: new Vector3(0, 0, -8),
                          rotation: Vector3.Zero,
                          scale: new Vector2(1, 1))
        {

        }
        public SCamera(Vector3 position, Vector3 rotation, Vector2 scale)
        {
            this.position = position;

            this.rotation = rotation;
            //GetLocalVectors(rotation, out this.side, out this.up, out this.direction);

            this.scale = scale;
        }

        public void SendToShader(int programID, string name)
        {
            int uniformLoc;

            uniformLoc = GL.GetUniformLocation(programID, name + ".Position");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, position);
            }

            Vector3 lright, lup, lforward;
            GetLocalVectors(rotation, out lright, out lup, out lforward);

            uniformLoc = GL.GetUniformLocation(programID, name + ".Direction");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, lforward);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".Up");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, lup);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".Side");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, lright);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".Scale");
            if (uniformLoc != -1)
            {
                GL.Uniform2(uniformLoc, scale);
            }
        }
    };
    class SMaterial : ISendableToShader
    {
        public Vector3 color { get; private set; }
        public Vector4 lightCoeffs { get; private set; }
        public float reflectionPercent { get; private set; }
        public float refractionPercent { get; private set; }
        public float refractionCoef { get; private set; }

        public void Update(Vector3 color, Vector4 lightCoeffs, float reflectionPercent, float refractionPercent, float refractionCoef)
        {
            this.color = color;
            this.lightCoeffs = lightCoeffs;
            this.reflectionPercent = reflectionPercent;
            this.refractionPercent = refractionPercent;
            this.refractionCoef = refractionCoef;
        }
        public SMaterial() : this(color: new Vector3(0.3f, 0.3f, 0.3f),
                                lightCoeffs: new Vector4(0.4f, 0.9f, 0f, 512f),
                                reflectionPercent: 0,
                                refractionPercent: 0,
                                refractionCoef: 1)
        {

        }

        public SMaterial(Vector3 color, Vector4 lightCoeffs, float reflectionPercent, float refractionPercent, float refractionCoef)
        {
            this.color = color;
            this.lightCoeffs = lightCoeffs;
            this.reflectionPercent = reflectionPercent;
            this.refractionPercent = refractionPercent;
            this.refractionCoef = refractionCoef;
        }
        public void SendToShader(int programID, string name)
        {
            int uniformLoc;

            uniformLoc = GL.GetUniformLocation(programID, name + ".Color");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, color);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".LightCoeffs");
            if (uniformLoc != -1)
            {
                GL.Uniform4(uniformLoc, lightCoeffs);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".ReflectionPercent");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, reflectionPercent);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".RefractionPercent");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, refractionPercent);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".RefractionCoef");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, refractionCoef);
            }
        }
    };
    class SSphere : SObject, ISendableToShader
    {
        public Vector3 position { get; private set; }
        public float radius { get; private set; }
        public int materialID { get; private set; }

        public void Update(Vector3 position, float radius, int materialID)
        {
            this.position = position;
            this.radius = radius;
            this.materialID = materialID;
        }

        public SSphere() : this(position: Vector3.Zero,
                               radius: 1,
                               materialID: 0)
        {

        }
        public SSphere(Vector3 position, float radius, int materialID)
        {
            this.position = position;
            this.radius = radius;
            this.materialID = materialID;
        }

        public void SendToShader(int programID, string name)
        {
            int uniformLoc;

            uniformLoc = GL.GetUniformLocation(programID, name + ".Position");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, position);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".Radius");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, radius);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".MaterialID");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, materialID);
            }
        }
    };
    class STriangle : ISendableToShader
    {
        private Vector3 v1, v2, v3;
        private int materialID;

        public void Update(Vector3 v1, Vector3 v2, Vector3 v3, int materialID)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.materialID = materialID;
        }

        public STriangle() : this(v1: Vector3.Zero,
                                  v2: new Vector3(0, 1, 0),
                                  v3: new Vector3(1, 0, 0),
                                  materialID: 0)
        {

        }
        public STriangle(Vector3 v1, Vector3 v2, Vector3 v3, int materialID)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.materialID = materialID;
        }

        public void SendToShader(int programID, string name)
        {
            int uniformLoc;

            uniformLoc = GL.GetUniformLocation(programID, name + ".v1");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, v1);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".v2");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, v2);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".v3");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, v3);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".MaterialID");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, materialID);
            }
        }
    };
    class SLight : SObject, ISendableToShader
    {
        public enum LightType
        {
            Directional = 0,
            Point = 1
        }

        public Vector3 position;
        public Vector3 color;
        public float intensity;

        public LightType lightType;
        public Vector3 rotation;
        public float range;

        public SLight() : this(position: Vector3.Zero,
                               color: new Vector3(1, 1, 1),
                               intensity: 1,
                               lightType: LightType.Directional,
                               rotation: new Vector3(50, -30, 0),
                               range: 5)
        {

        }
        public SLight(Vector3 position, Vector3 color, float intensity, LightType lightType, Vector3 rotation, float range)
        {
            this.position = position;
            this.color = color;
            this.intensity = intensity;
            this.lightType = lightType;
            this.rotation = rotation;
            this.range = range;
        }

        public void SendToShader(int programID, string name)
        {
            int uniformLoc = GL.GetUniformLocation(programID, name + ".Position");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, position);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".Color");
            if (uniformLoc != -1)
            {
                GL.Uniform3(uniformLoc, color);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".Intensity");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, intensity);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".Type");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, (int)lightType);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".Direction");
            if (uniformLoc != -1)
            {
                Vector3 right, up, forward;
                GetLocalVectors(rotation, out right, out up, out forward);

                GL.Uniform3(uniformLoc, forward);
            }
            uniformLoc = GL.GetUniformLocation(programID, name + ".Range");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, range);
            }
        }
    }

    interface ICuttableToTriangles
    {
        List<STriangle> GetTriangles();
    }

    class SPlane : SObject, ICuttableToTriangles
    {
        private STriangle[] triangles;
        public Vector3 position { get; private set; }
        public Vector3 right { get; private set; }
        public Vector3 up { get; private set; }
        public Vector3 forward { get; private set; }
        public Vector3 scale { get; private set; }
        public int materialID { get; private set; }

        private void Init()
        {
            triangles = new STriangle[2];


            triangles[0] = new STriangle();
            triangles[1] = new STriangle();
            Update(position, right, up, forward, scale, materialID);
        }

        public void Update(Vector3 position, Vector3 right, Vector3 up, Vector3 forward, Vector3 scale, int materialID)
        {
            this.position = position;
            this.right = right;
            this.up = up;
            this.forward = forward;
            this.scale = scale;
            this.materialID = materialID;

            triangles[0].Update((forward.Normalized() * scale.Z + right.Normalized() * scale.X) / 2 + position,
                                (-forward.Normalized() * scale.Z + right.Normalized() * scale.X) / 2 + position,
                                (forward.Normalized() * scale.Z + -right.Normalized() * scale.X) / 2 + position,
                                materialID);

            triangles[1].Update((-forward.Normalized() * scale.Z + -right.Normalized() * scale.X) / 2 + position,
                                (-forward.Normalized() * scale.Z + right.Normalized() * scale.X) / 2 + position,
                                (forward.Normalized() * scale.Z + -right.Normalized() * scale.X) / 2 + position,
                                materialID);
        }

        public SPlane(Vector3 position, Vector3 rotation, Vector3 scale, int materialID)
        {
            Vector3 right, up, forward;
            GetLocalVectors(rotation, out right, out up, out forward);
            this.position = position;
            this.right = right;
            this.up = up;
            this.forward = forward;
            this.scale = scale;
            this.materialID = materialID;
            Init();
        }
        public SPlane(Vector3 position, Vector3 right, Vector3 up, Vector3 forward, Vector3 scale, int materialID)
        {
            this.position = position;
            this.right = right;
            this.up = up;
            this.forward = forward;
            this.scale = scale;
            this.materialID = materialID;
            Init();
        }

        public List<STriangle> GetTriangles()
        {
            return new List<STriangle>(triangles);
        }
    }
    class SCube : SObject, ICuttableToTriangles
    {
        private SPlane[] planes;
        public Vector3 position { get; private set; }
        public Vector3 right { get; private set; }
        public Vector3 up { get; private set; }
        public Vector3 forward { get; private set; }
        public Vector3 scale { get; private set; }
        public int materialID { get; private set; }

        private void Init()
        {
            planes = new SPlane[6];

            planes[0] = new SPlane(position + up * scale.Y / 2, right, up, forward, scale, materialID);
            planes[1] = new SPlane(position - up * scale.Y / 2, right, up, forward, scale, materialID);
            planes[2] = new SPlane(position + right * scale.X / 2, -up, right, forward, scale.Yxz, materialID);
            planes[3] = new SPlane(position - right * scale.X / 2, -up, right, forward, scale.Yxz, materialID);
            planes[4] = new SPlane(position + forward * scale.Z / 2, right, forward, -up, scale.Xzy, materialID);
            planes[5] = new SPlane(position - forward * scale.Z / 2, right, forward, -up, scale.Xzy, materialID);

        }

        public void Update(Vector3 position, Vector3 right, Vector3 up, Vector3 forward, Vector3 scale, int materialID)
        {
            this.position = position;
            this.right = right;
            this.up = up;
            this.forward = forward;
            this.scale = scale;
            this.materialID = materialID;
            planes[0].Update(position + up * scale.Y / 2, right, up, forward, scale, materialID);
            planes[1].Update(position - up * scale.Y / 2, right, up, forward, scale, materialID);
            planes[2].Update(position + right * scale.X / 2, -up, right, forward, scale.Yxz, materialID);
            planes[3].Update(position - right * scale.X / 2, -up, right, forward, scale.Yxz, materialID);
            planes[4].Update(position + forward * scale.Z / 2, right, forward, -up, scale.Xzy, materialID);
            planes[5].Update(position - forward * scale.Z / 2, right, forward, -up, scale.Xzy, materialID);
        }
        public SCube(Vector3 position, Vector3 rotation, Vector3 scale, int materialID)
        {
            Vector3 right, up, forward;
            GetLocalVectors(rotation, out right, out up, out forward);

            this.position = position;
            this.right = right;
            this.up = up;
            this.forward = forward;
            this.scale = scale;
            this.materialID = materialID;

            Init();


        }
        public SCube(Vector3 position, Vector3 right, Vector3 up, Vector3 forward, Vector3 scale, int materialID)
        {
            this.position = position;
            this.right = right;
            this.up = up;
            this.forward = forward;
            this.scale = scale;
            this.materialID = materialID;
            Init();
        }

        public List<STriangle> GetTriangles()
        {
            List<STriangle> result = new List<STriangle>();

            for (int i = 0; i < planes.Length; i++)
            {
                result.AddRange(planes[i].GetTriangles());
            }

            return result;
        }
    }
    class STetrahedron : SObject, ICuttableToTriangles
    {
        private STriangle[] triangles;
        public Vector3 position { get; private set; }
        public Vector3 right { get; private set; }
        public Vector3 up { get; private set; }
        public Vector3 forward { get; private set; }
        public Vector3 scale { get; private set; }
        public int materialID { get; private set; }

        private void Init()
        {
            triangles = new STriangle[4];



            float ho = (float)Math.Sqrt(3) / 2;
            float ht = (float)Math.Sqrt(6) / 3;


            Vector3 v1 = position - up * ht / 4 + forward * ho * 2 / 3;
            Vector3 v2 = position - up * ht / 4 - forward * ho / 3 + right * 0.5f;
            Vector3 v3 = position - up * ht / 4 - forward * ho / 3 - right * 0.5f;
            Vector3 v4 = position + up * ht * 3 / 4;

            triangles[0] = new STriangle(v1, v2, v3, materialID);
            triangles[1] = new STriangle(v4, v2, v3, materialID);
            triangles[2] = new STriangle(v1, v4, v3, materialID);
            triangles[3] = new STriangle(v1, v2, v4, materialID);

        }

        private void Update(Vector3 position, Vector3 right, Vector3 up, Vector3 forward, Vector3 scale, int materialID)
        {
            this.position = position;
            this.right = right;
            this.up = up;
            this.forward = forward;
            this.scale = scale;
            this.materialID = materialID;


            float ho = (float)Math.Sqrt(3) / 2;
            float ht = (float)Math.Sqrt(6) / 3;


            Vector3 v1 = position - up * ht / 4 + forward * ho * 2 / 3;
            Vector3 v2 = position - up * ht / 4 - forward * ho / 3 + right * 0.5f;
            Vector3 v3 = position - up * ht / 4 - forward * ho / 3 - right * 0.5f;
            Vector3 v4 = position + up * ht * 3 / 4;

            triangles[0].Update(v1, v2, v3, materialID);
            triangles[1].Update(v4, v2, v3, materialID);
            triangles[2].Update(v1, v4, v3, materialID);
            triangles[3].Update(v1, v2, v4, materialID);
        }

        public STetrahedron(Vector3 position, Vector3 rotation, Vector3 scale, int materialID)
        {
            Vector3 right, up, forward;
            GetLocalVectors(rotation, out right, out up, out forward);
            this.position = position;
            this.right = right;
            this.up = up;
            this.forward = forward;
            this.scale = scale;
            this.materialID = materialID;
            Init();

        }
        public STetrahedron(Vector3 position, Vector3 right, Vector3 up, Vector3 forward, Vector3 scale, int materialID)
        {
            this.position = position;
            this.right = right;
            this.up = up;
            this.forward = forward;
            this.scale = scale;
            this.materialID = materialID;
            Init();
        }

        public List<STriangle> GetTriangles()
        {
            return new List<STriangle>(triangles);
        }
    }



    class View
    {


        static void SendListToShader(List<ISendableToShader> list, int programID, string name)
        {
            int uniformLoc = GL.GetUniformLocation(programID, name + "Count");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, list.Count);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i].SendToShader(programID, name+"["+i+"]");
            }
        }

        static List<SMaterial> InitMaterials()
        {
            List<SMaterial> materials = new List<SMaterial>();
            materials.Add(new SMaterial(new Vector3(0f, 1f, 0f),
                                       new Vector4(0.5f, 0.7f, 0.5f, 128f),
                                       0,
                                       0,
                                       1));
            materials.Add(new SMaterial(new Vector3(0f, 0f, 1f),
                                       new Vector4(0.4f, 0.9f, 0f, 512f),
                                       0,
                                       0,
                                       1));
            //MIRROR MATERIAL
            materials.Add(new SMaterial(new Vector3(192 / 255f, 192 / 255f, 192 / 255f),
                                       new Vector4(1f, 0.2f, 0f, 512f),
                                       0.8f,
                                       0,
                                       1));
            materials.Add(new SMaterial(new Vector3(1f, 0f, 0f),
                                       new Vector4(0.4f, 0.9f, 0f, 512f),
                                       0.1f,
                                       0.7f,
                                       0.85f));
            materials.Add(new SMaterial(new Vector3(1f, 0f, 0f),
                                       new Vector4(0.4f, 0.9f, 0f, 512f),
                                       0,
                                       0,
                                       1));
            return materials;
        }
        static List<SObject> InitObjects()
        {
            List<SObject> objects = new List<SObject>();

            objects.Add(new SCamera(new Vector3(0, 0, -8f),
                              new Vector3(0, 0, 0),
                              new Vector2(1f, 1f)));

            objects.Add(new SLight(new Vector3(-4, 4, -1),
                                  new Vector3(1, 1, 1),
                                  0.7f,
                                  SLight.LightType.Point,
                                  new Vector3(50,-30,0),
                                  15));
            objects.Add(new SLight(new Vector3(4, 4, -1),
                                  new Vector3(1, 1, 1),
                                  1.5f,
                                  SLight.LightType.Point,
                                  new Vector3(50, -30, 0),
                                  15));

            objects.Add(new SCube(new Vector3(0.7f, 0, 4),
                                new Vector3(45, 45, 0),
                                new Vector3(1, 2, 3),
                                4));

            objects.Add(new SSphere(new Vector3(0,0,-2),
                                    2,
                                    3));

            objects.Add(new STetrahedron(new Vector3(-4f, 0, 0),
                                        new Vector3(0, 70, 30),
                                        new Vector3(1, 1, 1),
                                        4));


            //left wall
            objects.Add(new SPlane(new Vector3(-5, 0, -2),
                                    new Vector3(0, 0, 90),
                                    new Vector3(10, 0, 14),
                                    0));
            // right wall 
            objects.Add(new SPlane(new Vector3(5, 0, -2),
                                    new Vector3(0, 0, 90),
                                    new Vector3(10, 0, 14),
                                    2));
            // back wall 
            objects.Add(new SPlane(new Vector3(0, 0, 5),
                                    new Vector3(90, 0, 0),
                                    new Vector3(10, 0, 10),
                                    0));
            // down wall 
            objects.Add(new SPlane(new Vector3(0, -5, -2),
                                    new Vector3(0, 0, 0),
                                    new Vector3(10, 0, 14),
                                    1));
            // up wall 
            objects.Add(new SPlane(new Vector3(0, 5, -2),
                                    new Vector3(0, 0, 0),
                                    new Vector3(10, 0, 14),
                                    1));
            // front wall 
            objects.Add(new SPlane(new Vector3(0, 0, -9),
                                    new Vector3(90, 0, 0),
                                    new Vector3(10, 0, 10),
                                    1));

            return objects;
        }

        static void SplitObjects(List<SObject> objects, out SCamera cam, out List<SSphere> spheres, out List<STriangle> triangles, out List<SLight> lights)
        {
            spheres = new List<SSphere>();
            triangles = new List<STriangle>();
            lights = new List<SLight>();
            cam = null;

            for (int i = 0; i < objects.Count; i++)
            {
                SCamera cCam = objects[i] as SCamera;
                SSphere cSphere = objects[i] as SSphere;
                SLight cLight = objects[i] as SLight;
                ICuttableToTriangles cICuttableToTriangles = objects[i] as ICuttableToTriangles;

                if (cCam != null)
                {
                    cam = cCam;
                }
                else if (cSphere != null)
                {
                    spheres.Add(cSphere);
                }
                else if (cLight != null)
                {
                    lights.Add(cLight);
                }
                else if (cICuttableToTriangles != null)
                {
                    triangles.AddRange(cICuttableToTriangles.GetTriangles());
                }
                
            }

        }


        

        public List<SMaterial> materials;
        public List<SObject> objects;

        public List<SSphere> spheres;
        public List<STriangle> triangles;
        public List<SLight> lights;
        public SCamera cam;

        public void ChangeCamRotation(object sender, EventArgs e)
        {
            cam.rotation.Y = ((TrackBar)sender).Value;
            ReSendObject(cam);

            

        }

        public View()
        {
            materials = InitMaterials();
            objects = InitObjects();
            SplitObjects(objects, out cam, out spheres, out triangles, out lights);
            
        }

        public void ReSendMaterial(SMaterial material)
        {
            int id = materials.FindIndex(x => x == material);
            materials[id].SendToShader(basicProgramID, "materials[" + id+"]");
        }

        public void ReSendObject(SObject obj)
        {
            SCamera cCam = obj as SCamera;
            SSphere cSphere = obj as SSphere;
            SLight cLight = obj as SLight;
            ICuttableToTriangles cICuttableToTriangles = obj as ICuttableToTriangles;

            if (cCam != null)
            {
                cCam.SendToShader(basicProgramID, "uCamera");
            }
            else if (cSphere != null)
            {
                int id = spheres.FindIndex(x => x == cSphere);
                cSphere.SendToShader(basicProgramID, "spheres[" + id + "]");
            }
            else if (cLight != null)
            {
                int id = lights.FindIndex(x => x == cLight);
                cLight.SendToShader(basicProgramID, "spheres[" + id + "]");
            }
            else if (cICuttableToTriangles != null)
            {
                List<STriangle> trigs = cICuttableToTriangles.GetTriangles();
                for (int i = 0; i < trigs.Count; i++)
                {
                    int id = triangles.FindIndex(x => x == trigs[i]);
                    trigs[i].SendToShader(basicProgramID, "triangles[" + id + "]");
                }
            }
        }
        public void ReSendObject(int num)
        {
            ReSendObject(objects[num]);
        }

        int basicProgramID;

        Vector3[] vertdata = new Vector3[] {
                new Vector3(-1f, -1f, 0f),
                new Vector3( 1f, -1f, 0f),
                new Vector3( 1f, 1f, 0f),
                new Vector3(-1f, 1f, 0f) };

        public int maxDepth = 5;
        public void UpdateMaxDepth()
        {
            int uniformLoc = GL.GetUniformLocation(basicProgramID, "maxDepth");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, maxDepth);
            }
        }

        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.DrawArrays(PrimitiveType.Quads, 0, vertdata.Length);
        }

        public void InitShaders()
        {
            int basicVertexShader;
            int basicFragmentShader;
            basicProgramID = GL.CreateProgram();
            LoadShader("../../raytracing.vert", ShaderType.VertexShader, basicProgramID, out basicVertexShader);
            LoadShader("../../raytracing.frag", ShaderType.FragmentShader, basicProgramID, out basicFragmentShader);

            GL.BindAttribLocation(basicProgramID, 1, "scale");
            
            GL.LinkProgram(basicProgramID);
            int status = 0;
            GL.GetProgram(basicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(GL.GetProgramInfoLog(basicProgramID));


            
            int attrLoc = GL.GetAttribLocation(basicProgramID, "scale");
            if (attrLoc != -1)
            {
                GL.VertexAttrib1(attrLoc, 1f);
            }




            //INIT
            Vector3 camright, camup, camforward;
            SObject.GetLocalVectors(cam.rotation, out camright, out camup, out camforward);

            Matrix4 viewMat = Matrix4.LookAt(cam.position, camforward, camup);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.LoadMatrix(ref viewMat);

            int vbo_position;
            GL.GenBuffers(1, out vbo_position);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length *
                                                                      Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);


            GL.UseProgram(basicProgramID);

            cam.SendToShader(basicProgramID, "uCamera");
            SendListToShader(materials.Select(x => (ISendableToShader)x).ToList(), basicProgramID, "materials");
            SendListToShader(spheres.Select(x => (ISendableToShader)x).ToList(), basicProgramID, "spheres");
            SendListToShader(triangles.Select(x => (ISendableToShader)x).ToList(), basicProgramID, "triangles");
            SendListToShader(lights.Select(x => (ISendableToShader)x).ToList(), basicProgramID, "lights");

            int uniformLoc = GL.GetUniformLocation(basicProgramID, "maxDepth");
            if (uniformLoc != -1)
            {
                GL.Uniform1(uniformLoc, maxDepth);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, 0);
        }

        private void LoadShader(String filename, ShaderType shaderType, int program, out int address)
        {
            address = GL.CreateShader(shaderType);
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));

        }
    }
}
