﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NumericalMethods;
using NumericalMethods.FourthBlog;


namespace Drag_AND_Drop_between_Forms
{
    public partial class Bomba : Form
    {
        Double D1,D2, D3, D4, D5,D7, D8, D9;
        Double correntrada, corrsalida;
        Double numequipo;

        Double numecuaciones2;
        Double numvariables2;

        //Lista de cadenas para gardar los nombres de las variables del sistema de ecuaciones
        List<String> variables1 = new List<String>();

        //Lista de cadenas que guardan las ecuaciones del sistema
        List<String> ecuaciones1 = new List<String>();

        Aplicacion punteroaplicacion1;

        Double numparametroscreados = 0;

        int auxiliar = 0;

        public Bomba(Aplicacion punteroaplicion,Double numecuaciones1,Double numvariables1)
        {
            InitializeComponent();           

            punteroaplicacion1 = punteroaplicion;
            numparametroscreados = (punteroaplicacion1.numcorrientes) * 3;

            //Inicializamos las etiquetas de las unidades de entrada en el cuadro de dialogo de toma de datos
            if (punteroaplicacion1.unidades == 0)
            {

            }
            else if (punteroaplicacion1.unidades == 1)
            {

                label15.Text = "kPa";

            }
            else if (punteroaplicacion1.unidades == 2)
            {
                label15.Text = "Bar";
                
            }
            else
            {

            }
            D1 = 0;
            D2 = 0;
            D3= 0;
            D4 = 0;
            D5 = 0;
            D7 = 0;
            D8 = 0;
            D9 = 0;

            correntrada = 0;
            corrsalida = 0;

            numequipo = 0;

            numecuaciones2 = numecuaciones1;

            numvariables2 = numvariables1;
        
        }

     
        //Botón de Generar Ecuaciones
        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            //CREAMOS EL ARRAY DE PARAMETROS
            for (int v = (int)numparametroscreados; v < ((numparametroscreados) + 3); v++)
            {
                int randomNumber = random.Next(0, 2500);
                //Creamos la lista de parámetros generadas por este programa
                punteroaplicacion1.p.Add(punteroaplicacion1.ptemp);
                punteroaplicacion1.p[v] = new Parameter(randomNumber, 0.01, "");
            }
            //INCREMENTAMOS EL NÚMERO DE CORRIENTES
            punteroaplicacion1.numcorrientes = punteroaplicacion1.numcorrientes + 1;

            D1=Convert.ToDouble(textBox1.Text);
            D2=Convert.ToDouble(textBox2.Text);
            D3=Convert.ToDouble(textBox3.Text);
            D4=Convert.ToDouble(textBox4.Text);
            D5=Convert.ToDouble(textBox5.Text);
            D7=Convert.ToDouble(textBox6.Text);
            D8=Convert.ToDouble(textBox10.Text);
            D9=Convert.ToDouble(textBox11.Text);

            correntrada=Convert.ToDouble(textBox7.Text);
            corrsalida = Convert.ToDouble(textBox8.Text);

            numequipo = Convert.ToDouble(textBox9.Text);

            //Conversión UNIDADES
            //Como las Tablas de Vapor de Agua ASME 1967 están en unidades británicas, siempre tenemos que convertir los datos de entrada a Unidades Británicas

            //Unidades Métricas (W Kgr/sg P Bar H Kj/Kgr)
            if (punteroaplicacion1.unidades == 2)
            {
                
                //Presión Bar a psia
                D5 = D5 / (6.8947572 / 100);
                D1 = D1 / (6.8947572 / 100);

            }

            //Unidades Sistema Internacional (W Kgr/sg, P kPa, H Kj/Kgr)
            else if (punteroaplicacion1.unidades == 1)
            {
               
                //Presión Bar a psia
                D5 = D5 / (6.8947572);
                D1 = D1 / (6.8947572);
            }

            //Unidades Sistema Británico
            else if (punteroaplicacion1.unidades == 0)
            {

            }
            else
            {

            }

            ecuaciones1=generaecucaiones(D1,D2,D3,D4,D5,D7,D8,D9,correntrada,corrsalida);

            listBox1.Items.Add("Nº Equipo:" + Convert.ToString(numequipo));

            for (int numecua = 0; numecua < auxiliar; numecua++)
            {
                listBox1.Items.Add(ecuaciones1[numecua]);
            }

            listBox1.Items.Add("");

            button2.Enabled = true;
            
        }

