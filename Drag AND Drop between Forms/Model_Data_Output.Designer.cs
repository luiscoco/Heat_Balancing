namespace Drag_AND_Drop_between_Forms
{
    partial class Model_Data_Output
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
            this.label48 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label47 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(79, 19);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(120, 13);
            this.label48.TabIndex = 27;
            this.label48.Text = "Equipments Information:";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(657, 44);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(65, 21);
            this.comboBox3.TabIndex = 26;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(656, 19);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(66, 13);
            this.label47.TabIndex = 25;
            this.label47.Text = "Set Number:";
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(79, 44);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(559, 388);
            this.listBox2.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(684, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 31);
            this.button1.TabIndex = 28;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Model_Data_Output
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.listBox2);
            this.Name = "Model_Data_Output";
            this.Text = "Model_Data_Output";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label48;
        public System.Windows.Forms.ComboBox comboBox3;
        public System.Windows.Forms.Label label47;
        public System.Windows.Forms.ListBox listBox2;
        public System.Windows.Forms.Button button1;
    }
}