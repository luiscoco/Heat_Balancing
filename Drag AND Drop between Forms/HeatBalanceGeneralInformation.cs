using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClaseEquipos;

using System.Runtime.InteropServices;
using System.IO; //Para lectura y escritura de archivos
using System.Globalization;

//Acceso a la Librería de Diagram.Net
using System.Collections;
using Dalssoft.DiagramNet;

using NumericalMethods; //Método numérico de Newton Raphson
//using NumericalMethods.FourthBlog;  //Método numérico de Newton Raphson

using DotNumerics;
using DotNumerics.LinearAlgebra;

using WindowsFormsApplication2;

using Files_in_csharp; //Interface lectura archivos HBAL

using TablasAgua1967; //Tablas de Agua-Vapor ASME 1967

using sc.net;

using ZedGraphSample; //Ejemplo de Ploteo de Tablas definidas por el usuario

using System.Diagnostics;

using Drag_AND_Drop_between_Forms.DotNumerics;

using CSharpScripter;

using Dalssoft.TestForm;

//Tablas AGUA IAPWS 1997 
using Tablas_Vapor_ASME;

using Excel = Microsoft.Office.Interop.Excel;

using System.Windows.Forms.DataVisualization.Charting;

using CompiladorLUISCOCO;

using CSharpScripter2;

using DasslInterface;

using Monofasico;

using Bifasico;

using HeatExchangers;

using RefPropWindowsForms;

namespace Drag_AND_Drop_between_Forms
{
    public partial class HeatBalanceGeneralInformation : Form
    {
        public Aplicacion puntero1 = new Aplicacion();

        public HeatBalanceGeneralInformation(Aplicacion puntero)
        {
            puntero1 = puntero;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Botón para ACTUALIZAR la vista de ARBOL TREEVIEW 
        public void actualizararbol()
        {
            treeView1.Nodes.Clear();
            treeView2.Nodes.Clear();
            treeView3.Nodes.Clear();

            TreeNode luis = new TreeNode();

            TreeNode node1 = treeView1.Nodes.Add("DATOS GENERALES");
            node1.Nodes.Add("Título del Archivo: " + puntero1.Titulo);
            node1.Nodes.Add("Nº Total de Equipos: " + Convert.ToString(puntero1.NumTotalEquipos));
            node1.Nodes.Add("Nº Total de Corrientes: " + Convert.ToString(puntero1.NumTotalCorrientes));
            node1.Nodes.Add("Nº Total de Tablas: " + Convert.ToString(puntero1.NumTotalTablas));
            node1.Nodes.Add("Nº Máximo de Iteraciones: " + Convert.ToString(puntero1.NumMaxIteraciones));
            node1.Nodes.Add("Error Máximo Admisible: " + Convert.ToString(puntero1.ErrorMaxAdmisible));
            node1.Nodes.Add("Factor de Iteraciones (EPS): " + Convert.ToString(puntero1.FactorIteraciones));
            node1.Nodes.Add("Unidades: " + "Sistema Internacional");

            TreeNode node2 = treeView2.Nodes.Add("EQUIPOS");
            for (int i = 0; i < puntero1.equipos11.Count; i++)
            {
                TreeNode node7 = node2.Nodes.Add("Número de Equipo: " + Convert.ToString(puntero1.equipos11[i].numequipo2));
                TreeNode node8 = node7.Nodes.Add("Tipo de Equipo: " + Convert.ToString(puntero1.equipos11[i].tipoequipo2));
                TreeNode node9 = node7.Nodes.Add("Corriente Entrada1: " + Convert.ToString(puntero1.equipos11[i].aN1));
                TreeNode node10 = node7.Nodes.Add("Corriente Entrada2: " + Convert.ToString(puntero1.equipos11[i].aN2));
                TreeNode node11 = node7.Nodes.Add("Corriente Salida1: " + Convert.ToString(puntero1.equipos11[i].aN3));
                TreeNode node12 = node7.Nodes.Add("Corriente Salida2: " + Convert.ToString(puntero1.equipos11[i].aN4));
            }

            TreeNode node211 = treeView3.Nodes.Add("EQUIPOS");
            for (int i = 0; i < puntero1.equipos11.Count; i++)
            {
                TreeNode node71 = node211.Nodes.Add("Número de Equipo: " + Convert.ToString(puntero1.equipos11[i].numequipo2));
                TreeNode node81 = node71.Nodes.Add("Tipo de Equipo: " + Convert.ToString(puntero1.equipos11[i].tipoequipo2));
                TreeNode node91 = node71.Nodes.Add("Corriente Entrada1: " + Convert.ToString(puntero1.equipos11[i].aN1));
                TreeNode node101 = node71.Nodes.Add("Corriente Entrada2: " + Convert.ToString(puntero1.equipos11[i].aN2));
                TreeNode node111 = node71.Nodes.Add("Corriente Salida1: " + Convert.ToString(puntero1.equipos11[i].aN3));
                TreeNode node121 = node71.Nodes.Add("Corriente Salida2: " + Convert.ToString(puntero1.equipos11[i].aN4));

            }

            TreeNode node5 = treeView1.Nodes.Add("TABLAS");
            for (int j = 0; j < puntero1.listaTablas.Count; j++)
            {
                TreeNode node14 = node5.Nodes.Add("TÍTULO DE LA TABLA: " + puntero1.listaTituloTabla[j]);
                TreeNode node13 = node5.Nodes.Add("Número de Tabla: " + Convert.ToString(puntero1.listanumTablas[j]));
                TreeNode node15 = node5.Nodes.Add("Título de EjeX: " + puntero1.listaTituloEjeXTabla[j]);
                TreeNode node16 = node5.Nodes.Add("Título de EjeY: " + puntero1.listaTituloEjeYTabla[j]);
                TreeNode node17 = node5.Nodes.Add("Tipo de Interpolación: " + Convert.ToString(puntero1.listanumTipoInterpolacionTabla[j]));
            }

            TreeNode node20 = treeView1.Nodes.Add("ECUACIONES DEL MODELO");
            TreeNode node21 = node20.Nodes.Add("Nº Total de Ecuaciones del Modelo: " + Convert.ToString(puntero1.numecuaciones));

            TreeNode node22 = treeView1.Nodes.Add("VARIABLES DEL MODELO");
            TreeNode node23 = node22.Nodes.Add("Nº Total de Varibales del Modelo: " + Convert.ToString(puntero1.NumTotalCorrientes * 3));
            TreeNode node24 = node22.Nodes.Add("Valor de las Varibales del Modelo:");
            for (int j = 0; j < puntero1.p.Count; j++)
            {
                TreeNode node25 = node24.Nodes.Add(puntero1.p[j].Nombre + " :" + Convert.ToString(puntero1.p[j].Value));
            }

            TreeNode node18 = treeView1.Nodes.Add("CONDICIONES INICIALES");
            TreeNode node19 = node18.Nodes.Add("Nº Condiciones Iniciales: " + Convert.ToString(puntero1.numcondiciniciales));
            TreeNode node26 = node18.Nodes.Add("Valor de las Condiciones Iniciales:");
        }

        private void HeatBalanceGeneralInformation_Load(object sender, EventArgs e)
        {
            // Inicialización de los CONTROLES de ARBOL 
            // Inicializa el control arbol en el Tab: "Heat Balance General Information" 
            treeView1.Nodes.Clear();
            // Inicializa el control arbol en el Tab: "Equipment Data Input"
            treeView2.Nodes.Clear();
            // Inicializa el control arbol en el Tab: "Equipment Results"
            treeView3.Nodes.Clear();

            actualizararbol();
        }

        private void treeView3_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            listBox3.Items.Clear();

            TreeNode clickedNode = e.Node;

            Int16 numequipo = 0;

            ClassCondicionContorno1 condtemp = new ClassCondicionContorno1();
            condtemp.inicializar(0, 0, 0, 0, 0, 0);

            ClassDivisor2 divisortemp = new ClassDivisor2();
            divisortemp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassPerdidaCarga3 perdidacargatemp = new ClassPerdidaCarga3();
            perdidacargatemp.inicializar(0, 0, 0, 0, 0, 0);

            ClassBomba4 bombatemp = new ClassBomba4();
            bombatemp.inicializar(0, 0, 0, 0, 0, 0);

            ClassMezclador5 mezcladortemp = new ClassMezclador5();
            mezcladortemp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassReactor6 reactortemp = new ClassReactor6();
            reactortemp.inicializar(0, 0, 0, 0, 0, 0);

            ClassCalentador7 calentadortemp = new ClassCalentador7();
            calentadortemp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassCondensador8 condensadortemp = new ClassCondensador8();
            condensadortemp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassTurbina9 turbina9temp = new ClassTurbina9();
            turbina9temp.inicializar(0, 0, 0, 0, 0, 0);

            ClassTurbina10 turbina10temp = new ClassTurbina10();
            turbina10temp.inicializar(0, 0, 0, 0, 0, 0);

            ClassTurbina24 turbina24temp = new ClassTurbina24(puntero1);
            turbina10temp.inicializar(0, 0, 0, 0, 0, 0);

            ClassTurbina11 turbina11temp = new ClassTurbina11();
            turbina11temp.inicializar(0, 0, 0, 0, 0, 0);

            ClassSeparadorHumedad13 separadorhumedadtemp = new ClassSeparadorHumedad13();
            separadorhumedadtemp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassMSR14 MSRtemp = new ClassMSR14();
            MSRtemp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassCondensador15 condensador15tmp = new ClassCondensador15();
            condensador15tmp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassEnfriadorDrenajes16 enfriadortmp = new ClassEnfriadorDrenajes16();
            enfriadortmp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassAtemperador17 atemperadortmp = new ClassAtemperador17();
            atemperadortmp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassDesaireador18 desaireadortmp = new ClassDesaireador18();
            desaireadortmp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassValvula19 valvulatmp = new ClassValvula19();
            valvulatmp.inicializar(0, 0, 0, 0, 0, 0);

            ClassDivisorEntalpiaFija20 divientalpiatmp = new ClassDivisorEntalpiaFija20();
            divientalpiatmp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassTanqueVaporizacion21 tanquevapotmp = new ClassTanqueVaporizacion21();
            tanquevapotmp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0);