        private List<String> generaecucaiones(Double D1, Double D2,Double D3,Double D4, Double D5,Double D7, Double D8, Double D9, Double correntrada, Double corrsalida)
        {
            //Lista de cadenas que guardan las ecuaciones del sistema
            List<String> ecuaciones2 = new List<String>();
            

            punteroaplicacion1.p[(int)numparametroscreados ].Nombre = "W" + Convert.ToString(corrsalida);
            punteroaplicacion1.p[(int)numparametroscreados + 1].Nombre = "P" + Convert.ToString(corrsalida);
            punteroaplicacion1.p[(int)numparametroscreados + 2].Nombre = "H" + Convert.ToString(corrsalida);

            Parameter W1 = new Parameter();
            Parameter W2 = new Parameter();
            Parameter P1 = new Parameter();
            Parameter P2 = new Parameter();
            Parameter H1 = new Parameter();
            Parameter H2 = new Parameter();

            W1 = punteroaplicacion1.p.Find(p => p.Nombre == "W" + Convert.ToString(correntrada));
            W2 = punteroaplicacion1.p.Find(p => p.Nombre == "W" + Convert.ToString(corrsalida));
            P1 = punteroaplicacion1.p.Find(p => p.Nombre == "P" + Convert.ToString(correntrada));
            P2 = punteroaplicacion1.p.Find(p => p.Nombre == "P" + Convert.ToString(corrsalida));
            H1 = punteroaplicacion1.p.Find(p => p.Nombre == "H" + Convert.ToString(correntrada));
            H2 = punteroaplicacion1.p.Find(p => p.Nombre == "H" + Convert.ToString(corrsalida));

            ecuaciones2.Add("");
            ecuaciones2[auxiliar] = "W" + Convert.ToString(correntrada) + "-" + "W" + Convert.ToString(corrsalida);
            Func<Double> primeraecuacion = () => W1 - W2;
            punteroaplicacion1.functions.Add(primeraecuacion);
            auxiliar++;

            if(D4>0)
            {     
               ecuaciones2.Add("");
               ecuaciones2[auxiliar] = "W" + Convert.ToString(correntrada) + "-" + "(" + "H" + Convert.ToString(corrsalida) + "-" + "H" + Convert.ToString(correntrada) + ")"+"(TDH x W1)/Rend.";
               Func<Double> segundaecuacion = () =>W1*(H2-H1)-((punteroaplicacion1.tabla(D8,W1,2)*W1)/D4);
               punteroaplicacion1.functions.Add(segundaecuacion);      
               auxiliar++;
            }

            // Calculo de la Presión de Descarga
            if (D9==0)
            {
                ecuaciones2.Add("");
                ecuaciones2[auxiliar] = "P" + Convert.ToString(corrsalida) + "-" + Convert.ToString(D5);
                Func<Double> terceraecuacion = () => P2 - D5;
                punteroaplicacion1.functions.Add(terceraecuacion);
                auxiliar++;
            }

            // Calculo del TDH
            else if (D9 == 1)
            {
                ecuaciones2.Add("");
                ecuaciones2[auxiliar] = "P" + Convert.ToString(corrsalida) + "-" + "P" + Convert.ToString(correntrada) + "x" + Convert.ToString(D9) + "-" + "TDH";
                Func<Double> terceraecuacion = () => P2 - P1 * D9 - punteroaplicacion1.tabla(D8, W1, 2);
                punteroaplicacion1.functions.Add(terceraecuacion);
                auxiliar++;
            }

            else if ((D1>0)||(D2>0)||(D3>0))
            {
                ecuaciones2.Add("");
                ecuaciones2[auxiliar] = "P" + Convert.ToString(corrsalida) + "-" + "P" + Convert.ToString(correntrada) +"*"+Convert.ToString(D9) + "-" + "AP";
                Func<Double> terceraecuacion = () => P2 - P1 * D9 - (D1+D2*(W1/D7)+D3*(W1*W1/D7));
                punteroaplicacion1.functions.Add(terceraecuacion);
                auxiliar++;           
            }

            variables1.Add("");
            variables1[0] = "W" + Convert.ToString(corrsalida);
            variables1.Add("");
            variables1[1] = "P" + Convert.ToString(corrsalida);
            variables1.Add("");
            variables1[2] = "H" + Convert.ToString(corrsalida);

            numecuaciones2 = auxiliar;
            numvariables2 = 3;
           
            return (ecuaciones2);
        }

        //Botón de OK
        private void button2_Click(object sender, EventArgs e)
        {
            int j;
            j = 0;

            for (int i = (int)punteroaplicacion1.numecuaciones; i < numecuaciones2 + (int)punteroaplicacion1.numecuaciones; i++)
            {
                    punteroaplicacion1.ecuaciones.Add("");
                    punteroaplicacion1.ecuaciones[i] = ecuaciones1[j];
                    j++;
            }

            int f;
            f = 0;

            for (int i = (int)punteroaplicacion1.numvariables; i < numvariables2 + (int)punteroaplicacion1.numvariables; i++)
            {
                punteroaplicacion1.variables.Add("");
                punteroaplicacion1.variables[i] = variables1[f];
                f++;
            }
            
            punteroaplicacion1.numecuaciones = numecuaciones2+punteroaplicacion1.numecuaciones;
            punteroaplicacion1.numvariables = numvariables2+punteroaplicacion1.numvariables;
                                   
            this.Hide();

        }

        //Botón de Cancel
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}