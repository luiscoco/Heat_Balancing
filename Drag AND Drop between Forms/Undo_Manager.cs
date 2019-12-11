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

    public partial class Undo_Manager : Form
    {
        public Aplicacion puntero2;

        public Undo_Manager(Aplicacion puntero1)
        {
            puntero2 = puntero1;

            InitializeComponent();
        }

        //Ok button
        private void button1_Click(object sender, EventArgs e)
        {
            puntero2.designer1.undo.canRedo = checkBox1.Checked;
            puntero2.designer1.undo.canUndo = checkBox2.Checked;
            puntero2.designer1.undo.enabled = checkBox3.Checked;

            puntero2.designer1.undo.capacity = Convert.ToInt32(textBox1.Text);
            textBox2.Text = Convert.ToString(puntero2.designer1.undo.lastPos);
            textBox3.Text = Convert.ToString(puntero2.designer1.undo.currPos);

            this.Hide();
        }

        //Cancel button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

