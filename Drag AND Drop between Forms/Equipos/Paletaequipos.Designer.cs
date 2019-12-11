namespace Drag_AND_Drop_between_Forms
{
    partial class Paletaequipos
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Paletaequipos));
            this.button10 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // button10
            // 
            this.button10.Image = global::Drag_AND_Drop_between_Forms.Properties.Resources.Fuente;
            this.button10.Location = new System.Drawing.Point(102, 15);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(42, 44);
            this.button10.TabIndex = 10;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            this.button10.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button10_MouseMove);
            // 
            // button5
            // 
            this.button5.Image = global::Drag_AND_Drop_between_Forms.Properties.Resources.Cursor;
            this.button5.Location = new System.Drawing.Point(54, 15);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(42, 44);
            this.button5.TabIndex = 5;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Image = global::Drag_AND_Drop_between_Forms.Properties.Resources.Cursor1;
            this.button4.Location = new System.Drawing.Point(6, 15);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(42, 44);
            this.button4.TabIndex = 4;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "Boundary Condition Type 1.bmp");
            this.imageList2.Images.SetKeyName(1, "Condensador1.bmp");
            this.imageList2.Images.SetKeyName(2, "Bomba.bmp");
            this.imageList2.Images.SetKeyName(3, "Condensador.bmp");
            this.imageList2.Images.SetKeyName(4, "Cursor.bmp");
            this.imageList2.Images.SetKeyName(5, "Cursor1.bmp");
            this.imageList2.Images.SetKeyName(6, "Calentador.bmp");
            this.imageList2.Images.SetKeyName(7, "Desgasificador.bmp");
            this.imageList2.Images.SetKeyName(8, "MSR.bmp");
            this.imageList2.Images.SetKeyName(9, "SepHumedad.bmp");
            this.imageList2.Images.SetKeyName(10, "Valvula.bmp");
            // 
            // Paletaequipos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 373);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Name = "Paletaequipos";
            this.Text = "Equipments";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button10;
        public System.Windows.Forms.ImageList imageList2;
    }
}

