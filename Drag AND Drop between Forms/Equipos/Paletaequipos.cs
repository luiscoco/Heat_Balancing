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
    public partial class Paletaequipos : Form
    {
        Aplicacion punteroaplicacion2;

        public Paletaequipos(Aplicacion punteroaplicacion1)
        {
            punteroaplicacion2 = punteroaplicacion1;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }



        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
          

        }

        private void button10_MouseMove(object sender, MouseEventArgs e)
        {
            //Si el boton pulsado al arrastrar no es el izquierdo.
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            punteroaplicacion2.tipoequipodrag = 1;
            
            Button boton1 = button10;
            //Arrastra el boton desde el Form1
            button10.DoDragDrop(boton1, DragDropEffects.Move);
        }      
       

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void listView4_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void listView4_ItemDrag(object sender, ItemDragEventArgs e)
        {
            
        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}
