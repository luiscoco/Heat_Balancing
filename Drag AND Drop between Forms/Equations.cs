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
    public partial class Equations : Form
    {
        List<String> listView1_local = new List<String>();
        List<String> listView2_local = new List<String>();
        List<String> listView3_local = new List<String>();
        List<String> listBox5_local = new List<String>();

        public Equations(List<String> listView1, List<String> listView2, List<String> listView3, List<String> listBox5)
        {
            listView1_local = listView1;
            listView2_local = listView2;
            listView3_local = listView3;
            listBox5_local = listBox5;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Equations_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1_local.Count; i++)
            {
                listView1.Items.Add(listView1_local[i]);
            }
            for (int j = 0; j < listView2_local.Count; j++)
            {
                listView2.Items.Add(listView2_local[j]);
            }
            for (int k = 0; k < listView3_local.Count; k++)
            {
                listView3.Items.Add(listView3_local[k]);
            }
        }
    }
}
