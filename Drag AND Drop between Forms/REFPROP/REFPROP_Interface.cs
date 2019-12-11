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

namespace RefPropWindowsForms
{

    public partial class REFPROP_Interface : Form
    {
        public Aplicacion puntero2;

        public core luis = new core();

        //Input Data:
        public RefrigerantCategory Fluid_Category;
        public ReferenceState Fluid_Reference_State;

        public REFPROP_Interface(Aplicacion puntero1)
        {
            puntero2 = puntero1;

            InitializeComponent();
        }

        //Button "Calculate" click
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "PureFluid")
            {
                Fluid_Category = RefrigerantCategory.PureFluid;
            }
            if (comboBox1.Text == "PredefinedMixture")
            {
                Fluid_Category = RefrigerantCategory.PredefinedMixture;
            }
            if (comboBox1.Text == "NewMixture")
            {
                Fluid_Category = RefrigerantCategory.NewMixture;
            }
            if (comboBox1.Text == "PseudoPureFluid")
            {
                Fluid_Category = RefrigerantCategory.PseudoPureFluid;
            }

            if (comboBox3.Text == "DEF")
            {
                Fluid_Reference_State = ReferenceState.DEF;
            }
            if (comboBox3.Text == "ASH")
            {
                Fluid_Reference_State = ReferenceState.ASH;
            }
            if (comboBox3.Text == "IIR")
            {
                Fluid_Reference_State = ReferenceState.IIR;
            }
            if (comboBox3.Text == "NBP")
            {
                Fluid_Reference_State = ReferenceState.NBP;
            }

            luis.core1(this.comboBox2.Text);
            luis.working_fluid.Category = Fluid_Category;
            luis.working_fluid.reference = Fluid_Reference_State;
            //OK BIEN
           

            //PENDIENTE
            //Refrigerant myRefrigerant = new Refrigerant(RefrigerantCategory.PredefinedMixture, "R405A", ReferenceState.DEF);

            //OK BIEN
            //Refrigerant myRefrigerant = new Refrigerant(RefrigerantCategory.PureFluid, "R22", ReferenceState.DEF);

            //OK BIEN
            //Refrigerant myRefrigerant = new Refrigerant(RefrigerantCategory.NewMixture, "R32=0.69761,R125=0.30239",ReferenceState.DEF);

            //Console.WriteLine(myRefrigerant.MolecularWeight);
            //myRefrigerant.FindSaturatedStateWithTemperature(273, SaturationPoint.Dew_Point);
            //myRefrigerant.DisplayThermoDynamicState();

            luis.working_fluid.FindStateWithTP(Convert.ToDouble(this.textBox2.Text), Convert.ToDouble(this.textBox1.Text));
            //myRefrigerant.DisplayThermoDynamicState();

            this.textBox3.Text = luis.working_fluid.Density.ToString();
            this.textBox4.Text = luis.working_fluid.Enthalpy.ToString();
            this.textBox5.Text = luis.working_fluid.Entropy.ToString();

            this.textBox7.Text = luis.working_fluid.CriticalTemperature.ToString();
            this.textBox6.Text = luis.working_fluid.CriticalPressure.ToString();
            this.textBox8.Text = luis.working_fluid.CriticalDensity.ToString();
            
            //myRefrigerant.FindStateWithTD(300, 40 / myRefrigerant.MolecularWeight);
            //myRefrigerant.DisplayThermoDynamicState();

            //myRefrigerant.Display();

            //System.Console.ReadKey();
        }

        //OK Button
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "PureFluid")
            {
                Fluid_Category = RefrigerantCategory.PureFluid;
            }
            if (comboBox1.Text == "PredefinedMixture")
            {
                Fluid_Category = RefrigerantCategory.PredefinedMixture;
            }
            if (comboBox1.Text == "NewMixture")
            {
                Fluid_Category = RefrigerantCategory.NewMixture;
            }
            if (comboBox1.Text == "PseudoPureFluid")
            {
                Fluid_Category = RefrigerantCategory.PseudoPureFluid;
            }

            if (comboBox3.Text == "DEF")
            {
                Fluid_Reference_State = ReferenceState.DEF;
            }
            if (comboBox3.Text == "ASH")
            {
                Fluid_Reference_State = ReferenceState.ASH;
            }
            if (comboBox3.Text == "IIR")
            {
                Fluid_Reference_State = ReferenceState.IIR;
            }
            if (comboBox3.Text == "NBP")
            {
                Fluid_Reference_State = ReferenceState.NBP;
            }

            puntero2.luis1.core1(this.comboBox2.Text);
            puntero2.luis1.working_fluid.Category = Fluid_Category;
            puntero2.luis1.working_fluid.reference = Fluid_Reference_State;

            this.Dispose();
        }

        //Cancel Button
        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
