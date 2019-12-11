namespace RefPropWindowsForms
{
    partial class REFPROP_Interface
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(109, 95);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(66, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "74000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pressure (kPa):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Temperature (K):";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(109, 121);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(66, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "300";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(242, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Density (kg/m3):";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(333, 101);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(66, 20);
            this.textBox3.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(324, 240);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Ok";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 69);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 13);
            this.label15.TabIndex = 42;
            this.label15.Text = "Refrigerant State:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 44);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 13);
            this.label16.TabIndex = 41;
            this.label16.Text = "Refrigerant Name:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(107, 13);
            this.label17.TabIndex = 40;
            this.label17.Text = "Refrigerant Category:";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "DEF",
            "NBP",
            "ASH",
            "IIR"});
            this.comboBox3.Location = new System.Drawing.Point(109, 66);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(67, 21);
            this.comboBox3.TabIndex = 39;
            this.comboBox3.Text = "DEF";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "ACETONE",
            "AMMONIA",
            "ARGON",
            "BENZENE",
            "BUTANE",
            "C12",
            "CO",
            "CO2",
            "CYCLOHEX",
            "CYCLOPEN",
            "DECANE",
            "DME",
            "ETHANE",
            "ETHANOL",
            "ETHYLENE",
            "H2S",
            "HELIUM",
            "HEPTANE",
            "HEXANE",
            "HYDROGEN",
            "IPENTANE",
            "ISOBUTAN",
            "KRYPTON",
            "METHANE",
            "METHANOL",
            "N2O",
            "NEON",
            "NITROGEN",
            "NONANE",
            "OCTANE",
            "OXYGEN",
            "PARAHYD",
            "PENTANE",
            "PROPANE",
            "PROPYLEN",
            "R123",
            "R1234YF",
            "R1234ZE",
            "R125",
            "R134A",
            "R143A",
            "R152A",
            "R22",
            "R227EA",
            "R23",
            "R245FA",
            "R32",
            "SF6",
            "SO2",
            "TOLUENE",
            "WATER",
            "XENON"});
            this.comboBox2.Location = new System.Drawing.Point(109, 41);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(67, 21);
            this.comboBox2.TabIndex = 38;
            this.comboBox2.Text = "CO2";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "PureFluid",
            "PredefinedMixture",
            "NewMixture",
            "PseudoPureFluid"});
            this.comboBox1.Location = new System.Drawing.Point(109, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(67, 21);
            this.comboBox1.TabIndex = 37;
            this.comboBox1.Text = "PureFluid";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Enthalpy (kJ/kg):";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(333, 129);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(66, 20);
            this.textBox4.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(239, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "Entropy (kJ/kg K):";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(333, 155);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(66, 20);
            this.textBox5.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "Critical Pressure (kPa):";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(333, 39);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(66, 20);
            this.textBox6.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(207, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "Critical Temperature (K):";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(333, 16);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(66, 20);
            this.textBox7.TabIndex = 47;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(207, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Critical Density (kg/m3):";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(333, 65);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(66, 20);
            this.textBox8.TabIndex = 51;
            // 
            // REFPROP_Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 285);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "REFPROP_Interface";
            this.Text = "REFPROP C# Interface, writen by: Luis Coco Enriquez";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox8;
    }
}

