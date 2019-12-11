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

    public partial class Grid_Size : Form
    {
        public Aplicacion puntero2;        

        public Grid_Size(Aplicacion puntero1)
        {
            puntero2 = puntero1;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            puntero2.grid_width = Convert.ToInt32(textBox1.Text);
            puntero2.grid_height = Convert.ToInt32(textBox2.Text);
            puntero2.grid_thickness = Convert.ToInt32(textBox3.Text);

            puntero2.designer1.Document.gridSize = new Size(puntero2.grid_width, puntero2.grid_height);
            puntero2.designer1.Document.gridanchura = puntero2.grid_thickness;

            this.Hide();
        }
    }
}

