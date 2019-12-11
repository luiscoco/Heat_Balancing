namespace Drag_AND_Drop_between_Forms
{
    partial class Equations
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
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.listView3 = new System.Windows.Forms.ListView();
            this.listView2 = new System.Windows.Forms.ListView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.listBox5 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(21, 17);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(262, 13);
            this.label28.TabIndex = 15;
            this.label28.Text = "Nº equations greater than  number of output variables:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(21, 187);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(247, 13);
            this.label29.TabIndex = 14;
            this.label29.Text = "Nº equations less than  number of output variables:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(18, 360);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(243, 13);
            this.label35.TabIndex = 13;
            this.label35.Text = "Nº equations equal to  number of output variables:";
            // 
            // listView3
            // 
            this.listView3.Location = new System.Drawing.Point(21, 33);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(281, 148);
            this.listView3.TabIndex = 12;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.List;
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(18, 203);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(284, 154);
            this.listView2.TabIndex = 11;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.List;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(18, 379);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(284, 110);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // listBox5
            // 
            this.listBox5.FormattingEnabled = true;
            this.listBox5.Location = new System.Drawing.Point(324, 33);
            this.listBox5.Name = "listBox5";
            this.listBox5.Size = new System.Drawing.Size(284, 472);
            this.listBox5.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(264, 524);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 26);
            this.button1.TabIndex = 17;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Equations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 562);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox5);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Name = "Equations";
            this.Text = "Equations";
            this.Load += new System.EventHandler(this.Equations_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ListView listView3;
        public System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListBox listBox5;
        private System.Windows.Forms.Button button1;
    }
}