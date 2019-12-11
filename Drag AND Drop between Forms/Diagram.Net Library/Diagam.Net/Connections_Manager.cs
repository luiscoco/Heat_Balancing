using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Drag_AND_Drop_between_Forms.Diagram.Net_Library.Diagam.Net
{
    public partial class Connections_Manager : Form
    {
        public Color connectionsFillColor1 = new Color();

        public Connections_Manager()
        {
            InitializeComponent();
        }

        //Connections Fill Color
        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                connectionsFillColor1 = colorDialog1.Color;
            }
        }

        //OK button
        private void button2_Click(object sender, EventArgs e)
        {

        }

        //Cancel button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
