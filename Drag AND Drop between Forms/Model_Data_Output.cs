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
    public partial class Model_Data_Output : Form
    {
        public Model_Data_Output()
        {
            InitializeComponent();
        }

        //Ok Button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
