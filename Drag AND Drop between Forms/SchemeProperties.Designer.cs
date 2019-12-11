namespace Drag_AND_Drop_between_Forms
{
    partial class SchemeProperties
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
            this.propertyGrid10 = new System.Windows.Forms.PropertyGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // propertyGrid10
            // 
            this.propertyGrid10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid10.Location = new System.Drawing.Point(12, 12);
            this.propertyGrid10.Name = "propertyGrid10";
            this.propertyGrid10.Size = new System.Drawing.Size(356, 385);
            this.propertyGrid10.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(138, 519);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 34);
            this.button1.TabIndex = 6;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtLog.Location = new System.Drawing.Point(12, 403);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(354, 101);
            this.txtLog.TabIndex = 7;
            // 
            // SchemeProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 565);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.propertyGrid10);
            this.Name = "SchemeProperties";
            this.Text = "SchemeProperties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PropertyGrid propertyGrid10;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txtLog;
    }
}