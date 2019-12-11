namespace Drag_AND_Drop_between_Forms
{
    partial class Model_Data_Input
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
            this.label45 = new System.Windows.Forms.Label();
            this.button59 = new System.Windows.Forms.Button();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label45
            // 
            this.label45.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(31, 22);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(522, 44);
            this.label45.TabIndex = 10;
            this.label45.Text = "Note: Model Input Code in the following TextBox is Editable. Click \"Read Data Inp" +
    "ut\" button after your modifications to update Heat Balance Model.";
            // 
            // button59
            // 
            this.button59.Location = new System.Drawing.Point(238, 395);
            this.button59.Name = "button59";
            this.button59.Size = new System.Drawing.Size(91, 41);
            this.button59.TabIndex = 9;
            this.button59.Text = "Read Data Input";
            this.button59.UseVisualStyleBackColor = true;
            // 
            // textBox14
            // 
            this.textBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.Location = new System.Drawing.Point(34, 69);
            this.textBox14.Multiline = true;
            this.textBox14.Name = "textBox14";
            this.textBox14.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox14.Size = new System.Drawing.Size(519, 311);
            this.textBox14.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(589, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 36);
            this.button1.TabIndex = 11;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Model_Data_Input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.button59);
            this.Controls.Add(this.textBox14);
            this.Name = "Model_Data_Input";
            this.Text = "Model_Data_Input";
            this.Load += new System.EventHandler(this.Model_Data_Input_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Button button59;
        public System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Button button1;
    }
}