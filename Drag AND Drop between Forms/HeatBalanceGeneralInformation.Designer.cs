namespace Drag_AND_Drop_between_Forms
{
    partial class HeatBalanceGeneralInformation
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.treeView3 = new System.Windows.Forms.TreeView();
            this.listBox6 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.SystemColors.Control;
            this.treeView1.Location = new System.Drawing.Point(3, 5);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(308, 471);
            this.treeView1.TabIndex = 29;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(423, 482);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 29);
            this.button1.TabIndex = 30;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView2
            // 
            this.treeView2.Location = new System.Drawing.Point(317, 5);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(311, 201);
            this.treeView2.TabIndex = 31;
            this.treeView2.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView2_NodeMouseClick_1);
            // 
            // treeView3
            // 
            this.treeView3.Location = new System.Drawing.Point(634, 5);
            this.treeView3.Name = "treeView3";
            this.treeView3.Size = new System.Drawing.Size(325, 201);
            this.treeView3.TabIndex = 32;
            this.treeView3.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView3_NodeMouseClick_1);
            // 
            // listBox6
            // 
            this.listBox6.FormattingEnabled = true;
            this.listBox6.HorizontalScrollbar = true;
            this.listBox6.Location = new System.Drawing.Point(317, 212);
            this.listBox6.Name = "listBox6";
            this.listBox6.Size = new System.Drawing.Size(311, 264);
            this.listBox6.TabIndex = 33;
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.HorizontalScrollbar = true;
            this.listBox3.Location = new System.Drawing.Point(634, 212);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(325, 264);
            this.listBox3.TabIndex = 34;
            // 
            // HeatBalanceGeneralInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 523);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox6);
            this.Controls.Add(this.treeView3);
            this.Controls.Add(this.treeView2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Name = "HeatBalanceGeneralInformation";
            this.Text = "HeatBalanceGeneralInformation";
            this.Load += new System.EventHandler(this.HeatBalanceGeneralInformation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.TreeView treeView3;
        private System.Windows.Forms.ListBox listBox6;
        private System.Windows.Forms.ListBox listBox3;
    }
}