using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Drag_AND_Drop_between_Forms
{
    public partial class StreamResults : Form
    {
        public Aplicacion puntero1 = new Aplicacion();

        public StreamResults(Aplicacion puntero)
        {
            puntero1 = puntero;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Botón del Menú para VISUALIZAR RESULTADOS DE CORRIENTES
        public void visualizarResultadosStreams()
        {
            Double num = puntero1.p.Count;

            for (int a = 0; a < num; a++)
            {
                puntero1.p5.Add(puntero1.ptemp);
            }

            //Limpiamos el contenido del ListBox del Tabpage "StreamResults" en el tabcontrol2 de la aplicación principal
            listBox1.Items.Clear();

            //Caso de haber Creado un EJEMPLO de Validación del Motor de Cálculo y ser el Análisis Tipo Estacionario
            if ((puntero1.ejemplovalidacion == 1) && (puntero1.tipoanalisis == 0))
            {
                listBox1.Items.Add("Validación del Motor de Cálculo");
                listBox1.Items.Add("");
                listBox1.Items.Add("Tipo Análisis Estacionario.");
                listBox1.Items.Add("");
                listBox1.Items.Add("");

                for (int i = 0; i < puntero1.p.Count; i++)
                {
                    puntero1.p5[i].Nombre = puntero1.p[i].Nombre;
                    puntero1.p5[i].Value = puntero1.p[i].Value;
                    listBox1.Items.Add(puntero1.p5[i].ToString());
                }

                puntero1.ejemplovalidacion = 0;
            }

            //Caso de haber Creado un EJEMPLO de Validación del Motor de Cálculo y ser el Análisis Transitorio
            else if ((puntero1.ejemplovalidacion == 1) && (puntero1.tipoanalisis == 1))
            {
                this.chart2.Series.Clear();
                
                //Análisis de Transitorio tipo 0 (realizado con la librería de TrentGuidry)
                if (puntero1.tipoanalisistransitorio == 0)
                {
                    listBox1.Items.Add("Validación del Motor de Cálculo.");
                    listBox1.Items.Add("");
                    listBox1.Items.Add("Tipo Análisis Transitorio.");
                    listBox1.Items.Add("");
                    listBox1.Items.Add("");

                    //El primer bucle barre el número de iteraciones=(tiempofinal-tiempoinicial)/stepsize
                    for (int i = 0; i < puntero1.dRes.GetLength(0); i++)
                    {
                        //El segundo bucle recorre todos los parámetros
                        for (int j = 0; j < puntero1.dRes[0].GetLength(0); j++)
                        {
                            listBox1.Items.Add(puntero1.p[j].Nombre + "= " + puntero1.dRes[i][j].ToString());
                        }

                        listBox1.Items.Add("");
                    }

                    //Graficamos los resultados del Análisis TRANSITORIO 
                    //Tendrá que haber tantas series como funciones dependientes del tiempo                    
                    List<Series> graficas = new List<Series>();

                    //Hay tantas funciones dependientes como parámetros menos uno (que representa el tiempo)
                    for (int i = 0; i < puntero1.dRes[0].GetLength(0) - 1; i++)
                    {
                        Series serietemp = new Series();
                        serietemp.Name = "MyGraph" + Convert.ToString(i);
                        serietemp.Color = Color.Blue;
                        serietemp.Legend = "Legend1";
                        serietemp.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                        serietemp.ChartArea = "ChartArea1";
                        serietemp.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                        graficas.Add(serietemp);
                        serietemp = null;

                        this.chart2.Series.Add(graficas[i]);
                    }

                    //Primer Bucle que indica el número de intervalos de integración =tiempofinal-tiempoinicial/stepsize
                    for (int i = 0; i < puntero1.dRes.GetLength(0); i++)
                    {
                        //Segundo Bucle que indica el número de funciones a graficar
                        for (int j = 0; j < puntero1.dRes[0].GetLength(0) - 1; j++)
                        {
                            chart2.Series["MyGraph" + Convert.ToString(j)].Points.AddXY(puntero1.dRes[i][0], puntero1.dRes[i][j + 1]);
                        }
                    }

                    graficas = null;

                    puntero1.ejemplovalidacion = 0;
                }

                //Análsis de Transitorio tipo 1 (realizado con la librería de DotNumerics)
                else if (puntero1.tipoanalisistransitorio == 1)
                {
                    listBox1.Items.Add("Validación del Motor de Cálculo.");
                    listBox1.Items.Add("");
                    listBox1.Items.Add("Tipo Análisis Transitorio.");
                    listBox1.Items.Add("");
                    listBox1.Items.Add("");

                    //El primer bucle barre el número de iteraciones=(tiempofinal-tiempoinicial)/stepsize
                    for (int i = 0; i < puntero1.sol.GetLength(0); i++)
                    {
                        //El segundo bucle recorre todos los parámetros
                        for (int j = 0; j < puntero1.sol.GetLength(1); j++)
                        {
                            listBox1.Items.Add(puntero1.p[j].Nombre + "= " + puntero1.sol[i, j].ToString());
                        }

                        listBox1.Items.Add("");
                    }

                    //Graficamos los resultados del Análisis TRANSITORIO 
                    //Tendrá que haber tantas series como funciones dependientes del tiempo                    
                    List<Series> graficas = new List<Series>();

                    //Hay tantas funciones dependientes como parámetros menos uno (que representa el tiempo)
                    for (int i = 0; i < puntero1.sol.GetLength(1) - 1; i++)
                    {
                        Series serietemp = new Series();
                        serietemp.Name = "MyGraph" + Convert.ToString(i);
                        serietemp.Color = Color.Blue;
                        serietemp.Legend = "Legend1";
                        serietemp.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                        serietemp.ChartArea = "ChartArea1";
                        serietemp.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                        graficas.Add(serietemp);
                        serietemp = null;

                        this.chart2.Series.Add(graficas[i]);
                    }

                    //Primer Bucle que indica el número de intervalos de integración =tiempofinal-tiempoinicial/stepsize
                    for (int i = 0; i < puntero1.sol.GetLength(0); i++)
                    {
                        //Segundo Bucle que indica el número de funciones a graficar
                        for (int j = 0; j < puntero1.sol.GetLength(1) - 1; j++)
                        {
                            chart2.Series["MyGraph" + Convert.ToString(j)].Points.AddXY(puntero1.sol[i, 0], puntero1.sol[i, j + 1]);
                        }
                    }

                    graficas = null;

                    puntero1.ejemplovalidacion = 0;
                }
            }

            //Caso de no estar ejecutando un ejemplo de validación del Motor de Cáculo
            else
            {
                //Unidades Sistema Internacional
                if (puntero1.unidades == 1)
                {
                    listBox1.Items.Add("Unidades: W(Kgr/sg), P(kPa), H(Kj/Kgr)");
                    listBox1.Items.Add("");
                }

                //Unidades Métricas
                else if (puntero1.unidades == 2)
                {
                    listBox1.Items.Add("Unidades: W(Kgr/sg), P(Bar), H(Kj/Kgr)");
                    listBox1.Items.Add("");
                }
                //Unidades Británicas

                else if (puntero1.unidades == 0)
                {
                    listBox1.Items.Add("Unidades: W(Lb/sg), P(psia), H(BTU/Lb)");
                    listBox1.Items.Add("");
                }

                listBox1.Items.Add("Nº Variables:" + Convert.ToString(puntero1.numvariables));
                listBox1.Items.Add("Nombre de las variables:");
                listBox1.Items.Add("");

                for (int i = 0; i < puntero1.p.Count; i++)
                {
                    //Unidades
                    //Sistema Britanico=0;Sistema Internacional=1;Sistema Métrico=2

                    //Unidades Sistema Internacional
                    if (puntero1.unidades == 1)
                    {
                        String primercaracter = puntero1.listaresultadoscorrientes[i, puntero1.setnumber].Nombre.Substring(0, 1);

                        if (primercaracter == "W")
                        {
                            puntero1.p5[i].Value = puntero1.listaresultadoscorrientes[i, puntero1.setnumber].Value * (0.4536);
                        }
                        else if (primercaracter == "P")
                        {
                            puntero1.p5[i].Value = puntero1.listaresultadoscorrientes[i, puntero1.setnumber].Value * (6.8947572);
                        }
                        else if (primercaracter == "H")
                        {
                            puntero1.p5[i].Value = puntero1.listaresultadoscorrientes[i, puntero1.setnumber].Value * 2.326009;
                        }

                        puntero1.p5[i].Nombre = puntero1.listaresultadoscorrientes[i, puntero1.setnumber].Nombre;
                        listBox1.Items.Add(puntero1.p5[i].ToString());
                        //listBox1.Items.Add("---");
                        //listBox1.Items.Add(listaresultadoscorrientes[i, setnumber].ToString());    
                    }

                    //Unidades Sistema Métrico
                    else if (puntero1.unidades == 2)
                    {
                        String primercaracter = puntero1.listaresultadoscorrientes[i, puntero1.setnumber].Nombre.Substring(0, 1);

                        if (primercaracter == "W")
                        {
                            (puntero1.p5[i].Value) = ((puntero1.listaresultadoscorrientes[i, puntero1.setnumber].Value) * 0.4536);
                        }
                        else if (primercaracter == "P")
                        {
                            puntero1.p5[i].Value = puntero1.listaresultadoscorrientes[i, puntero1.setnumber].Value * (6.8947572 / 100);
                        }
                        else if (primercaracter == "H")
                        {
                            puntero1.p5[i].Value = puntero1.listaresultadoscorrientes[i, puntero1.setnumber].Value * 2.326009;
                        }

                        puntero1.p5[i].Nombre = puntero1.listaresultadoscorrientes[i, puntero1.setnumber].Nombre;
                        listBox1.Items.Add(puntero1.p5[i].ToString());
                        //listBox1.Items.Add("---");
                        //listBox1.Items.Add(listaresultadoscorrientes[i, setnumber].ToString());    
                    }

                    //Unidades Sistema Británico
                    else if (puntero1.unidades == 0)
                    {
                        puntero1.p5[i] = puntero1.listaresultadoscorrientes[i, puntero1.setnumber];
                        listBox1.Items.Add(puntero1.p5[i].ToString());
                        //listBox1.Items.Add("---");
                        //listBox1.Items.Add(listaresultadoscorrientes[i, setnumber].ToString());                        
                    }
                }
            }

            puntero1.p5.Clear();

            listBox1.Items.Add("");
        }

        private void StreamResults_Load(object sender, EventArgs e)
        {
            visualizarResultadosStreams();
        }
    }
}
