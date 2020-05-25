using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK;

namespace lab3
{
    public partial class Form1 : Form
    {
        int frameCount = 0;
        DateTime nextFPSUpdate = DateTime.Now.AddSeconds(1);

        View view = new View();

        SObject choosenObj;

        SCube cube;
        SSphere sphere;
        SMaterial material;
        public Form1()
        {
            InitializeComponent();
            Application.Idle += Idle;
            trackBar1.ValueChanged += view.ChangeCamRotation;

            cube = (SCube)view.objects.Find(x => x.GetType() == typeof(SCube));
            sphere = (SSphere)view.objects.Find(x => x.GetType() == typeof(SSphere));

            inMatId.Maximum = inSphereMat.Maximum = inCubeMat.Maximum = view.materials.Count-1;
            inMatId.Minimum = inSphereMat.Minimum = inCubeMat.Minimum = 0;

            inCubePosX.Text = cube.position.X.ToString();
            inCubePosY.Text = cube.position.Y.ToString();
            inCubePosZ.Text = cube.position.Z.ToString();
            inCubeMat.Value = cube.materialID;

            inSpherePosX.Text = sphere.position.X.ToString();
            inSpherePosY.Text = sphere.position.Y.ToString();
            inSpherePosZ.Text = sphere.position.Z.ToString();
            inSphereRad.Text = sphere.radius.ToString();
            inSphereMat.Value = sphere.materialID;

            inMatId_ValueChanged(null, null);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            //view.Render();

            //glControl1.SwapBuffers();
        }

        void DisplayFPS()
        {
            frameCount++;
            if (DateTime.Now>nextFPSUpdate)
            {
                this.Text = "FPS = " + (float)frameCount/(DateTime.Now-nextFPSUpdate.AddSeconds(-1)).TotalSeconds;
                nextFPSUpdate = DateTime.Now.AddSeconds(1);
                frameCount = 0;
            }
        }

        private void Idle(object sender, EventArgs e)
        {
            
            view.Render();

            glControl1.SwapBuffers();
            DisplayFPS();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            view.InitShaders();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Idle -= Idle;
            trackBar1.ValueChanged -= view.ChangeCamRotation;
        }

        private void cubeApply_Click(object sender, EventArgs e)
        {
            Vector3 pos = new Vector3();

            bool ok = true;
            try
            {
                pos.X = float.Parse(inCubePosX.Text);
            }
            catch
            {
                inCubePosX.Text = "";
                ok = false;
            }
            try
            {
                pos.Y = float.Parse(inCubePosY.Text);
            }
            catch
            {
                inCubePosY.Text = "";
                ok = false;
            }
            try
            {
                pos.Z = float.Parse(inCubePosZ.Text);
            }
            catch
            {
                inCubePosZ.Text = "";
                ok = false;
            }

            if (ok)
            {
                cube.Update(pos, cube.right, cube.up, cube.forward, cube.scale, inCubeMat.Value);
                view.ReSendObject(cube);
            }
        }

        private void sphereApply_Click(object sender, EventArgs e)
        {
            Vector3 pos = new Vector3();
            float radius=0;
            bool ok = true;

            try
            {
                radius = float.Parse(inSphereRad.Text);
            }
            catch
            {
                inSphereRad.Text = "";
                ok = false;
            }
            try
            {
                pos.X = float.Parse(inSpherePosX.Text);
            }
            catch
            {
                inSpherePosX.Text = "";
                ok = false;
            }
            try
            {
                pos.Y = float.Parse(inSpherePosY.Text);
            }
            catch
            {
                inSpherePosY.Text = "";
                ok = false;
            }
            try
            {
                pos.Z = float.Parse(inSpherePosZ.Text);
            }
            catch
            {
                inSpherePosZ.Text = "";
                ok = false;
            }

            if (ok)
            {
                sphere.Update(pos, radius, inSphereMat.Value);
                view.ReSendObject(sphere);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Vector3 color=new Vector3();
            Vector4 lightCoeffs = new Vector4();
            float reflection = 0;
            float refraction = 0; 
            float refractCoef = 0;

            bool ok = true;

            try
            {
                color.X = float.Parse(inMatColorR.Text);
            }
            catch
            {
                inMatColorR.Text = "";
                ok = false;
            }
            try
            {
                color.Y = float.Parse(inMatColorG.Text);
            }
            catch
            {
                inMatColorG.Text = "";
                ok = false;
            }
            try
            {
                color.Z = float.Parse(inMatColorB.Text);
            }
            catch
            {
                inMatColorB.Text = "";
                ok = false;
            }

            try
            {
                reflection = float.Parse(inMatReflect.Text)/100;
            }
            catch
            {
                inMatReflect.Text = "";
                ok = false;
            }
            try
            {
                refraction = float.Parse(inMatRefract.Text) / 100;
            }
            catch
            {
                inMatRefract.Text = "";
                ok = false;
            }

            try
            {
                refractCoef = float.Parse(inMatRefractCoef.Text);
            }
            catch
            {
                inMatRefractCoef.Text = "";
                ok = false;
            }

            try
            {
                lightCoeffs.X = float.Parse(inMatLC1.Text);
            }
            catch
            {
                inMatLC1.Text = "";
                ok = false;
            }
            try
            {
                lightCoeffs.Y = float.Parse(inMatLC2.Text);
            }
            catch
            {
                inMatLC2.Text = "";
                ok = false;
            }
            try
            {
                lightCoeffs.Z = float.Parse(inMatLC3.Text);
            }
            catch
            {
                inMatLC3.Text = "";
                ok = false;
            }
            try
            {
                lightCoeffs.W = float.Parse(inMatLC4.Text);
            }
            catch
            {
                inMatLC4.Text = "";
                ok = false;
            }

            material.Update(color, lightCoeffs, reflection, refraction, refractCoef);
            view.ReSendMaterial(material);
        }

        private void inMatId_ValueChanged(object sender, EventArgs e)
        {
            material = view.materials[inMatId.Value];
            inMatColorR.Text = material.color.X.ToString();
            inMatColorG.Text = material.color.Y.ToString();
            inMatColorB.Text = material.color.Z.ToString();
            inMatLC1.Text = material.lightCoeffs.X.ToString();
            inMatLC2.Text = material.lightCoeffs.Y.ToString();
            inMatLC3.Text = material.lightCoeffs.Z.ToString();
            inMatLC4.Text = material.lightCoeffs.W.ToString();
            inMatReflect.Text = (material.reflectionPercent * 100).ToString();
            inMatRefract.Text = (material.refractionPercent * 100).ToString();
            inMatRefractCoef.Text = material.refractionCoef.ToString();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            view.maxDepth = trackBar2.Value;
            view.UpdateMaxDepth();
        }
    }
}
