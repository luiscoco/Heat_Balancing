using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Drag_AND_Drop_between_Forms
{
    public partial class Model_Data_Input : Form
    {
        Aplicacion puntero1;

        public Model_Data_Input(Aplicacion puntero)
        {
            puntero1 = puntero;

            InitializeComponent();
        }

        //OK button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Model_Data_Input_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < puntero1.textBox14.Count(); i++)
            {
                textBox14.AppendText(puntero1.textBox14[i]);
            }
        }
    }
}
