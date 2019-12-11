using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc.net;
using Drag_AND_Drop_between_Forms;

namespace Drag_AND_Drop_between_Forms
{

    public partial class Design_Manager: Form
    {
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
        public Aplicacion puntero2;
        private Button button3;
        private CheckBox checkBox2;
        private GroupBox groupBox1;
        private CheckBox checkBox3;
        private TextBox textBox2;
        private Label label3;
        private TextBox textBox1;
        private Label label2;
        private GroupBox groupBox2;
        private Button button4;
        private Button button6;
        private Button button5;
        public Color arrowFillColor1 = new Color();
        public Color linesBorderColor1 = new Color();
        private Button button7;
        private Button button8;
        private Button button9;
        private TextBox textBox3;
        private Label label1;
        private TextBox textBox4;
        private Label label4;
        private CheckBox checkBox4;
        public Color arrowsBorderColor1 = new Color();

        public Design_Manager(Aplicacion puntero1)
        {
            puntero2 = puntero1;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(503, 427);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(606, 427);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 33);
            this.button2.TabIndex = 1;
            this.button2.Text = "Ok";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(15, 28);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(143, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Arrow in the connections";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 138);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 25);
            this.button3.TabIndex = 3;
            this.button3.Text = "Connections arrows fill color";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(15, 51);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(228, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Fill with color the arrows in the connections";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 426);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(23, 307);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(259, 17);
            this.checkBox4.TabIndex = 19;
            this.checkBox4.Text = "Arrows at the begining and at the connection end";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(121, 242);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(21, 20);
            this.textBox3.TabIndex = 18;
            this.textBox3.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Arrow Border Width:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(121, 268);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(21, 20);
            this.textBox4.TabIndex = 16;
            this.textBox4.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Line Border Width:";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(164, 200);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(143, 25);
            this.button7.TabIndex = 14;
            this.button7.Text = "Selection arrows color";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(164, 169);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(143, 25);
            this.button8.TabIndex = 13;
            this.button8.Text = "Selection lines color";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(164, 138);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(143, 25);
            this.button9.TabIndex = 12;
            this.button9.Text = "Selection arrows fill color";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(15, 200);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(143, 25);
            this.button6.TabIndex = 11;
            this.button6.Text = "Connections arrows color";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(15, 169);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(143, 25);
            this.button5.TabIndex = 10;
            this.button5.Text = "Connections lines color";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(85, 100);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(36, 20);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "45";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Arrow angle:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(85, 74);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(36, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "15";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Arrow width:";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(16, 29);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(74, 17);
            this.checkBox3.TabIndex = 7;
            this.checkBox3.Text = "AutoScroll";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Location = new System.Drawing.Point(455, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 190);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Screen Options";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(16, 61);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(143, 25);
            this.button4.TabIndex = 8;
            this.button4.Text = "Select background color";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Design_Manager
            // 
            this.ClientSize = new System.Drawing.Size(718, 472);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Design_Manager";
            this.Text = "Design_Manager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        //Ok button
        private void button2_Click(object sender, EventArgs e)
        {
            puntero2.designer1.arrowInConnections = checkBox1.Checked;
            puntero2.designer1.Document.arrowInConnections = checkBox1.Checked;

            puntero2.designer1.fillArrowsWithColor = checkBox2.Checked;
            puntero2.designer1.Document.fillArrowsWithColor = checkBox2.Checked;

            puntero2.designer1.arrowColor = arrowFillColor1;
            puntero2.designer1.Document.arrowColor = arrowFillColor1;

            puntero2.designer1.linesBorderColor = linesBorderColor1;
            puntero2.designer1.Document.linesBorderColor = linesBorderColor1;

            puntero2.designer1.arrowsBorderColor = arrowsBorderColor1;
            puntero2.designer1.Document.arrowsBorderColor = arrowsBorderColor1;

            if (puntero2.designer1.arrowColor.IsEmpty == true) { puntero2.designer1.arrowColor = Color.Black; }
            if (puntero2.designer1.Document.arrowColor.IsEmpty == true) { puntero2.designer1.Document.arrowColor = Color.Black; }

            if (puntero2.designer1.linesBorderColor.IsEmpty == true) { puntero2.designer1.linesBorderColor = Color.Black; }
            if (puntero2.designer1.Document.linesBorderColor.IsEmpty == true) { puntero2.designer1.Document.linesBorderColor = Color.Black; }

            if (puntero2.designer1.arrowsBorderColor.IsEmpty == true) { puntero2.designer1.arrowsBorderColor = Color.Black; }
            if (puntero2.designer1.Document.arrowsBorderColor.IsEmpty == true) { puntero2.designer1.Document.arrowsBorderColor = Color.Black; }

            puntero2.designer1.arrowsBorderWidth = Convert.ToSingle(textBox3.Text);
            puntero2.designer1.Document.arrowsBorderWidth = Convert.ToSingle(textBox3.Text);

            puntero2.designer1.linesBorderWidth = Convert.ToSingle(textBox4.Text);
            puntero2.designer1.Document.linesBorderWidth = Convert.ToSingle(textBox4.Text);

            puntero2.designer1.ArrowWith = Convert.ToInt32(textBox1.Text);
            puntero2.designer1.Document.ArrowWith = Convert.ToInt32(textBox1.Text);

            puntero2.designer1.ArrowAngle = Convert.ToSingle(textBox2.Text);
            puntero2.designer1.Document.ArrowAngle = Convert.ToSingle(textBox2.Text);

            puntero2.designer1.arrowsAtBeginingAndAtEnd = checkBox4.Checked;
            puntero2.designer1.Document.arrowsAtBeginingAndAtEnd = checkBox4.Checked;

            this.Hide();
        }

        //Cancel button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Arrows Fill color
        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                arrowFillColor1 = colorDialog1.Color;
            }
        }

        //Lines Border color
        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog2 = new ColorDialog();

            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                linesBorderColor1 = colorDialog2.Color;
            }
        }

        //Arrows Border color
        private void button6_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog3 = new ColorDialog();

            if (colorDialog3.ShowDialog() == DialogResult.OK)
            {
                arrowsBorderColor1 = colorDialog3.Color;
            }
        }

        //Set Background Color
        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
