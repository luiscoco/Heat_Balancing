using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ClaseEquipos;

namespace Drag_AND_Drop_between_Forms
{
    public partial class ListaCompresores25 : Form
    {
        Aplicacion puntero1;

        public ListaCompresores25(Aplicacion puntero)
        {
            puntero1 = puntero;

            InitializeComponent();
        }

        private void ListaCompresores25_Load(object sender, EventArgs e)
        {
            LeerEquipos();
        }

        //Función para leer los Objetos de la lista de Equipos (equipos11) de la aplicación principal
        private void LeerEquipos()
        {
            for (int i = 0; i < puntero1.equipos11.Count; i++)
            {
                //IMPORTANTE: Modificar en Refactoring. Elegimos el Tipo de Equipo que queremos incluir en la lista de Equipos
                if (puntero1.equipos11[i].tipoequipo2 == 25)
                {
                    listBox1.Items.Add("Equipo Nº: " + Convert.ToString(puntero1.equipos11[i].numequipo2) + "   Tipo Equipo: " + Convert.ToString(puntero1.equipos11[i].tipoequipo2));
                }
            }
        }

        //Botón Add...
        private void button2_Click(object sender, EventArgs e)
        {
            puntero1.numequipos++;
            //Argumento 4 del constructor de la Clase Condcontorno :New =1
            Compresor25  compresor25 = new Compresor25(puntero1, puntero1.numecuaciones, puntero1.numvariables, 0, 0);
            if (compresor25.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                listBox1.Items.Add("Equipo Nº: " + Convert.ToString(puntero1.equipos11[puntero1.numequipos - 1].numequipo2) + "   Tipo Equipo: " + Convert.ToString(25));
            }
        }

        //Botón Edit
        private void button1_Click(object sender, EventArgs e)
        {
            String elemento;
            Int32 numeroequipo11 = 0;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                //Si el equipo esta selecciondo en la lista de equipos tipo 24, guardamos en la variable numeroequipo11 el número de equipo seleccionado en la lista
                if (listBox1.GetSelected(i) == true)
                {
                    elemento = listBox1.Items[i].ToString();
                    numeroequipo11 = Convert.ToInt32(elemento.Substring(10, 4));
                }
            }

            int indice = 0;
            int marca = 0;

            for (int j = 0; j < puntero1.equipos11.Count; j++)
            {
                if (puntero1.equipos11[j].numequipo2 == numeroequipo11)
                {
                    indice = j;
                    marca = 1;
                    goto maria;
                }
            }

            if (marca == 0)
            {
                MessageBox.Show("Error no se ha encontrado el número de Equipo en la lista de Equipos.");
            }

            maria:

            Compresor25 compresor25 = new Compresor25(puntero1, puntero1.numecuaciones, puntero1.numvariables, 1, indice);

            //Unidades
            //Sistema Britanico=0;Sistema Internacional=2;Sistema Métrico=1

            //Dependiendo de las unidades elegidas en la Aplicación principal se realiza una conversión 
            //de los valores de los parámetros (D1 a D9) guardados en el array de equipos "equipos11" para visualizarlo en el cuadro de diálogo en las unidades elegidas
            //Hay que tener en cuenta que dentro del array equipos11 siempre se guardan los parámetros (D1 al D9) en unidades del Sistema Británico porque son las utilizadas por las Tablas de Vapor ASME

            //Si las Unidades de la Aplicación son del Sistema Métrico
            if (puntero1.unidades == 1)
            {
                compresor25.textBox1.Text = Convert.ToString(puntero1.equipos11[indice].aD1);
                //Presión  psia a Bar
                compresor25.textBox10.Text = Convert.ToString(puntero1.equipos11[indice].aD2 * (6.8947572 / 100));
            }

            //Si las Unidades de la Aplicación son del Sistema Internacional
            else if (puntero1.unidades == 2)
            {
                compresor25.textBox1.Text = Convert.ToString(puntero1.equipos11[indice].aD1);
                //Presión psia a Bar
                compresor25.textBox10.Text = Convert.ToString(puntero1.equipos11[indice].aD2 * (6.8947572 / 100));
            }

            //Si las Unidades de la Aplicación son del Sistema Británico
            else if (puntero1.unidades == 0)
            {
                compresor25.textBox1.Text = Convert.ToString(puntero1.equipos11[indice].aD1);
                //Presión psia
                compresor25.textBox10.Text = Convert.ToString(puntero1.equipos11[indice].aD2);
            }

            compresor25.textBox7.Text = Convert.ToString(puntero1.equipos11[indice].aN1);
            compresor25.textBox8.Text = Convert.ToString(puntero1.equipos11[indice].aN3);
            compresor25.textBox9.Text = Convert.ToString(puntero1.equipos11[indice].numequipo2);

            compresor25.ShowDialog();

            //Leemos la lista de Equipos ya actualizada
            listBox1.Items.Clear();
            LeerEquipos();
        }

        //Botón Delete
        private void button3_Click(object sender, EventArgs e)
        {
            String elemento;
            Int32 numeroequipo11 = 0;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    elemento = listBox1.Items[i].ToString();
                    numeroequipo11 = Convert.ToInt32(elemento.Substring(10, 4));
                }
            }

            int indice = 0;
            int marca = 0;

            for (int j = 0; j < puntero1.equipos11.Count; j++)
            {
                if (puntero1.equipos11[j].numequipo2 == numeroequipo11)
                {
                    indice = j;
                    marca = 1;
                    goto maria;
                }
            }

            if (marca == 0)
            {
                MessageBox.Show("Error no se ha encontrado el número de Equipo en la lista de Equipos.");
            }

            maria:

            puntero1.equipos11.RemoveAt(indice);

            //Leemos la lista de Equipos ya actualizada
            listBox1.Items.Clear();
            LeerEquipos();
        }
    }
}
