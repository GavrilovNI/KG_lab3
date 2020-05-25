namespace lab3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl1 = new OpenTK.GLControl();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.inCubePosX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.inCubePosY = new System.Windows.Forms.TextBox();
            this.inCubePosZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.inCubeMat = new System.Windows.Forms.TrackBar();
            this.inSphereMat = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.inSpherePosZ = new System.Windows.Forms.TextBox();
            this.inSpherePosY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.inSpherePosX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.inMatColorB = new System.Windows.Forms.TextBox();
            this.inMatColorG = new System.Windows.Forms.TextBox();
            this.inMatColorR = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.inMatReflect = new System.Windows.Forms.TextBox();
            this.inMatRefract = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.inMatRefractCoef = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.inMatId = new System.Windows.Forms.TrackBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.inSphereRad = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.inMatLC3 = new System.Windows.Forms.TextBox();
            this.inMatLC2 = new System.Windows.Forms.TextBox();
            this.inMatLC1 = new System.Windows.Forms.TextBox();
            this.inMatLC4 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inCubeMat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inSphereMat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inMatId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(13, 13);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(450, 450);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 470);
            this.trackBar1.Maximum = 360;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(450, 56);
            this.trackBar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(471, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "cube:";
            // 
            // inCubePosX
            // 
            this.inCubePosX.Location = new System.Drawing.Point(511, 34);
            this.inCubePosX.Name = "inCubePosX";
            this.inCubePosX.Size = new System.Drawing.Size(47, 22);
            this.inCubePosX.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(471, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "pos:";
            // 
            // inCubePosY
            // 
            this.inCubePosY.Location = new System.Drawing.Point(564, 34);
            this.inCubePosY.Name = "inCubePosY";
            this.inCubePosY.Size = new System.Drawing.Size(47, 22);
            this.inCubePosY.TabIndex = 5;
            // 
            // inCubePosZ
            // 
            this.inCubePosZ.Location = new System.Drawing.Point(617, 34);
            this.inCubePosZ.Name = "inCubePosZ";
            this.inCubePosZ.Size = new System.Drawing.Size(47, 22);
            this.inCubePosZ.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(471, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "material:";
            // 
            // inCubeMat
            // 
            this.inCubeMat.Location = new System.Drawing.Point(539, 65);
            this.inCubeMat.Name = "inCubeMat";
            this.inCubeMat.Size = new System.Drawing.Size(216, 56);
            this.inCubeMat.TabIndex = 8;
            // 
            // inSphereMat
            // 
            this.inSphereMat.Location = new System.Drawing.Point(539, 192);
            this.inSphereMat.Name = "inSphereMat";
            this.inSphereMat.Size = new System.Drawing.Size(216, 56);
            this.inSphereMat.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(471, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "material:";
            // 
            // inSpherePosZ
            // 
            this.inSpherePosZ.Location = new System.Drawing.Point(617, 140);
            this.inSpherePosZ.Name = "inSpherePosZ";
            this.inSpherePosZ.Size = new System.Drawing.Size(47, 22);
            this.inSpherePosZ.TabIndex = 13;
            // 
            // inSpherePosY
            // 
            this.inSpherePosY.Location = new System.Drawing.Point(564, 140);
            this.inSpherePosY.Name = "inSpherePosY";
            this.inSpherePosY.Size = new System.Drawing.Size(47, 22);
            this.inSpherePosY.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(471, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "pos:";
            // 
            // inSpherePosX
            // 
            this.inSpherePosX.Location = new System.Drawing.Point(511, 140);
            this.inSpherePosX.Name = "inSpherePosX";
            this.inSpherePosX.Size = new System.Drawing.Size(47, 22);
            this.inSpherePosX.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(470, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "materials:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(471, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "id:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(470, 312);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "color:";
            // 
            // inMatColorB
            // 
            this.inMatColorB.Location = new System.Drawing.Point(617, 309);
            this.inMatColorB.Name = "inMatColorB";
            this.inMatColorB.Size = new System.Drawing.Size(47, 22);
            this.inMatColorB.TabIndex = 21;
            // 
            // inMatColorG
            // 
            this.inMatColorG.Location = new System.Drawing.Point(564, 309);
            this.inMatColorG.Name = "inMatColorG";
            this.inMatColorG.Size = new System.Drawing.Size(47, 22);
            this.inMatColorG.TabIndex = 20;
            // 
            // inMatColorR
            // 
            this.inMatColorR.Location = new System.Drawing.Point(511, 309);
            this.inMatColorR.Name = "inMatColorR";
            this.inMatColorR.Size = new System.Drawing.Size(47, 22);
            this.inMatColorR.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(470, 371);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "reflection(%):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(471, 399);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 17);
            this.label10.TabIndex = 23;
            this.label10.Text = "refraction(%):";
            // 
            // inMatReflect
            // 
            this.inMatReflect.Location = new System.Drawing.Point(564, 371);
            this.inMatReflect.Name = "inMatReflect";
            this.inMatReflect.Size = new System.Drawing.Size(47, 22);
            this.inMatReflect.TabIndex = 24;
            // 
            // inMatRefract
            // 
            this.inMatRefract.Location = new System.Drawing.Point(564, 399);
            this.inMatRefract.Name = "inMatRefract";
            this.inMatRefract.Size = new System.Drawing.Size(47, 22);
            this.inMatRefract.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(471, 430);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 17);
            this.label11.TabIndex = 26;
            this.label11.Text = "refractionCoef:";
            // 
            // inMatRefractCoef
            // 
            this.inMatRefractCoef.Location = new System.Drawing.Point(578, 430);
            this.inMatRefractCoef.Name = "inMatRefractCoef";
            this.inMatRefractCoef.Size = new System.Drawing.Size(47, 22);
            this.inMatRefractCoef.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(680, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 28;
            this.button1.Text = "apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cubeApply_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(680, 230);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 31);
            this.button2.TabIndex = 29;
            this.button2.Text = "apply";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.sphereApply_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(680, 432);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 31);
            this.button3.TabIndex = 30;
            this.button3.Text = "apply";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // inMatId
            // 
            this.inMatId.Location = new System.Drawing.Point(500, 278);
            this.inMatId.Name = "inMatId";
            this.inMatId.Size = new System.Drawing.Size(255, 56);
            this.inMatId.TabIndex = 18;
            this.inMatId.ValueChanged += new System.EventHandler(this.inMatId_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(471, 120);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 17);
            this.label12.TabIndex = 31;
            this.label12.Text = "sphere:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(470, 172);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 17);
            this.label13.TabIndex = 32;
            this.label13.Text = "radius:";
            // 
            // inSphereRad
            // 
            this.inSphereRad.Location = new System.Drawing.Point(539, 169);
            this.inSphereRad.Name = "inSphereRad";
            this.inSphereRad.Size = new System.Drawing.Size(47, 22);
            this.inSphereRad.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(471, 337);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 17);
            this.label14.TabIndex = 34;
            this.label14.Text = "lightCoefs:";
            // 
            // inMatLC3
            // 
            this.inMatLC3.Location = new System.Drawing.Point(657, 334);
            this.inMatLC3.Name = "inMatLC3";
            this.inMatLC3.Size = new System.Drawing.Size(47, 22);
            this.inMatLC3.TabIndex = 37;
            // 
            // inMatLC2
            // 
            this.inMatLC2.Location = new System.Drawing.Point(604, 334);
            this.inMatLC2.Name = "inMatLC2";
            this.inMatLC2.Size = new System.Drawing.Size(47, 22);
            this.inMatLC2.TabIndex = 36;
            // 
            // inMatLC1
            // 
            this.inMatLC1.Location = new System.Drawing.Point(551, 334);
            this.inMatLC1.Name = "inMatLC1";
            this.inMatLC1.Size = new System.Drawing.Size(47, 22);
            this.inMatLC1.TabIndex = 35;
            // 
            // inMatLC4
            // 
            this.inMatLC4.Location = new System.Drawing.Point(710, 334);
            this.inMatLC4.Name = "inMatLC4";
            this.inMatLC4.Size = new System.Drawing.Size(47, 22);
            this.inMatLC4.TabIndex = 38;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(468, 480);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 17);
            this.label15.TabIndex = 39;
            this.label15.Text = "Max Depth:";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(551, 470);
            this.trackBar2.Maximum = 15;
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(206, 56);
            this.trackBar2.TabIndex = 40;
            this.trackBar2.Value = 15;
            this.trackBar2.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 506);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.inMatLC4);
            this.Controls.Add(this.inMatLC3);
            this.Controls.Add(this.inMatLC2);
            this.Controls.Add(this.inMatLC1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.inSphereRad);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.inMatRefractCoef);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.inMatRefract);
            this.Controls.Add(this.inMatReflect);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.inMatColorB);
            this.Controls.Add(this.inMatColorG);
            this.Controls.Add(this.inMatColorR);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.inSphereMat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inSpherePosZ);
            this.Controls.Add(this.inSpherePosY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.inSpherePosX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.inCubeMat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inCubePosZ);
            this.Controls.Add(this.inCubePosY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inCubePosX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.inMatId);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inCubeMat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inSphereMat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inMatId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inCubePosX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inCubePosY;
        private System.Windows.Forms.TextBox inCubePosZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar inCubeMat;
        private System.Windows.Forms.TrackBar inSphereMat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox inSpherePosZ;
        private System.Windows.Forms.TextBox inSpherePosY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox inSpherePosX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox inMatColorB;
        private System.Windows.Forms.TextBox inMatColorG;
        private System.Windows.Forms.TextBox inMatColorR;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox inMatReflect;
        private System.Windows.Forms.TextBox inMatRefract;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox inMatRefractCoef;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TrackBar inMatId;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox inSphereRad;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox inMatLC3;
        private System.Windows.Forms.TextBox inMatLC2;
        private System.Windows.Forms.TextBox inMatLC1;
        private System.Windows.Forms.TextBox inMatLC4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TrackBar trackBar2;
    }
}

