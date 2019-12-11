using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Drag_AND_Drop_between_Forms;

namespace RefPropWindowsForms
{
    public partial class Configuration_Form : Form
    {
        public Aplicacion Mainwindow_pointer;

        public Configuration_Form(Aplicacion Mainwindow_pointer1)
        {
            Mainwindow_pointer = Mainwindow_pointer1;
            InitializeComponent();
        }

        //Button Ok in Configuration Window
        private void button2_Click(object sender, EventArgs e)
        {
            GetPath(Mainwindow_pointer);
            MessageBox.Show("You set the REFPROP library path to:" + Mainwindow_pointer.Fluids_Path_LCE);
            this.Dispose();
        }

        public void GetPath(Aplicacion luis)
        {
            Mainwindow_pointer.Fluids_Path_LCE = textBox1.Text;
        }


    }
}