            ClassIntercambiador22 intercambiadortmp = new ClassIntercambiador22();
            intercambiadortmp.inicializar(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            if (clickedNode.Text == "EQUIPOS")
            {
                //MessageBox.Show(clickedNode.Text);
            }

            else
            {
                int indice = 0;
                double numeroequipotemporal = 0;

                string temp = clickedNode.Text;
                int longitud = clickedNode.Text.Length;

                string numequipotemp = temp.Substring(17, longitud - 17);
                numeroequipotemporal = Convert.ToInt16(numequipotemp);

                for (int j = 0; j < puntero1.equipos11.Count; j++)
                {
                    if (puntero1.equipos11[j].numequipo2 == numeroequipotemporal)
                    {
                        indice = j;
                    }
                }

                //Unidades del Sistema Internacional
                if (puntero1.unidades == 2)
                {
                    condtemp.unidades1 = 2;
                    divisortemp.unidades1 = 2;
                    perdidacargatemp.unidades1 = 2;
                    bombatemp.unidades1 = 2;
                    mezcladortemp.unidades1 = 2;
                    reactortemp.unidades1 = 2;
                    calentadortemp.unidades1 = 2;
                    condensadortemp.unidades1 = 2;
                    turbina9temp.unidades1 = 2;
                    turbina10temp.unidades1 = 2;
                    turbina11temp.unidades1 = 2;
                    separadorhumedadtemp.unidades1 = 2;
                    MSRtemp.unidades1 = 2;
                    condensador15tmp.unidades1 = 2;
                    enfriadortmp.unidades1 = 2;
                    atemperadortmp.unidades1 = 2;
                    desaireadortmp.unidades1 = 2;
                    valvulatmp.unidades1 = 2;
                    divientalpiatmp.unidades1 = 2;
                    tanquevapotmp.unidades1 = 2;
                    intercambiadortmp.unidades1 = 2;
                }

                if (puntero1.equipos11[indice].tipoequipo2 == 1)
                {
                    for (int j = 0; j < puntero1.numtipo1; j++)
                    {
                        if (puntero1.matrixcondicioncontorno1[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            condtemp.numcorrentrada = puntero1.matrixcondicioncontorno1[j, puntero1.setnumber].numcorrentrada;
                            condtemp.numcorrsalida = puntero1.matrixcondicioncontorno1[j, puntero1.setnumber].numcorrsalida;
                            condtemp.numequipo = puntero1.matrixcondicioncontorno1[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //condtemp.numcorrentrada = equipos11[indice].aN1;
                    //condtemp.numcorrsalida = equipos11[indice].aN3;
                    //condtemp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == condtemp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                condtemp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                condtemp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                condtemp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == condtemp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                condtemp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                condtemp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                condtemp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    condtemp.Calcular();
                }


                //-------------------------------  A PARTIR DE AQUI CONTINUAR CON LOS CAMBIOS REALIZADOS SOBRE LA CONDICIÓN DE CONTORNO --------------


                else if (puntero1.equipos11[indice].tipoequipo2 == 2)
                {
                    for (int j = 0; j < puntero1.numtipo2; j++)
                    {
                        if (puntero1.matrixdivisor2[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            divisortemp.numcorrentrada = puntero1.matrixdivisor2[j, puntero1.setnumber].numcorrentrada;
                            divisortemp.numcorrsalida1 = puntero1.matrixdivisor2[j, puntero1.setnumber].numcorrsalida1;
                            divisortemp.numcorrsalida2 = puntero1.matrixdivisor2[j, puntero1.setnumber].numcorrsalida2;
                            divisortemp.numequipo = puntero1.matrixdivisor2[j, puntero1.setnumber].numequipo;

                        }
                    }
                    //divisortemp.numcorrentrada = equipos11[indice].aN1;
                    //divisortemp.numcorrsalida1 = equipos11[indice].aN3;
                    //divisortemp.numcorrsalida2 = equipos11[indice].aN4;
                    //divisortemp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == divisortemp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                divisortemp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                divisortemp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                divisortemp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == divisortemp.numcorrsalida1)
                        {
                            if (tipoparametro == "W")
                            {
                                divisortemp.caudalcorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                divisortemp.presioncorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                divisortemp.entalpiacorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == divisortemp.numcorrsalida2)
                        {
                            if (tipoparametro == "W")
                            {
                                divisortemp.caudalcorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                divisortemp.presioncorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                divisortemp.entalpiacorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }

                    divisortemp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 3)
                {
                    for (int j = 0; j < puntero1.numtipo3; j++)
                    {
                        if (puntero1.matrixperdida3[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            perdidacargatemp.numcorrentrada = puntero1.matrixperdida3[j, puntero1.setnumber].numcorrentrada;
                            perdidacargatemp.numcorrsalida = puntero1.matrixperdida3[j, puntero1.setnumber].numcorrsalida;
                            perdidacargatemp.numequipo = puntero1.matrixperdida3[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //perdidacargatemp.numcorrentrada = equipos11[indice].aN1;
                    //perdidacargatemp.numcorrsalida = equipos11[indice].aN3;
                    //perdidacargatemp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == perdidacargatemp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                perdidacargatemp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                perdidacargatemp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                perdidacargatemp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == perdidacargatemp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                perdidacargatemp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                perdidacargatemp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                perdidacargatemp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }

                    perdidacargatemp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 4)
                {
                    for (int j = 0; j < puntero1.numtipo4; j++)
                    {
                        if (puntero1.matrixbomba4[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            bombatemp.numcorrentrada = puntero1.matrixbomba4[j, puntero1.setnumber].numcorrentrada;
                            bombatemp.numcorrsalida = puntero1.matrixbomba4[j, puntero1.setnumber].numcorrsalida;
                            bombatemp.numequipo = puntero1.matrixbomba4[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //bombatemp.numcorrentrada = equipos11[indice].aN1;
                    //bombatemp.numcorrsalida = equipos11[indice].aN3;
                    //bombatemp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == bombatemp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                bombatemp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                bombatemp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                bombatemp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == bombatemp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                bombatemp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                bombatemp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                bombatemp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    bombatemp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 5)
                {
                    for (int j = 0; j < puntero1.numtipo5; j++)
                    {
                        if (puntero1.matrixmezclador5[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            mezcladortemp.numcorrentrada1 = puntero1.matrixmezclador5[j, puntero1.setnumber].numcorrentrada1;
                            mezcladortemp.numcorrentrada2 = puntero1.matrixmezclador5[j, puntero1.setnumber].numcorrentrada2;
                            mezcladortemp.numcorrsalida = puntero1.matrixmezclador5[j, puntero1.setnumber].numcorrsalida;
                            mezcladortemp.numequipo = puntero1.matrixmezclador5[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //mezcladortemp.numcorrentrada1 = equipos11[indice].aN1;
                    //mezcladortemp.numcorrentrada2 = equipos11[indice].aN2;
                    //mezcladortemp.numcorrsalida = equipos11[indice].aN3;
                    //mezcladortemp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == mezcladortemp.numcorrentrada1)
                        {
                            if (tipoparametro == "W")
                            {
                                mezcladortemp.caudalcorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                mezcladortemp.presioncorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                mezcladortemp.entalpiacorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == mezcladortemp.numcorrentrada2)
                        {
                            if (tipoparametro == "W")
                            {
                                mezcladortemp.caudalcorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                mezcladortemp.presioncorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                mezcladortemp.entalpiacorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == mezcladortemp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                mezcladortemp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                mezcladortemp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                mezcladortemp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    mezcladortemp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 6)
                {
                    for (int j = 0; j < puntero1.numtipo6; j++)
                    {
                        if (puntero1.matrixreactor6[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            reactortemp.numcorrentrada = puntero1.matrixreactor6[j, puntero1.setnumber].numcorrentrada;
                            reactortemp.numcorrsalida = puntero1.matrixreactor6[j, puntero1.setnumber].numcorrsalida;
                            reactortemp.numequipo = puntero1.matrixreactor6[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //reactortemp.numcorrentrada = equipos11[indice].aN1;
                    //reactortemp.numcorrsalida = equipos11[indice].aN3;
                    //reactortemp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == reactortemp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                reactortemp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                reactortemp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                reactortemp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == reactortemp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                reactortemp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                reactortemp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                reactortemp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }

                    reactortemp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 7)
                {
                    for (int j = 0; j < puntero1.numtipo7; j++)
                    {
                        if (puntero1.matrixcalentador7[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            calentadortemp.numcorrentrada1 = puntero1.matrixcalentador7[j, puntero1.setnumber].numcorrentrada1;
                            calentadortemp.numcorrentrada2 = puntero1.matrixcalentador7[j, puntero1.setnumber].numcorrentrada2;
                            calentadortemp.numcorrsalida1 = puntero1.matrixcalentador7[j, puntero1.setnumber].numcorrsalida1;
                            calentadortemp.numcorrsalida2 = puntero1.matrixcalentador7[j, puntero1.setnumber].numcorrsalida2;
                            calentadortemp.numequipo = puntero1.matrixcalentador7[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //calentadortemp.numcorrentrada1 = equipos11[indice].aN1;
                    //calentadortemp.numcorrentrada2 = equipos11[indice].aN2;
                    //calentadortemp.numcorrsalida1 = equipos11[indice].aN3;
                    //calentadortemp.numcorrsalida2 = equipos11[indice].aN4;
                    //calentadortemp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == calentadortemp.numcorrentrada1)
                        {
                            if (tipoparametro == "W")
                            {
                                calentadortemp.caudalcorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                calentadortemp.presioncorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                calentadortemp.entalpiacorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == calentadortemp.numcorrentrada2)
                        {
                            if (tipoparametro == "W")
                            {
                                calentadortemp.caudalcorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                calentadortemp.presioncorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                calentadortemp.entalpiacorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == calentadortemp.numcorrsalida1)
                        {
                            if (tipoparametro == "W")
                            {
                                calentadortemp.caudalcorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                calentadortemp.presioncorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                calentadortemp.entalpiacorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == calentadortemp.numcorrsalida2)
                        {
                            if (tipoparametro == "W")
                            {
                                calentadortemp.caudalcorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                calentadortemp.presioncorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                calentadortemp.entalpiacorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    calentadortemp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 8)
                {
                    for (int j = 0; j < puntero1.numtipo8; j++)
                    {
                        if (puntero1.matrixcondensador8[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            condensadortemp.numcorrentrada1 = puntero1.matrixcondensador8[j, puntero1.setnumber].numcorrentrada1;
                            condensadortemp.numcorrentrada2 = puntero1.matrixcondensador8[j, puntero1.setnumber].numcorrentrada2;
                            condensadortemp.numcorrsalida = puntero1.matrixcondensador8[j, puntero1.setnumber].numcorrsalida;
                            condensadortemp.numequipo = puntero1.matrixcondensador8[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //condensadortemp.numcorrentrada1 = equipos11[indice].aN1;
                    //condensadortemp.numcorrentrada2 = equipos11[indice].aN2;
                    //condensadortemp.numcorrsalida = equipos11[indice].aN3;
                    //condensadortemp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == condensadortemp.numcorrentrada1)
                        {
                            if (tipoparametro == "W")
                            {
                                condensadortemp.caudalcorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                condensadortemp.presioncorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                condensadortemp.entalpiacorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == condensadortemp.numcorrentrada2)
                        {
                            if (tipoparametro == "W")
                            {
                                condensadortemp.caudalcorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                condensadortemp.presioncorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                condensadortemp.entalpiacorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == condensadortemp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                condensadortemp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                condensadortemp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                condensadortemp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    condensadortemp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 9)
                {
                    for (int j = 0; j < puntero1.numtipo9; j++)
                    {
                        if (puntero1.matrixturbina9[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            turbina9temp.numcorrentrada = puntero1.matrixturbina9[j, puntero1.setnumber].numcorrentrada;
                            turbina9temp.numcorrsalida = puntero1.matrixturbina9[j, puntero1.setnumber].numcorrsalida;
                            turbina9temp.numequipo = puntero1.matrixturbina9[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //turbina9temp.numcorrentrada = equipos11[indice].aN1;
                    //turbina9temp.numcorrsalida = equipos11[indice].aN3;
                    //turbina9temp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == turbina9temp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                turbina9temp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                turbina9temp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                turbina9temp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == turbina9temp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                turbina9temp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                turbina9temp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                turbina9temp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }

                    turbina9temp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 10)
                {
                    for (int j = 0; j < puntero1.numtipo10; j++)
                    {
                        if (puntero1.matrixturbina10[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            turbina10temp.numcorrentrada = puntero1.matrixturbina10[j, puntero1.setnumber].numcorrentrada;
                            turbina10temp.numcorrsalida = puntero1.matrixturbina10[j, puntero1.setnumber].numcorrsalida;
                            turbina10temp.numequipo = puntero1.matrixturbina10[j, puntero1.setnumber].numequipo;
                        }
                    }

                    // turbina10temp.numcorrentrada = equipos11[indice].aN1;
                    // turbina10temp.numcorrsalida = equipos11[indice].aN3;
                    // turbina10temp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.p[o].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == turbina10temp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                turbina10temp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                turbina10temp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                turbina10temp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == turbina10temp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                turbina10temp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                turbina10temp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                turbina10temp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }

                    turbina10temp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 11)
                {
                    for (int j = 0; j < puntero1.numtipo11; j++)
                    {
                        if (puntero1.matrixturbina11[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            turbina11temp.numcorrentrada = puntero1.matrixturbina11[j, puntero1.setnumber].numcorrentrada;
                            turbina11temp.numcorrsalida = puntero1.matrixturbina11[j, puntero1.setnumber].numcorrsalida;
                            turbina11temp.numequipo = puntero1.matrixturbina11[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //turbina11temp.numcorrentrada = equipos11[indice].aN1;
                    //turbina11temp.numcorrsalida = equipos11[indice].aN3;
                    //turbina11temp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == turbina11temp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                turbina11temp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                turbina11temp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                turbina11temp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == turbina11temp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                turbina11temp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                turbina11temp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                turbina11temp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }

                    turbina11temp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 13)
                {
                    for (int j = 0; j < puntero1.numtipo13; j++)
                    {
                        if (puntero1.matrixseparador13[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            separadorhumedadtemp.numcorrentrada = puntero1.matrixseparador13[j, puntero1.setnumber].numcorrentrada;
                            separadorhumedadtemp.numcorrsalida1 = puntero1.matrixseparador13[j, puntero1.setnumber].numcorrsalida1;
                            separadorhumedadtemp.numcorrsalida2 = puntero1.matrixseparador13[j, puntero1.setnumber].numcorrsalida2;
                            separadorhumedadtemp.numequipo = puntero1.matrixseparador13[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //separadorhumedadtemp.numcorrentrada = equipos11[indice].aN1;
                    //separadorhumedadtemp.numcorrsalida1 = equipos11[indice].aN3;
                    //separadorhumedadtemp.numcorrsalida2 = equipos11[indice].aN4;
                    //separadorhumedadtemp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == separadorhumedadtemp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                separadorhumedadtemp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                separadorhumedadtemp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                separadorhumedadtemp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == separadorhumedadtemp.numcorrsalida1)
                        {
                            if (tipoparametro == "W")
                            {
                                separadorhumedadtemp.caudalcorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                separadorhumedadtemp.presioncorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                separadorhumedadtemp.entalpiacorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == separadorhumedadtemp.numcorrsalida2)
                        {
                            if (tipoparametro == "W")
                            {
                                separadorhumedadtemp.caudalcorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                separadorhumedadtemp.presioncorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                separadorhumedadtemp.entalpiacorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }

                    separadorhumedadtemp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 14)
                {
                    for (int j = 0; j < puntero1.numtipo14; j++)
                    {
                        if (puntero1.matrixMSR14[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            MSRtemp.numcorrentrada1 = puntero1.matrixMSR14[j, puntero1.setnumber].numcorrentrada1;
                            MSRtemp.numcorrentrada2 = puntero1.matrixMSR14[j, puntero1.setnumber].numcorrentrada2;
                            MSRtemp.numcorrsalida1 = puntero1.matrixMSR14[j, puntero1.setnumber].numcorrsalida1;
                            MSRtemp.numcorrsalida2 = puntero1.matrixMSR14[j, puntero1.setnumber].numcorrsalida2;
                            MSRtemp.numequipo = puntero1.matrixMSR14[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //  MSRtemp.numcorrentrada1 = equipos11[indice].aN1;
                    //  MSRtemp.numcorrentrada2 = equipos11[indice].aN2;
                    //  MSRtemp.numcorrsalida1 = equipos11[indice].aN3;
                    //  MSRtemp.numcorrsalida2 = equipos11[indice].aN4;
                    //  MSRtemp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == MSRtemp.numcorrentrada1)
                        {
                            if (tipoparametro == "W")
                            {
                                MSRtemp.caudalcorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                MSRtemp.presioncorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                MSRtemp.entalpiacorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == MSRtemp.numcorrentrada2)
                        {
                            if (tipoparametro == "W")
                            {
                                MSRtemp.caudalcorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                MSRtemp.presioncorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                MSRtemp.entalpiacorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == MSRtemp.numcorrsalida1)
                        {
                            if (tipoparametro == "W")
                            {
                                MSRtemp.caudalcorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                MSRtemp.presioncorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                MSRtemp.entalpiacorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == MSRtemp.numcorrsalida2)
                        {
                            if (tipoparametro == "W")
                            {
                                MSRtemp.caudalcorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                MSRtemp.presioncorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                MSRtemp.entalpiacorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    MSRtemp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 15)
                {
                    for (int j = 0; j < puntero1.numtipo15; j++)
                    {
                        if (puntero1.matrixcondensador15[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            condensador15tmp.numcorrentrada1 = puntero1.matrixcondensador15[j, puntero1.setnumber].numcorrentrada1;
                            condensador15tmp.numcorrentrada2 = puntero1.matrixcondensador15[j, puntero1.setnumber].numcorrentrada2;
                            condensador15tmp.numcorrsalida1 = puntero1.matrixcondensador15[j, puntero1.setnumber].numcorrsalida1;
                            condensador15tmp.numcorrsalida2 = puntero1.matrixcondensador15[j, puntero1.setnumber].numcorrsalida2;
                            condensador15tmp.numequipo = puntero1.matrixcondensador15[j, puntero1.setnumber].numequipo;
                        }
                    }

                    // condensador15tmp.numcorrentrada1 = equipos11[indice].aN1;
                    // condensador15tmp.numcorrentrada2 = equipos11[indice].aN2;
                    // condensador15tmp.numcorrsalida1 = equipos11[indice].aN3;
                    // condensador15tmp.numcorrsalida2 = equipos11[indice].aN4;
                    // condensador15tmp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == condensador15tmp.numcorrentrada1)
                        {
                            if (tipoparametro == "W")
                            {
                                condensador15tmp.caudalcorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                condensador15tmp.presioncorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                condensador15tmp.entalpiacorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == condensador15tmp.numcorrentrada2)
                        {
                            if (tipoparametro == "W")
                            {
                                condensador15tmp.caudalcorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                condensador15tmp.presioncorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                condensador15tmp.entalpiacorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == condensador15tmp.numcorrsalida1)
                        {
                            if (tipoparametro == "W")
                            {
                                condensador15tmp.caudalcorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                condensador15tmp.presioncorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                condensador15tmp.entalpiacorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == condensador15tmp.numcorrsalida2)
                        {
                            if (tipoparametro == "W")
                            {
                                condensador15tmp.caudalcorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                condensador15tmp.presioncorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                condensador15tmp.entalpiacorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    condensador15tmp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 16)
                {
                    for (int j = 0; j < puntero1.numtipo16; j++)
                    {
                        if (puntero1.matrixenfriador16[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            enfriadortmp.numcorrentrada1 = puntero1.matrixenfriador16[j, puntero1.setnumber].numcorrentrada1;
                            enfriadortmp.numcorrentrada2 = puntero1.matrixenfriador16[j, puntero1.setnumber].numcorrentrada2;
                            enfriadortmp.numcorrsalida1 = puntero1.matrixenfriador16[j, puntero1.setnumber].numcorrsalida1;
                            enfriadortmp.numcorrsalida2 = puntero1.matrixenfriador16[j, puntero1.setnumber].numcorrsalida2;
                            enfriadortmp.numequipo = puntero1.matrixenfriador16[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //enfriadortmp.numcorrentrada1 = equipos11[indice].aN1;
                    //enfriadortmp.numcorrentrada2 = equipos11[indice].aN2;
                    //enfriadortmp.numcorrsalida1 = equipos11[indice].aN3;
                    //enfriadortmp.numcorrsalida2 = equipos11[indice].aN4;
                    //enfriadortmp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == enfriadortmp.numcorrentrada1)
                        {
                            if (tipoparametro == "W")
                            {
                                enfriadortmp.caudalcorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                enfriadortmp.presioncorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                enfriadortmp.entalpiacorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == enfriadortmp.numcorrentrada2)
                        {
                            if (tipoparametro == "W")
                            {
                                enfriadortmp.caudalcorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                enfriadortmp.presioncorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                enfriadortmp.entalpiacorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == enfriadortmp.numcorrsalida1)
                        {
                            if (tipoparametro == "W")
                            {
                                enfriadortmp.caudalcorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                enfriadortmp.presioncorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                enfriadortmp.entalpiacorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == enfriadortmp.numcorrsalida2)
                        {
                            if (tipoparametro == "W")
                            {
                                enfriadortmp.caudalcorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                enfriadortmp.presioncorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                enfriadortmp.entalpiacorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    enfriadortmp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 17)
                {
                    for (int j = 0; j < puntero1.numtipo17; j++)
                    {
                        if (puntero1.matrixatemperador17[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            atemperadortmp.numcorrentrada1 = puntero1.matrixatemperador17[j, puntero1.setnumber].numcorrentrada1;
                            atemperadortmp.numcorrentrada2 = puntero1.matrixatemperador17[j, puntero1.setnumber].numcorrentrada2;
                            atemperadortmp.numcorrsalida = puntero1.matrixatemperador17[j, puntero1.setnumber].numcorrsalida;
                            atemperadortmp.numequipo = puntero1.matrixatemperador17[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //atemperadortmp.numcorrentrada1 = equipos11[indice].aN1;
                    //atemperadortmp.numcorrentrada2 = equipos11[indice].aN2;
                    //atemperadortmp.numcorrsalida = equipos11[indice].aN3;
                    //atemperadortmp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == atemperadortmp.numcorrentrada1)
                        {
                            if (tipoparametro == "W")
                            {
                                atemperadortmp.caudalcorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                atemperadortmp.presioncorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                atemperadortmp.entalpiacorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == atemperadortmp.numcorrentrada2)
                        {
                            if (tipoparametro == "W")
                            {
                                atemperadortmp.caudalcorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                atemperadortmp.presioncorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                atemperadortmp.entalpiacorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == atemperadortmp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                atemperadortmp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                atemperadortmp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                atemperadortmp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    atemperadortmp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 18)
                {
                    for (int j = 0; j < puntero1.numtipo18; j++)
                    {
                        if (puntero1.matrixdesaireador18[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            desaireadortmp.numcorrentrada1 = puntero1.matrixdesaireador18[j, puntero1.setnumber].numcorrentrada1;
                            desaireadortmp.numcorrentrada2 = puntero1.matrixdesaireador18[j, puntero1.setnumber].numcorrentrada2;
                            desaireadortmp.numcorrsalida1 = puntero1.matrixdesaireador18[j, puntero1.setnumber].numcorrsalida1;
                            desaireadortmp.numcorrsalida2 = puntero1.matrixdesaireador18[j, puntero1.setnumber].numcorrsalida2;
                            desaireadortmp.numequipo = puntero1.matrixdesaireador18[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //desaireadortmp.numcorrentrada1 = equipos11[indice].aN1;
                    //desaireadortmp.numcorrentrada2 = equipos11[indice].aN2;
                    //desaireadortmp.numcorrsalida1 = equipos11[indice].aN3;
                    //desaireadortmp.numcorrsalida2 = equipos11[indice].aN4;
                    //desaireadortmp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == desaireadortmp.numcorrentrada1)
                        {
                            if (tipoparametro == "W")
                            {
                                desaireadortmp.caudalcorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                desaireadortmp.presioncorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                desaireadortmp.entalpiacorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == desaireadortmp.numcorrentrada2)
                        {
                            if (tipoparametro == "W")
                            {
                                desaireadortmp.caudalcorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                desaireadortmp.presioncorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                desaireadortmp.entalpiacorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == desaireadortmp.numcorrsalida1)
                        {
                            if (tipoparametro == "W")
                            {
                                desaireadortmp.caudalcorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                desaireadortmp.presioncorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                desaireadortmp.entalpiacorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == desaireadortmp.numcorrsalida2)
                        {
                            if (tipoparametro == "W")
                            {
                                desaireadortmp.caudalcorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                desaireadortmp.presioncorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                desaireadortmp.entalpiacorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    desaireadortmp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 19)
                {
                    for (int j = 0; j < puntero1.numtipo19; j++)
                    {
                        if (puntero1.matrixvalvula19[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            valvulatmp.numcorrentrada = puntero1.matrixvalvula19[j, puntero1.setnumber].numcorrentrada;
                            valvulatmp.numcorrsalida = puntero1.matrixvalvula19[j, puntero1.setnumber].numcorrsalida;
                            valvulatmp.numequipo = puntero1.matrixvalvula19[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //valvulatmp.numcorrentrada = equipos11[indice].aN1;
                    //valvulatmp.numcorrsalida = equipos11[indice].aN3;
                    //valvulatmp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == valvulatmp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                valvulatmp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                valvulatmp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                valvulatmp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == valvulatmp.numcorrsalida)
                        {
                            if (tipoparametro == "W")
                            {
                                valvulatmp.caudalcorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                valvulatmp.presioncorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                valvulatmp.entalpiacorrsalida = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    valvulatmp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 20)
                {
                    for (int j = 0; j < puntero1.numtipo20; j++)
                    {
                        if (puntero1.matrixdivisor20[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            divientalpiatmp.numcorrentrada = puntero1.matrixdivisor20[j, puntero1.setnumber].numcorrentrada;
                            divientalpiatmp.numcorrsalida1 = puntero1.matrixdivisor20[j, puntero1.setnumber].numcorrsalida1;
                            divientalpiatmp.numcorrsalida2 = puntero1.matrixdivisor20[j, puntero1.setnumber].numcorrsalida2;
                            divientalpiatmp.numequipo = puntero1.matrixdivisor20[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //divientalpiatmp.numcorrentrada = equipos11[indice].aN1;
                    //divientalpiatmp.numcorrsalida1 = equipos11[indice].aN3;
                    //divientalpiatmp.numcorrsalida2 = equipos11[indice].aN4;
                    //divientalpiatmp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == divientalpiatmp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                divientalpiatmp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                divientalpiatmp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                divientalpiatmp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == divientalpiatmp.numcorrsalida1)
                        {
                            if (tipoparametro == "W")
                            {
                                divientalpiatmp.caudalcorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                divientalpiatmp.presioncorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                divientalpiatmp.entalpiacorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == divientalpiatmp.numcorrsalida2)
                        {
                            if (tipoparametro == "W")
                            {
                                divientalpiatmp.caudalcorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                divientalpiatmp.presioncorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                divientalpiatmp.entalpiacorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }

                    divientalpiatmp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 21)
                {
                    for (int j = 0; j < puntero1.numtipo21; j++)
                    {
                        if (puntero1.matrixtanque21[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            tanquevapotmp.numcorrentrada = puntero1.matrixtanque21[j, puntero1.setnumber].numcorrentrada;
                            tanquevapotmp.numcorrsalida1 = puntero1.matrixtanque21[j, puntero1.setnumber].numcorrsalida1;
                            tanquevapotmp.numcorrsalida2 = puntero1.matrixtanque21[j, puntero1.setnumber].numcorrsalida2;
                            tanquevapotmp.numequipo = puntero1.matrixtanque21[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //tanquevapotmp.numcorrentrada = equipos11[indice].aN1;
                    //tanquevapotmp.numcorrsalida1 = equipos11[indice].aN3;
                    //tanquevapotmp.numcorrsalida2 = equipos11[indice].aN4;
                    //tanquevapotmp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == tanquevapotmp.numcorrentrada)
                        {
                            if (tipoparametro == "W")
                            {
                                tanquevapotmp.caudalcorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                tanquevapotmp.presioncorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                tanquevapotmp.entalpiacorrentrada = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == tanquevapotmp.numcorrsalida1)
                        {
                            if (tipoparametro == "W")
                            {
                                tanquevapotmp.caudalcorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                tanquevapotmp.presioncorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                tanquevapotmp.entalpiacorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == tanquevapotmp.numcorrsalida2)
                        {
                            if (tipoparametro == "W")
                            {
                                tanquevapotmp.caudalcorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                tanquevapotmp.presioncorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                tanquevapotmp.entalpiacorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }

                    tanquevapotmp.Calcular();
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 22)
                {
                    for (int j = 0; j < puntero1.numtipo22; j++)
                    {
                        if (puntero1.matrixintercambiador22[j, puntero1.setnumber].numequipo == puntero1.equipos11[indice].numequipo2)
                        {
                            intercambiadortmp.numcorrentrada1 = puntero1.matrixintercambiador22[j, puntero1.setnumber].numcorrentrada1;
                            intercambiadortmp.numcorrentrada2 = puntero1.matrixintercambiador22[j, puntero1.setnumber].numcorrentrada2;
                            intercambiadortmp.numcorrsalida1 = puntero1.matrixintercambiador22[j, puntero1.setnumber].numcorrsalida1;
                            intercambiadortmp.numcorrsalida2 = puntero1.matrixintercambiador22[j, puntero1.setnumber].numcorrsalida2;
                            intercambiadortmp.numequipo = puntero1.matrixintercambiador22[j, puntero1.setnumber].numequipo;
                        }
                    }

                    //intercambiadortmp.numcorrentrada1 = equipos11[indice].aN1;
                    //intercambiadortmp.numcorrentrada2 = equipos11[indice].aN2;
                    //intercambiadortmp.numcorrsalida1 = equipos11[indice].aN3;
                    //intercambiadortmp.numcorrsalida2 = equipos11[indice].aN4;
                    //intercambiadortmp.numequipo = equipos11[indice].numequipo2;

                    //Rastreamos la lista de Parámetros con los número de corriente de los equipos Tipo 9 y guardamos sus valores en la ClassTurbina9 
                    for (int o = 0; o < puntero1.p.Count; o++)
                    {
                        String nom = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Nombre;
                        int longitud1 = nom.Length;
                        String tem = nom.Substring(1, longitud1 - 1);
                        String tipoparametro = nom.Substring(0, 1);
                        Double numcorr = Convert.ToDouble(tem);

                        if (numcorr == intercambiadortmp.numcorrentrada1)
                        {
                            if (tipoparametro == "W")
                            {
                                intercambiadortmp.caudalcorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                intercambiadortmp.presioncorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                intercambiadortmp.entalpiacorrentrada1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == intercambiadortmp.numcorrentrada2)
                        {
                            if (tipoparametro == "W")
                            {
                                intercambiadortmp.caudalcorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                intercambiadortmp.presioncorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                intercambiadortmp.entalpiacorrentrada2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == intercambiadortmp.numcorrsalida1)
                        {
                            if (tipoparametro == "W")
                            {
                                intercambiadortmp.caudalcorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                intercambiadortmp.presioncorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                intercambiadortmp.entalpiacorrsalida1 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }

                        else if (numcorr == intercambiadortmp.numcorrsalida2)
                        {
                            if (tipoparametro == "W")
                            {
                                intercambiadortmp.caudalcorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "P")
                            {
                                intercambiadortmp.presioncorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }

                            else if (tipoparametro == "H")
                            {
                                intercambiadortmp.entalpiacorrsalida2 = puntero1.listaresultadoscorrientes[o, puntero1.setnumber].Value;
                            }
                        }
                    }
                    intercambiadortmp.Calcular();
                }

                //Analizamos el Tipo de Equipo de que se trata 
                if (puntero1.equipos11[indice].tipoequipo2 == 1)
                {
                    listBox3.Items.Add("Boundary Condition Equipment, Type 1." + "Equipment Number: " + condtemp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + condtemp.numcorrentrada);
                    listBox3.Items.Add("Output Stream Nº: " + condtemp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + condtemp.caudalcorrentrada);
                    listBox3.Items.Add("Output Flow: " + condtemp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + condtemp.presioncorrentrada);
                    listBox3.Items.Add("Output Pressure: " + condtemp.presioncorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + condtemp.entalpiacorrentrada);
                    listBox3.Items.Add("Output Enthalpy: " + condtemp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + condtemp.entropiaentrada);
                    listBox3.Items.Add("Output Entropy: " + condtemp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + condtemp.temperaturaentrada);
                    listBox3.Items.Add("Output Temperature: " + condtemp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + condtemp.volumenespecificoentrada);
                    listBox3.Items.Add("Output Volumen Específico: " + condtemp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + condtemp.tituloentrada);
                    listBox3.Items.Add("Output Título: " + condtemp.titulosalida);
                    listBox3.Items.Add("");
                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 2)
                {
                    listBox3.Items.Add("Divider Equipment, Type 2." + "Equipment Number: " + divisortemp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + divisortemp.numcorrentrada);
                    listBox3.Items.Add("Output1 Stream Nº: " + divisortemp.numcorrsalida1);
                    listBox3.Items.Add("Output2 Stream Nº: " + divisortemp.numcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + divisortemp.caudalcorrentrada);
                    listBox3.Items.Add("Output1 Flow: " + divisortemp.caudalcorrsalida1);
                    listBox3.Items.Add("Output2 Flow: " + divisortemp.caudalcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + divisortemp.presioncorrentrada);
                    listBox3.Items.Add("Output1 Pressure: " + divisortemp.presioncorrsalida1);
                    listBox3.Items.Add("Output2 Pressure: " + divisortemp.presioncorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + divisortemp.entalpiacorrentrada);
                    listBox3.Items.Add("Output1 Enthalpy: " + divisortemp.entalpiacorrsalida1);
                    listBox3.Items.Add("Output2 Enthalpy: " + divisortemp.entalpiacorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + divisortemp.entropiaentrada);
                    listBox3.Items.Add("Output1 Entropy: " + divisortemp.entropiasalida1);
                    listBox3.Items.Add("Output2 Entropy: " + divisortemp.entropiasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + divisortemp.temperaturaentrada);
                    listBox3.Items.Add("Output1 Temperature: " + divisortemp.temperaturasalida1);
                    listBox3.Items.Add("Output2 Temperature: " + divisortemp.temperaturasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + divisortemp.volumenespecificoentrada);
                    listBox3.Items.Add("Output1 Volumen Epecífico: " + divisortemp.volumenespecificosalida1);
                    listBox3.Items.Add("Output2 Volumen Específico: " + divisortemp.volumenespecificosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + divisortemp.tituloentrada);
                    listBox3.Items.Add("Output1 Título: " + divisortemp.titulosalida1);
                    listBox3.Items.Add("Output2 Título: " + divisortemp.titulosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Flow Input/Output1 ratio: " + divisortemp.porcentajesalida1 + "%");
                    listBox3.Items.Add("Flow Input/Output2 ratio: " + divisortemp.porcentajesalida2 + "%");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Flow Factor Output1: " + divisortemp.factorflujosalida1);
                    listBox3.Items.Add("Flow Factor Output2: " + divisortemp.factorflujosalida2);
                    listBox3.Items.Add("");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 3)
                {
                    listBox3.Items.Add("Pressure Loss Equipment, Type 3." + "Equipment Number: " + perdidacargatemp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + perdidacargatemp.numcorrentrada);
                    listBox3.Items.Add("Output Stream Nº: " + perdidacargatemp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + perdidacargatemp.caudalcorrentrada);
                    listBox3.Items.Add("Output Flow: " + perdidacargatemp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + perdidacargatemp.presioncorrentrada);
                    listBox3.Items.Add("Output Pressure: " + perdidacargatemp.presioncorrsalida);
                    listBox3.Items.Add("");
                    listBox3.Items.Add("Pressure Drop: " + perdidacargatemp.AP);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + perdidacargatemp.entalpiacorrentrada);
                    listBox3.Items.Add("Output Enthalpy: " + perdidacargatemp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + perdidacargatemp.entropiaentrada);
                    listBox3.Items.Add("Output Entropy: " + perdidacargatemp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + perdidacargatemp.temperaturaentrada);
                    listBox3.Items.Add("Output Temperature: " + perdidacargatemp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + perdidacargatemp.volumenespecificoentrada);
                    listBox3.Items.Add("Output Volumen Específico: " + perdidacargatemp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + perdidacargatemp.tituloentrada);
                    listBox3.Items.Add("Output Título: " + perdidacargatemp.titulosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 4)
                {
                    listBox3.Items.Add("Pump Equipment, Type 4." + "Equipment Number: " + bombatemp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + bombatemp.numcorrentrada);
                    listBox3.Items.Add("Output Stream Nº: " + bombatemp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + bombatemp.caudalcorrentrada);
                    listBox3.Items.Add("Output Flow: " + bombatemp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + bombatemp.presioncorrentrada);
                    listBox3.Items.Add("Output Pressure: " + bombatemp.presioncorrsalida);
                    listBox3.Items.Add("");
                    listBox3.Items.Add("Pressure Drop: " + bombatemp.AP);
                    listBox3.Items.Add("");
                    listBox3.Items.Add("TDH (m): " + bombatemp.TDH);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + bombatemp.entalpiacorrentrada);
                    listBox3.Items.Add("Output Enthalpy: " + bombatemp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + bombatemp.entropiaentrada);
                    listBox3.Items.Add("Output Entropy: " + bombatemp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + bombatemp.temperaturaentrada);
                    listBox3.Items.Add("Output Temperature: " + bombatemp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + bombatemp.volumenespecificoentrada);
                    listBox3.Items.Add("Output Volumen Específico: " + bombatemp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + bombatemp.tituloentrada);
                    listBox3.Items.Add("Output Título: " + bombatemp.titulosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Pump Power: " + bombatemp.potencia);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Pump Efficiency: " + bombatemp.eficiencia);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 5)
                {
                    listBox3.Items.Add("Mixer Equipment, Type 5." + "Equipment Number: " + mezcladortemp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Stream Nº: " + mezcladortemp.numcorrentrada1);
                    listBox3.Items.Add("Input2 Stream Nº: " + mezcladortemp.numcorrentrada2);
                    listBox3.Items.Add("Output Stream Nº: " + mezcladortemp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Flow: " + mezcladortemp.caudalcorrentrada1);
                    listBox3.Items.Add("Input2 Flow: " + mezcladortemp.caudalcorrentrada2);
                    listBox3.Items.Add("Output Flow: " + mezcladortemp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Pressure: " + mezcladortemp.presioncorrentrada1);
                    listBox3.Items.Add("Input2 Pressure: " + mezcladortemp.presioncorrentrada2);
                    listBox3.Items.Add("Output Pressure: " + mezcladortemp.presioncorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Enthalphy: " + mezcladortemp.entalpiacorrentrada1);
                    listBox3.Items.Add("Input2 Enthalpy: " + mezcladortemp.entalpiacorrentrada2);
                    listBox3.Items.Add("Output Enthalpy: " + mezcladortemp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Entropy: " + mezcladortemp.entropiaentrada1);
                    listBox3.Items.Add("Input2 Entropy: " + mezcladortemp.entropiaentrada2);
                    listBox3.Items.Add("Output Entropy: " + mezcladortemp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Temperature: " + mezcladortemp.temperaturaentrada1);
                    listBox3.Items.Add("Input2 Temperature: " + mezcladortemp.temperaturaentrada2);
                    listBox3.Items.Add("Output Temperature: " + mezcladortemp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Volumen Específico: " + mezcladortemp.volumenespecificoentrada1);
                    listBox3.Items.Add("Input2 Volumen Epecífico: " + mezcladortemp.volumenespecificoentrada2);
                    listBox3.Items.Add("Output Volumen Específico: " + mezcladortemp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Título: " + mezcladortemp.tituloentrada1);
                    listBox3.Items.Add("Input2 Título: " + mezcladortemp.tituloentrada2);
                    listBox3.Items.Add("Output Título: " + mezcladortemp.titulosalida);
                    listBox3.Items.Add("");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 6)
                {
                    listBox3.Items.Add("Reactor Equipment, Type 6." + "Equipment Number: " + reactortemp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + reactortemp.numcorrentrada);
                    listBox3.Items.Add("Output Stream Nº: " + reactortemp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + reactortemp.caudalcorrentrada);
                    listBox3.Items.Add("Output Flow: " + reactortemp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + reactortemp.presioncorrentrada);
                    listBox3.Items.Add("Output Pressure: " + reactortemp.presioncorrsalida);
                    listBox3.Items.Add("");
                    listBox3.Items.Add("Pressure Drop: " + reactortemp.AP);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + reactortemp.entalpiacorrentrada);
                    listBox3.Items.Add("Output Enthalpy: " + reactortemp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + reactortemp.entropiaentrada);
                    listBox3.Items.Add("Output Entropy: " + reactortemp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + reactortemp.temperaturaentrada);
                    listBox3.Items.Add("Output Temperature: " + reactortemp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + reactortemp.volumenespecificoentrada);
                    listBox3.Items.Add("Output Volumen Específico: " + reactortemp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + reactortemp.tituloentrada);
                    listBox3.Items.Add("Output Título: " + reactortemp.titulosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 7)
                {
                    listBox3.Items.Add("FeedWaterHeater Equipment, Type 7." + "Equipment Number: " + calentadortemp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Stream Nº: " + calentadortemp.numcorrentrada1);
                    listBox3.Items.Add("Input2 Stream Nº: " + calentadortemp.numcorrentrada2);
                    listBox3.Items.Add("Output1 Stream Nº: " + calentadortemp.numcorrsalida1);
                    listBox3.Items.Add("Output2 Stream Nº: " + calentadortemp.numcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Flow: " + calentadortemp.caudalcorrentrada1);
                    listBox3.Items.Add("Input2 Flow: " + calentadortemp.caudalcorrentrada2);
                    listBox3.Items.Add("Output1 Flow: " + calentadortemp.caudalcorrsalida1);
                    listBox3.Items.Add("Output2 Flow: " + calentadortemp.caudalcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Pressure: " + calentadortemp.presioncorrentrada1);
                    listBox3.Items.Add("Input2 Pressure: " + calentadortemp.presioncorrentrada2);
                    listBox3.Items.Add("Output1 Pressure: " + calentadortemp.presioncorrsalida1);
                    listBox3.Items.Add("Output2 Pressure: " + calentadortemp.presioncorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Enthalphy: " + calentadortemp.entalpiacorrentrada1);
                    listBox3.Items.Add("Input2 Enthalphy: " + calentadortemp.entalpiacorrentrada2);
                    listBox3.Items.Add("Output1 Enthalpy: " + calentadortemp.entalpiacorrsalida1);
                    listBox3.Items.Add("Output2 Enthalpy: " + calentadortemp.entalpiacorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Entropy: " + calentadortemp.entropiaentrada1);
                    listBox3.Items.Add("Input2 Entropy: " + calentadortemp.entropiaentrada2);
                    listBox3.Items.Add("Output1 Entropy: " + calentadortemp.entropiasalida1);
                    listBox3.Items.Add("Output2 Entropy: " + calentadortemp.entropiasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Temperature: " + calentadortemp.temperaturaentrada1);
                    listBox3.Items.Add("Input2 Temperature: " + calentadortemp.temperaturaentrada2);
                    listBox3.Items.Add("Output1 Temperature: " + calentadortemp.temperaturasalida1);
                    listBox3.Items.Add("Output2 Temperature: " + calentadortemp.temperaturasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Volumen Específico: " + calentadortemp.volumenespecificoentrada1);
                    listBox3.Items.Add("Input2 Volumen Específico: " + calentadortemp.volumenespecificoentrada2);
                    listBox3.Items.Add("Output1 Volumen Específico: " + calentadortemp.volumenespecificosalida1);
                    listBox3.Items.Add("Output2 Volumen Específico: " + calentadortemp.volumenespecificosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Título: " + calentadortemp.tituloentrada1);
                    listBox3.Items.Add("Input2 Título: " + calentadortemp.tituloentrada2);
                    listBox3.Items.Add("Output1 Título: " + calentadortemp.titulosalida1);
                    listBox3.Items.Add("Output2 Título: " + calentadortemp.titulosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("TTD: " + calentadortemp.TTD);
                    listBox3.Items.Add("DCA: " + calentadortemp.DCA);

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 8)
                {
                    listBox3.Items.Add("Main Condenser Equipment, Type 8." + "Equipment Number: " + condensadortemp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Stream Nº: " + condensadortemp.numcorrentrada1);
                    listBox3.Items.Add("Input2 Stream Nº: " + condensadortemp.numcorrentrada2);
                    listBox3.Items.Add("Output Stream Nº: " + condensadortemp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Flow: " + condensadortemp.caudalcorrentrada1);
                    listBox3.Items.Add("Input2 Flow: " + condensadortemp.caudalcorrentrada2);
                    listBox3.Items.Add("Output Flow: " + condensadortemp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Pressure: " + condensadortemp.presioncorrentrada1);
                    listBox3.Items.Add("Input2 Pressure: " + condensadortemp.presioncorrentrada2);
                    listBox3.Items.Add("Output Pressure: " + condensadortemp.presioncorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Enthalphy: " + condensadortemp.entalpiacorrentrada1);
                    listBox3.Items.Add("Input2 Enthalphy: " + condensadortemp.entalpiacorrentrada2);
                    listBox3.Items.Add("Output Enthalpy: " + condensadortemp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Entropy: " + condensadortemp.entropiaentrada1);
                    listBox3.Items.Add("Input2 Entropy: " + condensadortemp.entropiaentrada2);
                    listBox3.Items.Add("Output Entropy: " + condensadortemp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Temperature: " + condensadortemp.temperaturaentrada1);
                    listBox3.Items.Add("Input2 Temperature: " + condensadortemp.temperaturaentrada2);
                    listBox3.Items.Add("Output Temperature: " + condensadortemp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Volumen Específico: " + condensadortemp.volumenespecificoentrada1);
                    listBox3.Items.Add("Input2 Volumen Específico: " + condensadortemp.volumenespecificoentrada2);
                    listBox3.Items.Add("Output Volumen Específico: " + condensadortemp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Título: " + condensadortemp.tituloentrada1);
                    listBox3.Items.Add("Input2 Título: " + condensadortemp.tituloentrada2);
                    listBox3.Items.Add("Output Título: " + condensadortemp.titulosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 9)
                {
                    listBox3.Items.Add("Turbine9 Equipment, Type 9." + "Equipment Number: " + turbina9temp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + turbina9temp.numcorrentrada);
                    listBox3.Items.Add("Output Stream Nº: " + turbina9temp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + turbina9temp.caudalcorrentrada);
                    listBox3.Items.Add("Output Flow: " + turbina9temp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + turbina9temp.presioncorrentrada);
                    listBox3.Items.Add("Output Pressure: " + turbina9temp.presioncorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + turbina9temp.entalpiacorrentrada);
                    listBox3.Items.Add("Output Enthalpy: " + turbina9temp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + turbina9temp.entropiaentrada);
                    listBox3.Items.Add("Output Entropy: " + turbina9temp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + turbina9temp.temperaturaentrada);
                    listBox3.Items.Add("Output Temperature: " + turbina9temp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + turbina9temp.volumenespecificoentrada);
                    listBox3.Items.Add("Output Volumen Específico: " + turbina9temp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + turbina9temp.tituloentrada);
                    listBox3.Items.Add("Output Título: " + turbina9temp.titulosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Eficiencia: " + turbina9temp.eficiencia);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Potencia: " + turbina9temp.potencia);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Relación Presiones: " + turbina9temp.relacionpresiones);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Factor Flujo: " + turbina9temp.factorflujo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 10)
                {
                    listBox3.Items.Add("Turbine10 Equipment, Type 10." + "Equipment Number: " + turbina10temp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + turbina10temp.numcorrentrada);
                    listBox3.Items.Add("Output Stream Nº: " + turbina10temp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + turbina10temp.caudalcorrentrada);
                    listBox3.Items.Add("Output Flow: " + turbina10temp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + turbina10temp.presioncorrentrada);
                    listBox3.Items.Add("Output Pressure: " + turbina10temp.presioncorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + turbina10temp.entalpiacorrentrada);
                    listBox3.Items.Add("Output Enthalpy: " + turbina10temp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + turbina10temp.entropiaentrada);
                    listBox3.Items.Add("Output Entropy: " + turbina10temp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + turbina10temp.temperaturaentrada);
                    listBox3.Items.Add("Output Temperature: " + turbina10temp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + turbina10temp.volumenespecificoentrada);
                    listBox3.Items.Add("Output Volumen Específico: " + turbina10temp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + turbina10temp.tituloentrada);
                    listBox3.Items.Add("Output Título: " + turbina10temp.titulosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Eficiencia: " + turbina10temp.eficiencia);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Potencia: " + turbina10temp.potencia);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Relación Presiones: " + turbina10temp.relacionpresiones);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Factor Flujo: " + turbina10temp.factorflujo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 11)
                {
                    listBox3.Items.Add("Auxiliary Turbine Equipment, Type 11." + "Equipment Number: " + turbina11temp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + turbina11temp.numcorrentrada);
                    listBox3.Items.Add("Output Stream Nº: " + turbina11temp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + turbina11temp.caudalcorrentrada);
                    listBox3.Items.Add("Output Flow: " + turbina11temp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + turbina11temp.presioncorrentrada);
                    listBox3.Items.Add("Output Pressure: " + turbina11temp.presioncorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + turbina11temp.entalpiacorrentrada);
                    listBox3.Items.Add("Output Enthalpy: " + turbina11temp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + turbina11temp.entropiaentrada);
                    listBox3.Items.Add("Output Entropy: " + turbina11temp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + turbina11temp.temperaturaentrada);
                    listBox3.Items.Add("Output Temperature: " + turbina11temp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + turbina11temp.volumenespecificoentrada);
                    listBox3.Items.Add("Output Volumen Específico: " + turbina11temp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + turbina11temp.tituloentrada);
                    listBox3.Items.Add("Output Título: " + turbina11temp.titulosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Eficiencia: " + turbina11temp.eficiencia);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Potencia: " + turbina11temp.potencia);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Relación Presiones: " + turbina11temp.relacionpresiones);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Factor Flujo: " + turbina11temp.factorflujo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 13)
                {
                    listBox3.Items.Add("Moisture Separator Equipment, Type 13." + "Equipment Number: " + separadorhumedadtemp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + separadorhumedadtemp.numcorrentrada);
                    listBox3.Items.Add("Output1 Stream Nº: " + separadorhumedadtemp.numcorrsalida1);
                    listBox3.Items.Add("Output2 Stream Nº: " + separadorhumedadtemp.numcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + separadorhumedadtemp.caudalcorrentrada);
                    listBox3.Items.Add("Output1 Flow: " + separadorhumedadtemp.caudalcorrsalida1);
                    listBox3.Items.Add("Output2 Flow: " + separadorhumedadtemp.caudalcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + separadorhumedadtemp.presioncorrentrada);
                    listBox3.Items.Add("Output1 Pressure: " + separadorhumedadtemp.presioncorrsalida1);
                    listBox3.Items.Add("Output2 Pressure: " + separadorhumedadtemp.presioncorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + separadorhumedadtemp.entalpiacorrentrada);
                    listBox3.Items.Add("Output1 Enthalpy: " + separadorhumedadtemp.entalpiacorrsalida1);
                    listBox3.Items.Add("Output2 Enthalpy: " + separadorhumedadtemp.entalpiacorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + separadorhumedadtemp.entropiaentrada);
                    listBox3.Items.Add("Output1 Entropy: " + separadorhumedadtemp.entropiasalida1);
                    listBox3.Items.Add("Output2 Entropy: " + separadorhumedadtemp.entropiasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + separadorhumedadtemp.temperaturaentrada);
                    listBox3.Items.Add("Output1 Temperature: " + separadorhumedadtemp.temperaturasalida1);
                    listBox3.Items.Add("Output2 Temperature: " + separadorhumedadtemp.temperaturasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + separadorhumedadtemp.volumenespecificoentrada);
                    listBox3.Items.Add("Output1 Volumen Epecífico: " + separadorhumedadtemp.volumenespecificosalida1);
                    listBox3.Items.Add("Output2 Volumen Específico: " + separadorhumedadtemp.volumenespecificosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + separadorhumedadtemp.tituloentrada);
                    listBox3.Items.Add("Output1 Título: " + separadorhumedadtemp.titulosalida1);
                    listBox3.Items.Add("Output2 Título: " + separadorhumedadtemp.titulosalida2);
                    listBox3.Items.Add("");
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 14)
                {
                    listBox3.Items.Add("MSR Equipment, Type 14." + "Equipment Number: " + MSRtemp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Stream Nº: " + MSRtemp.numcorrentrada1);
                    listBox3.Items.Add("Input2 Stream Nº: " + MSRtemp.numcorrentrada2);
                    listBox3.Items.Add("Output1 Stream Nº: " + MSRtemp.numcorrsalida1);
                    listBox3.Items.Add("Output2 Stream Nº: " + MSRtemp.numcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Flow: " + MSRtemp.caudalcorrentrada1);
                    listBox3.Items.Add("Input2 Flow: " + MSRtemp.caudalcorrentrada2);
                    listBox3.Items.Add("Output1 Flow: " + MSRtemp.caudalcorrsalida1);
                    listBox3.Items.Add("Output2 Flow: " + MSRtemp.caudalcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Pressure: " + MSRtemp.presioncorrentrada1);
                    listBox3.Items.Add("Input2 Pressure: " + MSRtemp.presioncorrentrada2);
                    listBox3.Items.Add("Output1 Pressure: " + MSRtemp.presioncorrsalida1);
                    listBox3.Items.Add("Output2 Pressure: " + MSRtemp.presioncorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Enthalphy: " + MSRtemp.entalpiacorrentrada1);
                    listBox3.Items.Add("Input2 Enthalphy: " + MSRtemp.entalpiacorrentrada2);
                    listBox3.Items.Add("Output1 Enthalpy: " + MSRtemp.entalpiacorrsalida1);
                    listBox3.Items.Add("Output2 Enthalpy: " + MSRtemp.entalpiacorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Entropy: " + MSRtemp.entropiaentrada1);
                    listBox3.Items.Add("Input2 Entropy: " + MSRtemp.entropiaentrada2);
                    listBox3.Items.Add("Output1 Entropy: " + MSRtemp.entropiasalida1);
                    listBox3.Items.Add("Output2 Entropy: " + MSRtemp.entropiasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Temperature: " + MSRtemp.temperaturaentrada1);
                    listBox3.Items.Add("Input2 Temperature: " + MSRtemp.temperaturaentrada2);
                    listBox3.Items.Add("Output1 Temperature: " + MSRtemp.temperaturasalida1);
                    listBox3.Items.Add("Output2 Temperature: " + MSRtemp.temperaturasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Volumen Específico: " + MSRtemp.volumenespecificoentrada1);
                    listBox3.Items.Add("Input2 Volumen Específico: " + MSRtemp.volumenespecificoentrada2);
                    listBox3.Items.Add("Output1 Volumen Específico: " + MSRtemp.volumenespecificosalida1);
                    listBox3.Items.Add("Output2 Volumen Específico: " + MSRtemp.volumenespecificosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Título: " + MSRtemp.tituloentrada1);
                    listBox3.Items.Add("Input2 Título: " + MSRtemp.tituloentrada2);
                    listBox3.Items.Add("Output1 Título: " + MSRtemp.titulosalida1);
                    listBox3.Items.Add("Output2 Título: " + MSRtemp.titulosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("TTD: " + MSRtemp.TTD);

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 15)
                {
                    listBox3.Items.Add("Condensator Equipment, Type 15." + "Equipment Number: " + condensador15tmp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Stream Nº: " + condensador15tmp.numcorrentrada1);
                    listBox3.Items.Add("Input2 Stream Nº: " + condensador15tmp.numcorrentrada2);
                    listBox3.Items.Add("Output1 Stream Nº: " + condensador15tmp.numcorrsalida1);
                    listBox3.Items.Add("Output2 Stream Nº: " + condensador15tmp.numcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Flow: " + condensador15tmp.caudalcorrentrada1);
                    listBox3.Items.Add("Input2 Flow: " + condensador15tmp.caudalcorrentrada2);
                    listBox3.Items.Add("Output1 Flow: " + condensador15tmp.caudalcorrsalida1);
                    listBox3.Items.Add("Output2 Flow: " + condensador15tmp.caudalcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Pressure: " + condensador15tmp.presioncorrentrada1);
                    listBox3.Items.Add("Input2 Pressure: " + condensador15tmp.presioncorrentrada2);
                    listBox3.Items.Add("Output1 Pressure: " + condensador15tmp.presioncorrsalida1);
                    listBox3.Items.Add("Output2 Pressure: " + condensador15tmp.presioncorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Enthalphy: " + condensador15tmp.entalpiacorrentrada1);
                    listBox3.Items.Add("Input2 Enthalphy: " + condensador15tmp.entalpiacorrentrada2);
                    listBox3.Items.Add("Output1 Enthalpy: " + condensador15tmp.entalpiacorrsalida1);
                    listBox3.Items.Add("Output2 Enthalpy: " + condensador15tmp.entalpiacorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Entropy: " + condensador15tmp.entropiaentrada1);
                    listBox3.Items.Add("Input2 Entropy: " + condensador15tmp.entropiaentrada2);
                    listBox3.Items.Add("Output1 Entropy: " + condensador15tmp.entropiasalida1);
                    listBox3.Items.Add("Output2 Entropy: " + condensador15tmp.entropiasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Temperature: " + condensador15tmp.temperaturaentrada1);
                    listBox3.Items.Add("Input2 Temperature: " + condensador15tmp.temperaturaentrada2);
                    listBox3.Items.Add("Output1 Temperature: " + condensador15tmp.temperaturasalida1);
                    listBox3.Items.Add("Output2 Temperature: " + condensador15tmp.temperaturasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Volumen Específico: " + condensador15tmp.volumenespecificoentrada1);
                    listBox3.Items.Add("Input2 Volumen Específico: " + condensador15tmp.volumenespecificoentrada2);
                    listBox3.Items.Add("Output1 Volumen Específico: " + condensador15tmp.volumenespecificosalida1);
                    listBox3.Items.Add("Output2 Volumen Específico: " + condensador15tmp.volumenespecificosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Título: " + condensador15tmp.tituloentrada1);
                    listBox3.Items.Add("Input2 Título: " + condensador15tmp.tituloentrada2);
                    listBox3.Items.Add("Output1 Título: " + condensador15tmp.titulosalida1);
                    listBox3.Items.Add("Output2 Título: " + condensador15tmp.titulosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 16)
                {
                    listBox3.Items.Add("Drainage Cooling Equipment, Type 16." + "Equipment Number: " + enfriadortmp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Stream Nº: " + enfriadortmp.numcorrentrada1);
                    listBox3.Items.Add("Input2 Stream Nº: " + enfriadortmp.numcorrentrada2);
                    listBox3.Items.Add("Output1 Stream Nº: " + enfriadortmp.numcorrsalida1);
                    listBox3.Items.Add("Output2 Stream Nº: " + enfriadortmp.numcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Flow: " + enfriadortmp.caudalcorrentrada1);
                    listBox3.Items.Add("Input2 Flow: " + enfriadortmp.caudalcorrentrada2);
                    listBox3.Items.Add("Output1 Flow: " + enfriadortmp.caudalcorrsalida1);
                    listBox3.Items.Add("Output2 Flow: " + enfriadortmp.caudalcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Pressure: " + enfriadortmp.presioncorrentrada1);
                    listBox3.Items.Add("Input2 Pressure: " + enfriadortmp.presioncorrentrada2);
                    listBox3.Items.Add("Output1 Pressure: " + enfriadortmp.presioncorrsalida1);
                    listBox3.Items.Add("Output2 Pressure: " + enfriadortmp.presioncorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Enthalphy: " + enfriadortmp.entalpiacorrentrada1);
                    listBox3.Items.Add("Input2 Enthalphy: " + enfriadortmp.entalpiacorrentrada2);
                    listBox3.Items.Add("Output1 Enthalpy: " + enfriadortmp.entalpiacorrsalida1);
                    listBox3.Items.Add("Output2 Enthalpy: " + enfriadortmp.entalpiacorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Entropy: " + enfriadortmp.entropiaentrada1);
                    listBox3.Items.Add("Input2 Entropy: " + enfriadortmp.entropiaentrada2);
                    listBox3.Items.Add("Output1 Entropy: " + enfriadortmp.entropiasalida1);
                    listBox3.Items.Add("Output2 Entropy: " + enfriadortmp.entropiasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Temperature: " + enfriadortmp.temperaturaentrada1);
                    listBox3.Items.Add("Input2 Temperature: " + enfriadortmp.temperaturaentrada2);
                    listBox3.Items.Add("Output1 Temperature: " + enfriadortmp.temperaturasalida1);
                    listBox3.Items.Add("Output2 Temperature: " + enfriadortmp.temperaturasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Volumen Específico: " + enfriadortmp.volumenespecificoentrada1);
                    listBox3.Items.Add("Input2 Volumen Específico: " + enfriadortmp.volumenespecificoentrada2);
                    listBox3.Items.Add("Output1 Volumen Específico: " + enfriadortmp.volumenespecificosalida1);
                    listBox3.Items.Add("Output2 Volumen Específico: " + enfriadortmp.volumenespecificosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Título: " + enfriadortmp.tituloentrada1);
                    listBox3.Items.Add("Input2 Título: " + enfriadortmp.tituloentrada2);
                    listBox3.Items.Add("Output1 Título: " + enfriadortmp.titulosalida1);
                    listBox3.Items.Add("Output2 Título: " + enfriadortmp.titulosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("TTD: " + enfriadortmp.TTD);
                    listBox3.Items.Add("DCA: " + enfriadortmp.DCA);

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 17)
                {
                    listBox3.Items.Add("DesuperHeater Equipment, Type 17." + "Equipment Number: " + atemperadortmp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Stream Nº: " + atemperadortmp.numcorrentrada1);
                    listBox3.Items.Add("Input2 Stream Nº: " + atemperadortmp.numcorrentrada2);
                    listBox3.Items.Add("Output Stream Nº: " + atemperadortmp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Flow: " + atemperadortmp.caudalcorrentrada1);
                    listBox3.Items.Add("Input2 Flow: " + atemperadortmp.caudalcorrentrada2);
                    listBox3.Items.Add("Output Flow: " + atemperadortmp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Pressure: " + atemperadortmp.presioncorrentrada1);
                    listBox3.Items.Add("Input2 Pressure: " + atemperadortmp.presioncorrentrada2);
                    listBox3.Items.Add("Output Pressure: " + atemperadortmp.presioncorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Enthalphy: " + atemperadortmp.entalpiacorrentrada1);
                    listBox3.Items.Add("Input2 Enthalpy: " + atemperadortmp.entalpiacorrentrada2);
                    listBox3.Items.Add("Output Enthalpy: " + atemperadortmp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Entropy: " + atemperadortmp.entropiaentrada1);
                    listBox3.Items.Add("Input2 Entropy: " + atemperadortmp.entropiaentrada2);
                    listBox3.Items.Add("Output Entropy: " + atemperadortmp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Temperature: " + atemperadortmp.temperaturaentrada1);
                    listBox3.Items.Add("Input2 Temperature: " + atemperadortmp.temperaturaentrada2);
                    listBox3.Items.Add("Output Temperature: " + atemperadortmp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Volumen Específico: " + atemperadortmp.volumenespecificoentrada1);
                    listBox3.Items.Add("Input2 Volumen Epecífico: " + atemperadortmp.volumenespecificoentrada2);
                    listBox3.Items.Add("Output Volumen Específico: " + atemperadortmp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Título: " + atemperadortmp.tituloentrada1);
                    listBox3.Items.Add("Input2 Título: " + atemperadortmp.tituloentrada2);
                    listBox3.Items.Add("Output Título: " + atemperadortmp.titulosalida);
                    listBox3.Items.Add("");
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 18)
                {
                    listBox3.Items.Add("Deaireator Equipment, Type 18." + "Equipment Number: " + desaireadortmp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Stream Nº: " + desaireadortmp.numcorrentrada1);
                    listBox3.Items.Add("Input2 Stream Nº: " + desaireadortmp.numcorrentrada2);
                    listBox3.Items.Add("Output1 Stream Nº: " + desaireadortmp.numcorrsalida1);
                    listBox3.Items.Add("Output2 Stream Nº: " + desaireadortmp.numcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Flow: " + desaireadortmp.caudalcorrentrada1);
                    listBox3.Items.Add("Input2 Flow: " + desaireadortmp.caudalcorrentrada2);
                    listBox3.Items.Add("Output1 Flow: " + desaireadortmp.caudalcorrsalida1);
                    listBox3.Items.Add("Output2 Flow: " + desaireadortmp.caudalcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Pressure: " + desaireadortmp.presioncorrentrada1);
                    listBox3.Items.Add("Input2 Pressure: " + desaireadortmp.presioncorrentrada2);
                    listBox3.Items.Add("Output1 Pressure: " + desaireadortmp.presioncorrsalida1);
                    listBox3.Items.Add("Output2 Pressure: " + desaireadortmp.presioncorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Enthalphy: " + desaireadortmp.entalpiacorrentrada1);
                    listBox3.Items.Add("Input2 Enthalphy: " + desaireadortmp.entalpiacorrentrada2);
                    listBox3.Items.Add("Output1 Enthalpy: " + desaireadortmp.entalpiacorrsalida1);
                    listBox3.Items.Add("Output2 Enthalpy: " + desaireadortmp.entalpiacorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Entropy: " + desaireadortmp.entropiaentrada1);
                    listBox3.Items.Add("Input2 Entropy: " + desaireadortmp.entropiaentrada2);
                    listBox3.Items.Add("Output1 Entropy: " + desaireadortmp.entropiasalida1);
                    listBox3.Items.Add("Output2 Entropy: " + desaireadortmp.entropiasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Temperature: " + desaireadortmp.temperaturaentrada1);
                    listBox3.Items.Add("Input2 Temperature: " + desaireadortmp.temperaturaentrada2);
                    listBox3.Items.Add("Output1 Temperature: " + desaireadortmp.temperaturasalida1);
                    listBox3.Items.Add("Output2 Temperature: " + desaireadortmp.temperaturasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Volumen Específico: " + desaireadortmp.volumenespecificoentrada1);
                    listBox3.Items.Add("Input2 Volumen Específico: " + desaireadortmp.volumenespecificoentrada2);
                    listBox3.Items.Add("Output1 Volumen Específico: " + desaireadortmp.volumenespecificosalida1);
                    listBox3.Items.Add("Output2 Volumen Específico: " + desaireadortmp.volumenespecificosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Título: " + desaireadortmp.tituloentrada1);
                    listBox3.Items.Add("Input2 Título: " + desaireadortmp.tituloentrada2);
                    listBox3.Items.Add("Output1 Título: " + desaireadortmp.titulosalida1);
                    listBox3.Items.Add("Output2 Título: " + desaireadortmp.titulosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("TTD: " + desaireadortmp.TTD);
                    listBox3.Items.Add("DCA: " + desaireadortmp.DCA);

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 19)
                {
                    listBox3.Items.Add("Valve Equipment, Type 19." + "Equipment Number: " + valvulatmp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + valvulatmp.numcorrentrada);
                    listBox3.Items.Add("Output Stream Nº: " + valvulatmp.numcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + valvulatmp.caudalcorrentrada);
                    listBox3.Items.Add("Output Flow: " + valvulatmp.caudalcorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + valvulatmp.presioncorrentrada);
                    listBox3.Items.Add("Output Pressure: " + valvulatmp.presioncorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + valvulatmp.entalpiacorrentrada);
                    listBox3.Items.Add("Output Enthalpy: " + valvulatmp.entalpiacorrsalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + valvulatmp.entropiaentrada);
                    listBox3.Items.Add("Output Entropy: " + valvulatmp.entropiasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + valvulatmp.temperaturaentrada);
                    listBox3.Items.Add("Output Temperature: " + valvulatmp.temperaturasalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + valvulatmp.volumenespecificoentrada);
                    listBox3.Items.Add("Output Volumen Específico: " + valvulatmp.volumenespecificosalida);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + valvulatmp.tituloentrada);
                    listBox3.Items.Add("Output Título: " + valvulatmp.titulosalida);
                    listBox3.Items.Add("");
                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 20)
                {
                    listBox3.Items.Add("Fixed Entalpht Divider Equipment, Type 20." + "Equipment Number: " + divientalpiatmp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + divientalpiatmp.numcorrentrada);
                    listBox3.Items.Add("Output1 Stream Nº: " + divientalpiatmp.numcorrsalida1);
                    listBox3.Items.Add("Output2 Stream Nº: " + divientalpiatmp.numcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + divientalpiatmp.caudalcorrentrada);
                    listBox3.Items.Add("Output1 Flow: " + divientalpiatmp.caudalcorrsalida1);
                    listBox3.Items.Add("Output2 Flow: " + divientalpiatmp.caudalcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + divientalpiatmp.presioncorrentrada);
                    listBox3.Items.Add("Output1 Pressure: " + divientalpiatmp.presioncorrsalida1);
                    listBox3.Items.Add("Output2 Pressure: " + divientalpiatmp.presioncorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + divientalpiatmp.entalpiacorrentrada);
                    listBox3.Items.Add("Output1 Enthalpy: " + divientalpiatmp.entalpiacorrsalida1);
                    listBox3.Items.Add("Output2 Enthalpy: " + divientalpiatmp.entalpiacorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + divientalpiatmp.entropiaentrada);
                    listBox3.Items.Add("Output1 Entropy: " + divientalpiatmp.entropiasalida1);
                    listBox3.Items.Add("Output2 Entropy: " + divientalpiatmp.entropiasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + divientalpiatmp.temperaturaentrada);
                    listBox3.Items.Add("Output1 Temperature: " + divientalpiatmp.temperaturasalida1);
                    listBox3.Items.Add("Output2 Temperature: " + divientalpiatmp.temperaturasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + divientalpiatmp.volumenespecificoentrada);
                    listBox3.Items.Add("Output1 Volumen Epecífico: " + divientalpiatmp.volumenespecificosalida1);
                    listBox3.Items.Add("Output2 Volumen Específico: " + divientalpiatmp.volumenespecificosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + divientalpiatmp.tituloentrada);
                    listBox3.Items.Add("Output1 Título: " + divientalpiatmp.titulosalida1);
                    listBox3.Items.Add("Output2 Título: " + divientalpiatmp.titulosalida2);
                    listBox3.Items.Add("");
                }

                //Analizamos el Tipo de Equipo de que se trata 
                else if (puntero1.equipos11[indice].tipoequipo2 == 21)
                {
                    listBox3.Items.Add("Flash Tank Equipment, Type 21." + "Equipment Number: " + tanquevapotmp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Stream Nº: " + tanquevapotmp.numcorrentrada);
                    listBox3.Items.Add("Output1 Stream Nº: " + tanquevapotmp.numcorrsalida1);
                    listBox3.Items.Add("Output2 Stream Nº: " + tanquevapotmp.numcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Flow: " + tanquevapotmp.caudalcorrentrada);
                    listBox3.Items.Add("Output1 Flow: " + tanquevapotmp.caudalcorrsalida1);
                    listBox3.Items.Add("Output2 Flow: " + tanquevapotmp.caudalcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Pressure: " + tanquevapotmp.presioncorrentrada);
                    listBox3.Items.Add("Output1 Pressure: " + tanquevapotmp.presioncorrsalida1);
                    listBox3.Items.Add("Output2 Pressure: " + tanquevapotmp.presioncorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Enthalphy: " + tanquevapotmp.entalpiacorrentrada);
                    listBox3.Items.Add("Output1 Enthalpy: " + tanquevapotmp.entalpiacorrsalida1);
                    listBox3.Items.Add("Output2 Enthalpy: " + tanquevapotmp.entalpiacorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Entropy: " + tanquevapotmp.entropiaentrada);
                    listBox3.Items.Add("Output1 Entropy: " + tanquevapotmp.entropiasalida1);
                    listBox3.Items.Add("Output2 Entropy: " + tanquevapotmp.entropiasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Temperature: " + tanquevapotmp.temperaturaentrada);
                    listBox3.Items.Add("Output1 Temperature: " + tanquevapotmp.temperaturasalida1);
                    listBox3.Items.Add("Output2 Temperature: " + tanquevapotmp.temperaturasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Volumen Específico: " + tanquevapotmp.volumenespecificoentrada);
                    listBox3.Items.Add("Output1 Volumen Epecífico: " + tanquevapotmp.volumenespecificosalida1);
                    listBox3.Items.Add("Output2 Volumen Específico: " + tanquevapotmp.volumenespecificosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input Título: " + tanquevapotmp.tituloentrada);
                    listBox3.Items.Add("Output1 Título: " + tanquevapotmp.titulosalida1);
                    listBox3.Items.Add("Output2 Título: " + tanquevapotmp.titulosalida2);
                    listBox3.Items.Add("");
                }

                else if (puntero1.equipos11[indice].tipoequipo2 == 22)
                {
                    listBox3.Items.Add("Heat Exchanger Equipment, Type 22." + "Equipment Number: " + intercambiadortmp.numequipo);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Stream Nº: " + intercambiadortmp.numcorrentrada1);
                    listBox3.Items.Add("Input2 Stream Nº: " + intercambiadortmp.numcorrentrada2);
                    listBox3.Items.Add("Output1 Stream Nº: " + intercambiadortmp.numcorrsalida1);
                    listBox3.Items.Add("Output2 Stream Nº: " + intercambiadortmp.numcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Unidades: Flow - W(Kgr/sg),Pressure - P(Bar),Entalphy - H(Kj/Kgr), Entropy - S(Kj/KgrºC), Temperature - (ºC)");
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Flow: " + intercambiadortmp.caudalcorrentrada1);
                    listBox3.Items.Add("Input2 Flow: " + intercambiadortmp.caudalcorrentrada2);
                    listBox3.Items.Add("Output1 Flow: " + intercambiadortmp.caudalcorrsalida1);
                    listBox3.Items.Add("Output2 Flow: " + intercambiadortmp.caudalcorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Pressure: " + intercambiadortmp.presioncorrentrada1);
                    listBox3.Items.Add("Input2 Pressure: " + intercambiadortmp.presioncorrentrada2);
                    listBox3.Items.Add("Output1 Pressure: " + intercambiadortmp.presioncorrsalida1);
                    listBox3.Items.Add("Output2 Pressure: " + intercambiadortmp.presioncorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Enthalphy: " + intercambiadortmp.entalpiacorrentrada1);
                    listBox3.Items.Add("Input2 Enthalphy: " + intercambiadortmp.entalpiacorrentrada2);
                    listBox3.Items.Add("Output1 Enthalpy: " + intercambiadortmp.entalpiacorrsalida1);
                    listBox3.Items.Add("Output2 Enthalpy: " + intercambiadortmp.entalpiacorrsalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Entropy: " + intercambiadortmp.entropiaentrada1);
                    listBox3.Items.Add("Input2 Entropy: " + intercambiadortmp.entropiaentrada2);
                    listBox3.Items.Add("Output1 Entropy: " + intercambiadortmp.entropiasalida1);
                    listBox3.Items.Add("Output2 Entropy: " + intercambiadortmp.entropiasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Temperature: " + intercambiadortmp.temperaturaentrada1);
                    listBox3.Items.Add("Input2 Temperature: " + intercambiadortmp.temperaturaentrada2);
                    listBox3.Items.Add("Output1 Temperature: " + intercambiadortmp.temperaturasalida1);
                    listBox3.Items.Add("Output2 Temperature: " + intercambiadortmp.temperaturasalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Volumen Específico: " + intercambiadortmp.volumenespecificoentrada1);
                    listBox3.Items.Add("Input2 Volumen Específico: " + intercambiadortmp.volumenespecificoentrada2);
                    listBox3.Items.Add("Output1 Volumen Específico: " + intercambiadortmp.volumenespecificosalida1);
                    listBox3.Items.Add("Output2 Volumen Específico: " + intercambiadortmp.volumenespecificosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("Input1 Título: " + intercambiadortmp.tituloentrada1);
                    listBox3.Items.Add("Input2 Título: " + intercambiadortmp.tituloentrada2);
                    listBox3.Items.Add("Output1 Título: " + intercambiadortmp.titulosalida1);
                    listBox3.Items.Add("Output2 Título: " + intercambiadortmp.titulosalida2);
                    listBox3.Items.Add("");

                    listBox3.Items.Add("TTD: " + intercambiadortmp.TTD);
                    listBox3.Items.Add("DCA: " + intercambiadortmp.DCA);

                    listBox3.Items.Add("");
                    listBox3.Items.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                //CONTINUAR CON EL RESTO DE EQUIPOS MEDIANTE OTROS ELSE IF 
            }
        }

        private void treeView2_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            listBox6.Items.Clear();

            TreeNode clickedNode = e.Node;
            Int16 numequipo = 0;

            if (clickedNode.Text == "EQUIPOS")
            {
                //MessageBox.Show(clickedNode.Text);
            }

            else
            {
                int indice = 0;
                double numeroequipotemporal = 0;

                string temp = clickedNode.Text;
                int longitud = clickedNode.Text.Length;

                string numequipotemp = temp.Substring(17, longitud - 17);

                numeroequipotemporal = Convert.ToInt16(numequipotemp);

                for (int j = 0; j < puntero1.equipos11.Count; j++)
                {
                    if (puntero1.equipos11[j].numequipo2 == numeroequipotemporal)
                    {
                        indice = j;
                    }
                }

                //MessageBox.Show(numequipo);

                puntero1.equipos11[indice].Number = puntero1.equipos11[indice].numequipo2;
                puntero1.equipos11[indice].Type = puntero1.equipos11[indice].tipoequipo2;

                puntero1.equipos11[indice].N1 = puntero1.equipos11[indice].aN1;
                puntero1.equipos11[indice].N2 = puntero1.equipos11[indice].aN2;
                puntero1.equipos11[indice].N3 = puntero1.equipos11[indice].aN3;
                puntero1.equipos11[indice].N4 = puntero1.equipos11[indice].aN4;

                puntero1.equipos11[indice].D1 = puntero1.equipos11[indice].aD1;
                puntero1.equipos11[indice].D2 = puntero1.equipos11[indice].aD2;
                puntero1.equipos11[indice].D3 = puntero1.equipos11[indice].aD3;
                puntero1.equipos11[indice].D4 = puntero1.equipos11[indice].aD4;
                puntero1.equipos11[indice].D5 = puntero1.equipos11[indice].aD5;
                puntero1.equipos11[indice].D6 = puntero1.equipos11[indice].aD6;
                puntero1.equipos11[indice].D7 = puntero1.equipos11[indice].aD7;
                puntero1.equipos11[indice].D8 = puntero1.equipos11[indice].aD8;
                puntero1.equipos11[indice].D9 = puntero1.equipos11[indice].aD9;

                //System.Reflection.MemberInfo property = typeof(Equipos).GetProperty("D1");
                //var attribute1 = property.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                //MessageBox.Show(Convert.ToString(attribute1));          
                //propertyGrid1.SelectedObject = equipos11[indice];
                if (puntero1.unidades == 2)
                {
                    listBox6.Items.Add("Sistema Internacional, Caudal Kg/Sg, Presión Bar, Entalpía Kj/Kgr");
                    listBox6.Items.Add("");
                    listBox6.Items.Add("Equipo Nº: " + puntero1.equipos11[indice].Number);
                    listBox6.Items.Add("Tipo de Equipo: " + puntero1.equipos11[indice].Type);
                    listBox6.Items.Add("");

                    if (puntero1.equipos11[indice].Type == 1)
                    {
                        listBox6.Items.Add("Caudal Entrada(D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * 0.4536));
                        listBox6.Items.Add("Presión Entrada(D2): " + Convert.ToString(puntero1.equipos11[indice].D2 * (6.8947572 / 100)));
                        listBox6.Items.Add("Entalpía Entrada(D3): " + Convert.ToString(puntero1.equipos11[indice].D3 * 2.326009));
                        listBox6.Items.Add("No plante ecuación continuidad(D5): " + puntero1.equipos11[indice].D5);

                        if (puntero1.equipos11[indice].D6 > 0)
                        {
                            listBox6.Items.Add("Presión(positivo), Temperatura(negativo) (D6): " + Convert.ToString(puntero1.equipos11[indice].D6 * (6.8947572 / 100)));
                        }

                        else if (puntero1.equipos11[indice].D6 < 0)
                        {
                            listBox6.Items.Add("Presión(positivo), Temperatura(negativo) (D6): " + Convert.ToString(puntero1.equipos11[indice].D6 - 273.0));
                        }

                        listBox6.Items.Add("Título (D7): " + puntero1.equipos11[indice].D7);
                    }

                    else if (puntero1.equipos11[indice].Type == 2)
                    {
                        listBox6.Items.Add("Caudal Entrada(D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * 0.4536));
                        listBox6.Items.Add("Fraccion de caudal N1 que sale por N3 (D2): " + puntero1.equipos11[indice].D2);
                        listBox6.Items.Add("Factor de Flujo(D3): " + puntero1.equipos11[indice].D3);
                    }

                    else if (puntero1.equipos11[indice].Type == 3)
                    {
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 / 2.984193609));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 6.578911309));
                        listBox6.Items.Add("Factor Porcentaje Pérdida Carga (D4): " + puntero1.equipos11[indice].D4);
                        listBox6.Items.Add("Factor fxL/D (D5): " + puntero1.equipos11[indice].D5);
                        listBox6.Items.Add("Diámetro Interno (D6): " + Convert.ToString((puntero1.equipos11[indice].D6 * 1000) / 3.28083));
                        listBox6.Items.Add("Diferencia de Cotas (D7): " + Convert.ToString(puntero1.equipos11[indice].D7 / 3.28083));
                        listBox6.Items.Add("Número Tuberías en Paralelo (D8): " + puntero1.equipos11[indice].D8);
                        listBox6.Items.Add("Válvula Antirretorno - 1 Si - 0 No (D9): " + puntero1.equipos11[indice].D9);
                    }

                    else if (puntero1.equipos11[indice].Type == 4)
                    {
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.984193609));
                        listBox6.Items.Add("Rendimiento de la Bomba (D4): " + puntero1.equipos11[indice].D4);
                        listBox6.Items.Add("Presión de Desacarga en N2 (D5): " + Convert.ToString(puntero1.equipos11[indice].D5 * (6.8947572 / 100)));
                        listBox6.Items.Add("Número Bombas en Paralelo (D7): " + puntero1.equipos11[indice].D7);
                        listBox6.Items.Add("Tabla TDH = f(caudal) (D8): " + puntero1.equipos11[indice].D8);
                        listBox6.Items.Add("Cálculo TDH D9=1, cálculo presión descarga D9=0 (D9): " + puntero1.equipos11[indice].D9);
                    }

                    else if (puntero1.equipos11[indice].Type == 5)
                    {
                        listBox6.Items.Add("Definición de Presión de Salida en N3 (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Ecuación de equilibrio de presiones (D9): " + puntero1.equipos11[indice].D9);
                    }

                    else if (puntero1.equipos11[indice].Type == 6)
                    {
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.984193609));
                        if (puntero1.equipos11[indice].D4 < 0)
                        {
                            //En este caso fijamos la presión P2=D4, por tanto, tenemos que realizar la conversión de Bar a PSI
                            listBox6.Items.Add("Factor Porcentaje Pérdida Carga (D4): " + Convert.ToString(puntero1.equipos11[indice].D4 * (6.8947572 / 100)));
                        }

                        if (puntero1.equipos11[indice].D5 > 0)
                        {
                            //Entalpia Kj/Kgr a Btu/Lb
                            listBox6.Items.Add("Fija la Temperatura o Entalpía en corriente de Salida (D5): " + Convert.ToString(puntero1.equipos11[indice].D5 * 2.326009));
                        }
                        else if (puntero1.equipos11[indice].D5 < 0)
                        {
                            //Convertir los grados ºC en ºF
                            listBox6.Items.Add("Fija la Temperatura o Entalpía en corriente de Salida (D5): " + Convert.ToString((puntero1.equipos11[indice].D5 - 32) * (5 / 9)));
                        }
                        listBox6.Items.Add("Calor aportado al Equipo (D6): " + Convert.ToString(puntero1.equipos11[indice].D6 / 0.9486608));
                        listBox6.Items.Add("Rendimiento de Intercambio Térmico (D7): " + puntero1.equipos11[indice].D7);
                        listBox6.Items.Add("Contabilización del Calor aportardo por el Equipo D9=1 Si, D9=0 No, (D9): " + puntero1.equipos11[indice].D9);
                    }

                    else if (puntero1.equipos11[indice].Type == 7)
                    {
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.984193609));
                        if (puntero1.equipos11[indice].D4 > 500)
                        {
                            listBox6.Items.Add("DCA (D4): " + puntero1.equipos11[indice].D4);
                        }
                        else if (puntero1.equipos11[indice].D4 < 500)
                        {
                            listBox6.Items.Add("DCA (D4): " + Convert.ToString(puntero1.equipos11[indice].D4 * (5.0 / 9.0)));
                        }

                        //Convertir ºC a ºF
                        if (puntero1.equipos11[indice].D5 > 500)
                        {
                            listBox6.Items.Add("TTD (D5): " + puntero1.equipos11[indice].D5);
                        }
                        else if (puntero1.equipos11[indice].D5 < 500)
                        {
                            listBox6.Items.Add("TTD (D5): " + Convert.ToString(puntero1.equipos11[indice].D5 * (5.0 / 9.0)));
                        }

                        listBox6.Items.Add("Rendimiento Térmico (D6): " + puntero1.equipos11[indice].D6);
                        listBox6.Items.Add("Igualdad e Presiones con Corriente de Cascada D7=1 Si, D7=0 No, (D7): " + puntero1.equipos11[indice].D7);
                        listBox6.Items.Add("Número de Calentadores en Paralelo (D8): " + puntero1.equipos11[indice].D8);
                        listBox6.Items.Add("Número de Corriente de Cascada N5, (D9): " + puntero1.equipos11[indice].D9);
                    }

                    else if (puntero1.equipos11[indice].Type == 8)
                    {
                        listBox6.Items.Add("Presión Vacion ó Coeficiente conversión m3/h a Tm/h + temperatura entrada agua circulación (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Diámetro Exterior Tubos (D2): " + puntero1.equipos11[indice].D2);
                        listBox6.Items.Add("Galga o Espesor (D3): " + puntero1.equipos11[indice].D3);
                        listBox6.Items.Add("Factor de Material de Tubos (D4): " + puntero1.equipos11[indice].D4);
                        listBox6.Items.Add("Factor de Limpieza (D5): " + puntero1.equipos11[indice].D5);
                        listBox6.Items.Add("Longitud efectiva de Tubos (D6): " + puntero1.equipos11[indice].D6);
                        listBox6.Items.Add("Caudal de Agua de Circulación, (D7): " + puntero1.equipos11[indice].D7);
                        listBox6.Items.Add("Superficie Efectiva Total (D8): " + puntero1.equipos11[indice].D8);
                        listBox6.Items.Add("Producto del Número de Pasos por el Número de Presiones (D9): " + puntero1.equipos11[indice].D9);
                    }

                    else if (puntero1.equipos11[indice].Type == 9)
                    {
                        listBox6.Items.Add("Rendimiento Termodinámico (D1): " + puntero1.equipos11[indice].D1);
                        listBox6.Items.Add("Factor de Flujo (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.316749697));
                        listBox6.Items.Add("Pérdidas de Entalpía en el Escape en función de la Velocidad o Caudal Volumétrico (D4): " + puntero1.equipos11[indice].D4);
                        listBox6.Items.Add("Semisuma de las Calidades de Vapor entrada y salida escalón de vapor (D5): " + puntero1.equipos11[indice].D5);
                        listBox6.Items.Add("Presión salida/ Presión entrada (D8): " + puntero1.equipos11[indice].D8);
                        listBox6.Items.Add("Area Total de Escape (D9): " + puntero1.equipos11[indice].D9);
                    }

                    else if (puntero1.equipos11[indice].Type == 10)
                    {
                        listBox6.Items.Add("Rendimiento Termodinámico (D1): " + puntero1.equipos11[indice].D1);
                        listBox6.Items.Add("Presión en el Escape (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 * (6.8947572 / 100)));
                        listBox6.Items.Add("Factor de Flujo (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.316749697));
                        listBox6.Items.Add("Semisuma de las Calidades de Vapor entrada y salida escalón de vapor (D5): " + puntero1.equipos11[indice].D5);
                        listBox6.Items.Add("Presión salida/ Presión entrada (D8): " + puntero1.equipos11[indice].D8);
                    }

                    else if (puntero1.equipos11[indice].Type == 11)
                    {
                        listBox6.Items.Add("Rendimiento Termodinámico (D1): " + puntero1.equipos11[indice].D1);
                        listBox6.Items.Add("Presión de Escape (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 * (6.8947572 / 100)));
                        listBox6.Items.Add("Potencia (D5): " + Convert.ToString(puntero1.equipos11[indice].D5 / 0.9486608));
                    }

                    else if (puntero1.equipos11[indice].Type == 13)
                    {
                        listBox6.Items.Add("Eficiencia del Separador de Humedad (D1): " + puntero1.equipos11[indice].D1);
                        listBox6.Items.Add("Fracción Caudal Másico de entrada arrastrado (D2): " + puntero1.equipos11[indice].D2);
                    }

                    else if (puntero1.equipos11[indice].Type == 14)
                    {
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.984193609));
                        listBox6.Items.Add("Factor Porcentaje Pérdida Carga  (D4): " + puntero1.equipos11[indice].D4);
                        listBox6.Items.Add("TTD (D5): " + Convert.ToString(puntero1.equipos11[indice].D5 * (5.0 / 9.0)));
                        listBox6.Items.Add("Rendimiento Térmico (D6): " + puntero1.equipos11[indice].D6);
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D7): " + Convert.ToString(puntero1.equipos11[indice].D7 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D8): " + Convert.ToString(puntero1.equipos11[indice].D8 / 6.578911309));
                        listBox6.Items.Add("Factor Porcentaje Pérdida Carga (D9): " + Convert.ToString(puntero1.equipos11[indice].D9 * 2.984193609));
                    }

                    else if (puntero1.equipos11[indice].Type == 15)
                    {
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.984193609));
                        listBox6.Items.Add("Título a la Salida del Drenaje N4 (D5): " + puntero1.equipos11[indice].D5);
                        listBox6.Items.Add("Rendimiento Térmico (D6): " + puntero1.equipos11[indice].D6);
                        listBox6.Items.Add("Presión de Operación de Carcasa (D7): " + Convert.ToString(puntero1.equipos11[indice].D7 * (6.8947572 / 100)));
                        listBox6.Items.Add("Número de Condensadores en Paralelo (D8): " + puntero1.equipos11[indice].D8);
                    }

                    else if (puntero1.equipos11[indice].Type == 16)
                    {
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.984193609));
                        //Convertir ºC a ºF
                        if (puntero1.equipos11[indice].D4 > 500)
                        {
                            listBox6.Items.Add("DCA (D4): " + Convert.ToString(puntero1.equipos11[indice].D4));
                        }
                        else if (puntero1.equipos11[indice].D4 < 500)
                        {
                            listBox6.Items.Add("DCA (D4): " + Convert.ToString(puntero1.equipos11[indice].D4 * (5.0 / 9.0)));
                        }
                        listBox6.Items.Add("Rendimiento Térmico (D6): " + puntero1.equipos11[indice].D6);
                        listBox6.Items.Add("Número de Calentadores en Paralelo (D8): " + puntero1.equipos11[indice].D8);
                    }

                    //----------- A partir de aquí está pendiente incluir los factores de conversión de unidades del Sistema Británico al Sistema Internacional------------------------------------------------------------------------------------------------------------

                    else if (puntero1.equipos11[indice].Type == 17)
                    {
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.984193609));
                        listBox6.Items.Add("Factor Porcentaje Pérdida Carga  (D4): " + puntero1.equipos11[indice].D4);
                        listBox6.Items.Add("Incremento de Temperatura sobre la de saturación en la corriente N3 (D5): " + Convert.ToString(puntero1.equipos11[indice].D5 * (5.0 / 9.0)));
                        listBox6.Items.Add("Rendimiento Térmico (D6): " + puntero1.equipos11[indice].D6);
                    }

                    else if (puntero1.equipos11[indice].Type == 18)
                    {
                        listBox6.Items.Add("TTD (D5): " + Convert.ToString(puntero1.equipos11[indice].D5 * (5.0 / 9.0)));
                        listBox6.Items.Add("Rendimiento Térmico (D6): " + puntero1.equipos11[indice].D6);
                        listBox6.Items.Add("Equilibrado de Presiones con corriente de agua de alimentación (D7): " + puntero1.equipos11[indice].D7);
                        listBox6.Items.Add("Número de la corriente de cascada N5 (D9): " + puntero1.equipos11[indice].D9);
                    }

                    else if (puntero1.equipos11[indice].Type == 19)
                    {
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.984193609));
                        listBox6.Items.Add("CV de la Válvula (D4): " + puntero1.equipos11[indice].D4);
                        listBox6.Items.Add("Factor de Caudal Crítico (D5): " + puntero1.equipos11[indice].D5);
                        listBox6.Items.Add("CV Máximo (D6): " + puntero1.equipos11[indice].D6);
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D7): " + Convert.ToString(puntero1.equipos11[indice].D7 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D8): " + Convert.ToString(puntero1.equipos11[indice].D8 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D9): " + Convert.ToString(puntero1.equipos11[indice].D9 / 2.984193609));
                    }

                    else if (puntero1.equipos11[indice].Type == 20)
                    {
                        listBox6.Items.Add("Caudal por N3 (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (0.4536)));
                        listBox6.Items.Add("Entalpía en la Corriente N3 (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 * 2.326009));
                        listBox6.Items.Add("Define la presión en la corriente N3 (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 * (6.8947572 / 100)));
                    }

                    else if (puntero1.equipos11[indice].Type == 21)
                    {
                        listBox6.Items.Add("Presión de Operación (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                    }

                    else if (puntero1.equipos11[indice].Type == 22)
                    {
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D1): " + Convert.ToString(puntero1.equipos11[indice].D1 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D2): " + Convert.ToString(puntero1.equipos11[indice].D2 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D3): " + Convert.ToString(puntero1.equipos11[indice].D3 / 2.984193609));
                        listBox6.Items.Add("Factor Porcentaje Pérdida Carga (D4): " + puntero1.equipos11[indice].D4);
                        listBox6.Items.Add("Rendimiento Térmico (D5): " + puntero1.equipos11[indice].D5);
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D6): " + Convert.ToString(puntero1.equipos11[indice].D6 * (6.8947572 / 100)));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D7): " + Convert.ToString(puntero1.equipos11[indice].D7 / 6.578911309));
                        listBox6.Items.Add("Coeficiente Ecuación Cuadrática (D8): " + Convert.ToString(puntero1.equipos11[indice].D8 / 2.984193609));
                        listBox6.Items.Add("Factor Porcentaje Pérdida Carga (D9): " + puntero1.equipos11[indice].D9);
                    }
                    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                }

                //CONTINUAR CON EL RESTO DE EQUIPOS
            }
        }
    }
}
