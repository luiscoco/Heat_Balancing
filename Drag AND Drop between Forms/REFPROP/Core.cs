using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc.net;
using System.Windows.Forms;

namespace RefPropWindowsForms
{
    public partial class core
    {
        public Refrigerant working_fluid;
        public Double wmm = 0;

      public void core1(string workingfluidname)
      {
         working_fluid = new Refrigerant(RefrigerantCategory.PureFluid,workingfluidname,ReferenceState.DEF);
         //Molar weight for converting enthalpy, entropy and density from mol basis to mass basis
      }

      public class Compressor : core
      {
         public  Double D_rotor = 0.0;      // rotor diameter (m)
         public  Double D_rotor_2 = 0.0;    //secondary rotor diameter (m) [used for two-stage recompressor, if necessary]
         public  Double N_design = 0.0;     //design-point shaft speed (rpm)
         public  Double eta_design = 0.0;   //design-point isentropic efficiency (-) [or stage efficiency in two-stage recompressor]
         public  Double phi_design = 0.0;   //design-point flow coefficient (-)
         public  Double phi_min = 0.0;     //surge limit (-)
         public  Double phi_max = 0.0;      //choke limit / zero pressure rise limit / x-intercept (-)
         public  Double N = 0.0;            //shaft speed (rpm)
         public  Double eta = 0.0;          //isentropic efficiency (-)
         public  Double phi = 0.0;          //dimensionless flow coefficient (-)
         public  Double phi_2 = 0.0;        //secondary dimensionless flow coefficient (-) [used for second stage phi, if necessary]
         public  Double w_tip_ratio = 0.0;  //ratio of the local (comp outlet) speed of sound to the tip speed (-)
         public Boolean surge = false;       //true if the compressor is in the surge region

         void Compressor1()
         {
    
         }
      }

      public class Turbine : core
      {
          public Double D_rotor = 0.0;     //rotor diameter (m)
          public Double A_nozzle = 0.0;    //effective nozzle area (m2)
          public Double N_design = 0.0;     //design-point shaft speed (rpm)
          public Double eta_design = 0.0;   //design-point isentropic efficiency (-)
          public Double N = 0.0;            //shaft speed (rpm)
          public Double eta = 0.0;          //isentropic efficiency (-)
          public Double nu = 0.0;           //ratio of tip speed to spouting velocity (-)
          public Double w_tip_ratio = 0.0;  //ratio of the local (turbine inlet) speed of sound to the tip speed (-)
          
          void Turbine1()
          { 
  
          }
      }

      public class HeatExchanger : core
      {
          //Under design conditions, streams are defined as cold (1) and hot (2)
          public Double UA_design = 0.0;                 //design-point conductance (kW/K)
          public Double DP_design1 ;  //design-point pressure drops across the heat exchanger (kPa)   
          public Double DP_design2 ;  //design-point pressure drops across the heat exchanger (kPa)
          public Double[] m_dot_design = new Double[2];  //0:Cold, 1:Hot; design-point mass flow rates of the two streams (kg/s)
          public Double Q_dot = 0.0;                       //heat transfer rate (kW)
          public Double UA = 0.0;                          //conductance (kW/K)
          public Double min_DT = 0.0;                      //minimum temperature difference in hxr (K)
          public Double eff = 0.0;                         //heat exchanger effectiveness (-)
          public Double C_dot_cold = 0.0;                  //cold stream capacitance rate (kW/K)
          public Double C_dot_hot = 0.0;                   //hot stream capacitance rate (kW/K)
          public Int64 N_sub = 1;                            //number of sub-heat exchangers used in model

          public Double T_c_in;
          public Double T_h_in;
          public Double P_c_in;
          public Double P_h_in;
          public Double P_c_out;
          public Double P_h_out;

          void HeatExchanger1()
          {

          }
      }

      public class RecompCycle : core
      {
          public Double W_dot_net;                        //net power output of the cycle (kW)
          public Double eta_thermal;                      //thermal efficiency of the cycle (-)
          public Double recomp_frac;                      //amount of flow that bypasses the precooler and is compressed in the recompressor (-)
          public Double m_dot_turbine;                    //mass flow rate through the turbine (kg/s)
          public Double high_pressure_limit;              //maximum allowable high-side pressure (kPa)
          public Double conv_tol;                         //relative convergence tolerance used during iteration loops involving this cycle (-)
          public Turbine t = new Turbine();                 //turbine user-defined type
          public Turbine t_rh = new Turbine();            //turbine user-defined type
          public Compressor mc = new Compressor();        //compressor and recompressor user-defined types
          public Compressor rc = new Compressor();        //compressor and recompressor user-defined types
          public HeatExchanger LT = new HeatExchanger();  //heat exchanger Low Temperature Recuperator
          public HeatExchanger HT = new HeatExchanger();  //heat exchanger High Temperature Recuperator
          public HeatExchanger PHX = new HeatExchanger();  //heat exchanger Primary Heat Exchanger
          public HeatExchanger RHX = new HeatExchanger();  //heat exchanger ReHeating Heat Exchanger
          public HeatExchanger PC = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
          public Double[] temp = new Double[12];          //thermodynamic properties at the state points of the cycle (K, kPa, kJ/kg, kJ/kg-K, kg/m3)
          public Double[] pres = new Double[12];
          public Double[] enth = new Double[12];
          public Double[] entr = new Double[12];
          public Double[] dens = new Double[12];   

          void RecompCycle1()
          { 
 
          }
      }

      public class ErrorTrace : core
      { 
          Int64 code = 0;     // the generated error code
          Int64 [] lines = new Int64[4];  // the lines of the calls that generated the error (warning: these are hard-coded and need to be updated if file changes)
          Int64[] files = new Int64[4];  // the files of the calls that generated the error, using:
          //1: core, 2: design_point, 3: off_design_point, 4: compressors, 5: turbines, 6: heat_exchangers, 7+: user-defined
      }

      //Function for calculating turbomachines outlet conditions given the inlet conditions and the efficiency
      public void calculate_turbomachine_outlet(core luis, Double T_in, Double P_in, Double P_out, Double eta, Boolean is_comp, ref Int64 error_code, ref Double enth_in, ref Double entr_in, ref Double dens_in, ref Double temp_out, ref Double enth_out, ref Double entr_out, ref Double dens_out, ref Double spec_work)
      {
          wmm = luis.working_fluid.MolecularWeight;

         // Determine the outlet state of a compressor or turbine using isentropic efficiency and outlet pressure.
         
         // Inputs:
         //   T_in -- inlet temperature (K)
         //   P_in -- inlet pressure (kPa)
         //   P_out -- outlet pressure (kPa)
         //   eta -- isentropic efficiency (-)
         //   is_comp -- if .true., model a compressor (w = w_s / eta); if .false., model a turbine (w = w_s * eta)
    
         // Outputs:
         //   error_trace -- an ErrorTrace object
         //   enth_in -- inlet specific enthalpy (kJ/kg) [optional]
         //   entr_in -- inlet specific entropy (kJ/kg-K) [optional]
         //   dens_in -- inlet fluid density (kg/m3) [optional]
         //   temp_out -- outlet fluid temperature (K) [optional]
         //   enth_out -- outlet specific enthalpy (kJ/kg) [optional]
         //   entr_out -- outlet specific entropy (kJ/kg-K) [optional]
         //   dens_out -- outlet fluid density (kg/m3) [optional]
         //   spec_work -- specific work of the turbomachine (kJ/kg) [optional]
         
         // Notes:
         //   1) The specific work of the turbomachine is positive for a turbine and negative for a compressor.
         //   2) No error checking is performed on the inlet and outlet pressures; valid pressure ratios are assumed.

       //Local Variables
       Double w_s, w, h_s_out_mol, h_s_out;
       Double enth_in_mol, entr_in_mol, dens_in_mol, entr_out_mol, dens_out_mol;
       //Int64 error_code;

       //This funcitions call TP, PS and PH to calculate working_fluid states:

       // Calculate rest of properties at the INLET CONDITIONS
       //Function TP: inputs (Temperature (K) and Pressure(kPa)); outputs (enth=h_in, entr=s_in, dens=dens_in)
       working_fluid.FindStateWithTP(T_in, P_in);
       enth_in = working_fluid.Enthalpy;
       entr_in = working_fluid.Entropy;
       dens_in = working_fluid.Density;
       //if (working_fluid.ierr!= 0) 
       //{
       //    MessageBox.Show("Error calculating the INLET CONDITIONS, calling working fluid TP function in Core.cs file in the function 'calculate_turbomachine_outlet'");
       //  return;
       //}

       //Calculates OUTLET CONDITIONS: Enthalpy if compression/expansion is isentropic
       //Function PS: inputs (Pressure (kPa) and Entropy(J/mol K)); outputs (enth=h_s_out)
       entr_in_mol=entr_in*wmm;
       working_fluid.FindStatueWithPS(P_out, entr_in_mol);
       h_s_out= working_fluid.Enthalpy;

        w_s = enth_in - h_s_out;  // specific work if process is isentropic (negative for compression, positive for expansion)
       
        if (is_comp) 
        {
        w = w_s / eta;            // actual specific work of compressor (negative value)
        }
        else
        {
        w = w_s * eta;            // actual specific work of turbine (positive value)
        }

        enth_out = enth_in - w;   // energy balance on turbomachine

       //Calculate properties at OUTLET CONDITIONS
       //Function PH: inputs (Pressure (kPa) and Enthalpy (J/mol)); outputs (temp=temp_out, entr=entr_out, dens=dens_out)
       working_fluid.FindStatueWithPH(P_out, enth_out*wmm);
       temp_out = working_fluid.Temperature;
       entr_out = working_fluid.Entropy;
       dens_out = working_fluid.Density;
       spec_work = w;
      }
        
      //Function for Calculating the Polytrophic efficienccy in the Turbomachines
      public void isen_eta_from_poly_eta(core luis, Double T_in, Double P_in, Double P_out, Double poly_eta, Boolean is_comp, ref Int64 error_code, ref Double isen_eta)
      {
          wmm = luis.working_fluid.MolecularWeight;

          //Calculate the isentropic efficiency that corresponds to a given polytropic efficiency
          //for the expansion or compression from T_in and P_in to P_out.
          //
          // Inputs:
          //   T_in -- inlet temperature (K)
          //   P_in -- inlet pressure (kPa)
          //   P_out -- outlet pressure (kPa)
          //   poly_eta -- polytropic efficiency (-)
          //   is_comp -- if .true., model a compressor (w = w_s / eta); if .false., model a turbine (w = w_s * eta)
          //
          // Outputs:
          //   error_trace -- an ErrorTrace object
          //   isen_eta -- the equivalent isentropic efficiency (-)
          //
          // Notes:
          //   1) Integration of small DP is approximated numerically by using 200 stages.
          //   2) No error checking is performed on the inlet and outlet pressures; valid pressure ratios are assumed.


          // Parameters
          Int64 stages = 200; 

          // Local Variables
          Double h_in, s_in, h_s_out, w_s, w, stage_DP;
          Double stage_P_in, stage_P_out, stage_h_in, stage_s_in, stage_h_s_out;
          Double stage_h_out = 0;
          Int64 stage;

          working_fluid.FindStateWithTP(T_in,P_in); // properties at the inlet conditions
          h_in = working_fluid.Enthalpy;
          s_in = working_fluid.Entropy;

          working_fluid.FindStatueWithPS(P_out, s_in*wmm);  // outlet enthalpy if compression/expansion is isentropic
          h_s_out = working_fluid.Enthalpy;

          stage_P_in = P_in;   // initialize first stage inlet pressure
          stage_h_in = h_in;   // initialize first stage inlet enthalpy
          stage_s_in = s_in;   // initialize first stage inlet entropy
          stage_DP = (P_out - P_in) / Convert.ToDouble(stages);  // pressure change per stage

          for (stage = 1; stage < stages; stage++)
          {
             stage_P_out = stage_P_in + stage_DP;

             //Calculate outlet enthalpy if compression/expansion is isentropic
             working_fluid.FindStatueWithPS(stage_P_out,stage_s_in*wmm);
             stage_h_s_out=working_fluid.Enthalpy;  

             w_s = stage_h_in - stage_h_s_out;  // specific work if process is isentropic
         
             if (is_comp==true)
             {
               w = w_s / poly_eta;            // actual specific work of compressor (negative value)
             }  
        
             else
             {
               w = w_s * poly_eta;            // actual specific work of turbine (positive value)
             }
        
             stage_h_out = stage_h_in - w;      // energy balance on stage

             // Reset next stage inlet values.
             stage_P_in = stage_P_out;
             stage_h_in = stage_h_out;

             working_fluid.FindStatueWithPH(stage_P_in,stage_h_in*wmm);
             stage_s_in=working_fluid.Entropy;
           }

           // Note: last stage outlet enthalpy is equivalent to turbomachine outlet enthalpy.

           if (is_comp==true)
           {
              isen_eta = (h_s_out - h_in) / (stage_h_out - h_in);
           }
           
           else
           {
              isen_eta = (stage_h_out - h_in) / (h_s_out - h_in);
           }
      }

      //Function for calculating Heat Exchanger Conductance (UA) for supercritical Brayton power cycles
      //Next step will be fixing the Effectiveness in Heat Exchangers
      public void calculate_hxr_UA(Int64 N_sub_hxrs, Double Q_dot, Double m_dot_c, Double m_dot_h, Double T_c_in, Double T_h_in, Double P_c_in, Double P_c_out, Double P_h_in, Double P_h_out,
          ref Int64 error_code, ref Double UA, ref Double min_DT,ref Double [] Th1, ref Double [] Tc1,ref Double Effec, ref Double [] Ph1, ref Double [] Pc1, ref Double [] UA_local)
      {
          wmm = working_fluid.MolecularWeight;

          // Calculate the conductance (UA value) and minimum temperature difference of a heat exchanger
          // given its mass flow rates, inlet temperatures, and a rate of heat transfer.
          //
          // Inputs:
          //   N_sub_hxrs -- the number of sub-heat exchangers to use for discretization
          //   Q_dot -- rate of heat transfer in the heat exchanger (kW)
          //   m_dot_c -- cold stream mass flow rate (kg/s)
          //   m_dot_h -- hot stream mass flow rate (kg/s)
          //   T_c_in -- cold stream inlet temperature (K)
          //   T_h_in -- hot stream inlet temperature (K)
          //   P_c_in -- cold stream inlet pressure (kPa)
          //   P_c_out -- cold stream outlet pressure (kPa)
          //   P_h_in -- hot stream inlet pressure (kPa)
          //   P_h_out -- hot stream outlet pressure (kPa)
          //
          // Outputs:
          //   error_trace -- an ErrorTrace object
          //   UA -- heat exchanger conductance (kW/K)
          //   min_DT -- minimum temperature difference ("pinch point") between hot and cold streams in heat exchanger (K)
          //
          // Notes:
          //   1) Total pressure drop for each stream is divided equally among the sub-heat exchangers (i.e., DP is a linear distribution).


          //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
          Double TempH, TempC, h_c_in_mol;
          // Local Variables
          Double h_c_in, h_h_in, h_c_out, h_h_out;
          Double[] P_c = new Double[N_sub_hxrs + 1];
          Double[] P_h = new Double[N_sub_hxrs + 1];
          Double[] T_c = new Double[N_sub_hxrs + 1];
          Double[] T_h = new Double[N_sub_hxrs + 1];
          Double[] h_c = new Double[N_sub_hxrs + 1];
          Double[] h_h = new Double[N_sub_hxrs + 1];
          Double[] tempdifferences = new Double[N_sub_hxrs + 1];

          Double[] C_dot_c = new Double[N_sub_hxrs];
          Double[] C_dot_h = new Double[N_sub_hxrs];
          Double[] C_dot_min = new Double[N_sub_hxrs];
          Double[] C_dot_max = new Double[N_sub_hxrs];
          Double[] C_R = new Double[N_sub_hxrs];
          Double[] eff = new Double[N_sub_hxrs];
          Double[] NTU = new Double[N_sub_hxrs];

          // Check inputs.
          if (T_h_in < T_c_in)
          {
              error_code = 5;
              return;
          }

          if (P_h_in < P_h_out)
          {
              error_code = 6;
              return;
          }

          if (P_c_in < P_c_out)
          {
              error_code = 7;
              return;
          }

          if (Math.Abs(Q_dot) <= 1d - 12)  // very low Q_dot; assume it is zero
          {
              UA = 0.0;
              min_DT = T_h_in - T_c_in;
              return;
          }

          // Assume pressure varies linearly through heat exchanger.
          for (int a = 0; a <= N_sub_hxrs; a++)
          {
              P_c[a] = P_c_out + a * (P_c_in - P_c_out) / N_sub_hxrs;
              P_h[a] = P_h_in + a * (P_h_in - P_h_out) / N_sub_hxrs;
              
              Pc1[a] = P_c[a];
              Ph1[a] = P_h[a];
          }

          // Calculate inlet enthalpies from known state points.

          //if (present(enth)) enth = enth_mol / wmm
          //if (present(entr)) entr = entr_mol / wmm
          //if (present(ssnd)) ssnd = ssnd_RP


          //call CO2_TP(T=T_c_in, P=P_c(N_sub_hxrs+1), error_code=error_code, enth=h_c_in)
          working_fluid.FindStateWithTP(T_c_in, P_c[N_sub_hxrs]);
          h_c_in = working_fluid.Enthalpy;

          //call CO2_TP(T=T_h_in, P=P_h(1), error_code=error_code, enth=h_h_in)
          working_fluid.FindStateWithTP(T_h_in, P_h[0]);
          h_h_in = working_fluid.Enthalpy;

          // Calculate outlet enthalpies from energy balances supporsing 100% Heat transferred
          h_c_out = h_c_in + Q_dot / m_dot_c;
          h_h_out = h_h_in - Q_dot / m_dot_h;

          // Set up the enthalpy vectors and loop through the sub-heat exchangers, calculating temperatures.
          for (int b = 0; b <= N_sub_hxrs; b++)
          {
              h_c[b] = h_c_out + b * (h_c_in - h_c_out) / N_sub_hxrs;  // create linear vector of cold stream enthalpies, with index 1 at the cold stream outlet
              h_h[b] = h_h_in - b * (h_h_in - h_h_out) / N_sub_hxrs;   // create linear vector of hot stream enthalpies, with index 1 at the hot stream inlet
          }

          T_h[0] = T_h_in;  //hot stream inlet temperature

          //IMPORTANT!!!: When calling call CO2_PH is necessary before converting the Enthalpy units from kJ/Kg to J/mol

          wmm = working_fluid.MolecularWeight;


          //call CO2_PH(P=P_c(1), H=h_c(1), error_code=error_code, temp=T_c(1))  ! cold stream outlet temperature
          TempC = h_c[0] * wmm;
          working_fluid.FindStatueWithPH(P_c[0], TempC);
          T_c[0] = working_fluid.Temperature;

          if (T_c[0] >= T_h[0])  // there was a second law violation in this sub-heat exchanger
          {
              error_code = 11;
              return;
          }

          for (int c = 0; c <= N_sub_hxrs; c++)
          {
              // call CO2_PH(P=P_h(i), H=h_h(i), error_code=error_code, temp=T_h(i))
              //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
              TempH = h_h[c] * wmm;  // convert enthalpy to molar basis
              working_fluid.FindStatueWithPH(P_h[c], TempH);
              T_h[c] = working_fluid.Temperature;

              // call CO2_PH(P=P_c(i), H=h_c(i), error_code=error_code, temp=T_c(i))
              //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
              TempC = h_c[c] * wmm;  // convert enthalpy to molar basis
              working_fluid.FindStatueWithPH(P_c[c], TempC);
              T_c[c] = working_fluid.Temperature;

              if (T_c[c] >= T_h[c])  // there was a second law violation in this sub-heat exchanger
              {
                  error_code = 11;
                  return;
              }
          }

          //UP TO HERE VALIDATED Temperatures and Enthapies

          // Perform effectiveness-NTU and UA calculations (note: the below are all array operations).
          for (int d = 0; d < N_sub_hxrs; d++)
          {
              C_dot_h[d] = m_dot_h * (h_h[d] - h_h[d+1]) / (T_h[d] - T_h[d+1]);  // hot stream capacitance rate
          }

          for (int e = 0; e < N_sub_hxrs; e++)
          {
              C_dot_c[e] = m_dot_c * (h_c[e] - h_c[e+1]) / (T_c[e] - T_c[e+1]);  // cold stream capacitance rate
          }

          for (int f = 0; f <= N_sub_hxrs-1; f++)
          {
              C_dot_min[f] = Math.Min(C_dot_h[f], C_dot_c[f]);  // minimum capacitance stream
              C_dot_max[f] = Math.Max(C_dot_h[f], C_dot_c[f]);  // maximum capacitance stream
              C_R[f] = C_dot_min[f] / C_dot_max[f];
              eff[f] = Q_dot / ((N_sub_hxrs * C_dot_min[f] * (T_h[f] - T_c[f+1])));  // effectiveness of each sub-heat exchanger

              if(C_R[f]==1)
              {
                  NTU[f] = eff[f] / (1 - eff[f]);
              }

              else
              {
                 NTU[f] = Math.Log((1 - eff[f] * C_R[f]) / (1 - eff[f])) / (1 - C_R[f]);  // NTU if C_R does not equal 1
              }
          }

          UA = 0;
          
          for (int g = 0; g <= N_sub_hxrs-1; g++)
          {
              UA_local[g] = NTU[g] * C_dot_min[g];
              UA = UA + NTU[g] * C_dot_min[g];  // calculate total UA value for the heat exchanger
              
          }

          for (int h = 0; h <= N_sub_hxrs; h++)
          {
              tempdifferences[h] = T_h[h] - T_c[h]; // temperatures differences within the heat exchanger
          }

          min_DT=tempdifferences[0];

          for (int i = 0; i <= N_sub_hxrs; i++)
          {
              if (tempdifferences[i]<min_DT)
              {
                min_DT = tempdifferences[i]; // find the smallest temperature difference within the heat exchanger
              }

              Th1[i] = T_h[i];
              Tc1[i] = T_c[i];
          }

          // Calculate PHX Effectiveness
          Double C_dot_hot, C_dot_cold, C_dot_min1, Q_dot_max;

          C_dot_hot = m_dot_h * (h_h_in - h_h_out) / (T_h[0] - T_h[15]);   // PHX recuperator hot stream capacitance rate
          C_dot_cold = m_dot_c * (h_c_out - h_c_in) / (T_c[0] - T_c[15]);  // PXH recuperator cold stream capacitance rate
          C_dot_min1 = Math.Min(C_dot_hot, C_dot_cold);
          Q_dot_max = C_dot_min1 * (T_h[0] - T_c[15]);
          Effec = Q_dot / Q_dot_max;  // Definition of effectiveness
      }

      // Funtion for calculating the Recompression Brayton Power cycle Design-Point performance
      public void Design_Point_RC(core luis, ref core.RecompCycle recomp_cycle, Double W_dot_net, Double T_mc_in, Double T_t_in, Double P_mc_in, Double P_mc_out, Double P_rhx_in
                               , Double T_rht_in, Double DP_LT_c, Double DP_HT_c, Double DP_PC, Double DP_PHX, Double DP_RHX,
                               Double DP_LT_h, Double DP_HT_h, Double UA_LT, Double UA_HT, Double recomp_frac, Double eta_mc,
                               Double eta_rc, Double eta_t, Double eta_trh, Int64 N_sub_hxrs, Double tol)
      {
        //Local Variables
        Int64  error_code, index;
        Double w_mc, w_rc, w_t, w_trh, C_dot_min, Q_dot_max;
        Double T9_lower_bound, T9_upper_bound, T8_lower_bound, T8_upper_bound, last_LT_residual, last_T9_guess;
        Double last_HT_residual, last_T8_guess, secant_guess;
        Double m_dot_t, m_dot_mc, m_dot_rc, eta_mc_isen, eta_rc_isen, eta_t_isen, eta_trh_isen;
        Double min_DT_LT, min_DT_HT, UA_LT_calc, UA_HT_calc, Q_dot_LT, Q_dot_HT, UA_HT_residual, UA_LT_residual;
        Double[] temp = new Double[12];
        Double[] pres = new Double[12];
        Double[] enth = new Double[12];
        Double[] entr = new Double[12];
        Double[] dens = new Double[12];

        Double[] T_c = new Double[N_sub_hxrs + 1];
        Double[] T_h = new Double[N_sub_hxrs + 1];

        Double[] P_c = new Double[N_sub_hxrs + 1];
        Double[] P_h = new Double[N_sub_hxrs + 1];

        Double[] UA_local = new Double[N_sub_hxrs];

        Double Effec = 0;
        
        //Parameters
        Int64 max_iter = 10;
        Double temperature_tolerance = 1.0e-6;  // temperature differences below this are considered zero

        error_code = 0;
        eta_mc_isen = 0;
        eta_rc_isen = 0;
        eta_t_isen = 0;
        eta_trh_isen = 0;
        min_DT_HT = 0;
        min_DT_LT = 0;
        w_mc = 0;
        w_rc = 0;
        w_t = 0;
        w_trh = 0;
        
          //Initialize a few variables.
          m_dot_t = 0.0;
          m_dot_mc = 0.0;
          m_dot_rc = 0.0;
          Q_dot_LT = 0.0;
          Q_dot_HT = 0.0;
          UA_LT_calc = 0.0;
          UA_HT_calc = 0.0;
          temp[0] = T_mc_in;
          pres[0] = P_mc_in;
          pres[1] = P_mc_out;
          temp[5] = T_t_in;
          pres[10] = P_rhx_in;
          temp[11] = T_rht_in;

          //Apply pressure drops to heat exchangers, fully defining the pressures at all states.

          // relative pressure drop specified for LT recuperator (cold stream)
          if (DP_LT_c < 0.0)
          {
              pres[2] = pres[1] - pres[1] * Math.Abs(DP_LT_c);
          }

          // absolute pressure drop specified for LT recuperator (cold stream)
          else
          {
              pres[2] = pres[1] - DP_LT_c;
          }

          // if there is no LT recuperator, there is no pressure drop
          if (UA_LT < 1.0e-12)
          {
              pres[2] = pres[1];
          }
          pres[3] = pres[2];        // assume no pressure drop in mixing valve
          pres[9] = pres[2];       // assume no pressure drop in mixing valve

          // relative pressure drop specified for HT recuperator (cold stream)
          if (DP_HT_c < 0.0)
          {
              pres[4] = pres[3] - pres[3] * Math.Abs(DP_HT_c);
          }

          // absolute pressure drop specified for HT recuperator (cold stream)
          else
          {
              pres[4] = pres[3] - DP_HT_c;
          }

          //if there is no HT recuperator, there is no pressure drop
          if (UA_HT < 1.0e-12)
          {
              pres[4] = pres[3];
          }

          // relative pressure drop specified for PHX
          if (DP_PHX < 0.0)
          {
              pres[5] = pres[4] - pres[4] * Math.Abs(DP_PHX);
          }
          // absolute pressure drop specified for PHX
          else
          {
              pres[5] = pres[4] - DP_PHX;
          }

          // relative pressure drop specified for RHX
          if (DP_RHX < 0.0)
          {
              pres[11] = pres[10] - pres[10] * Math.Abs(DP_RHX);
          }

          // absolute pressure drop specified for RHX
          else
          {
              pres[11] = pres[10] - DP_RHX;
          }

          // relative pressure drop specified for precooler: P1=P9-P9*rel_DP => P1=P9*(1-rel_DP)
          if (DP_PC < 0.0)
          {
              pres[8] = pres[0] / (1.0 - Math.Abs(DP_PC));
          }

          // absolute pressure drop specified for precooler
          else
          {
              pres[8] = pres[0] + DP_PC;
          }

          // relative pressure drop specified for LT recuperator (hot stream)
          if (DP_LT_h < 0.0)
          {
              pres[7] = pres[8] / (1.0 - Math.Abs(DP_LT_h));
          }

          // absolute pressure drop specified for LT recuperator (hot stream)
          else
          {
              pres[7] = pres[8] + DP_LT_h;
          }

          // if there is no LT recuperator, there is no pressure drop
          if (UA_LT < 1.0e-12)
          {
              pres[7] = pres[8];
          }

          // relative pressure drop specified for HT recuperator (hot stream)
          if (DP_HT_h < 0.0)
          {
              pres[6] = pres[7] / (1.0 - Math.Abs(DP_HT_h));
          }

         // absolute pressure drop specified for HT recuperator (hot stream)
          else
          {
              pres[6] = pres[7] + DP_HT_h;
          }

          // if there is no HT recuperator, there is no pressure drop
          if (UA_HT < 1.0e-12)
          {
              pres[6] = pres[7];
          }

          // Determine equivalent isentropic efficiencies for Main Compressor if necessary if given the Polytropic Efficiency
          if (eta_mc < 0.0)
          {
              luis.isen_eta_from_poly_eta(luis,temp[0], pres[0], pres[1], Math.Abs(eta_mc), true, ref error_code, ref eta_mc_isen);
          }

          else
          {
              eta_mc_isen = eta_mc;
          }

          // Determine equivalent Isentropic Efficiencies for Main Turbine if given the Polytropic Efficiency
          if (eta_t < 0.0)
          {
              luis.isen_eta_from_poly_eta(luis,temp[5], pres[5], pres[10], Math.Abs(eta_t), false, ref error_code, ref eta_t_isen);
          }

          else
          {
              eta_t_isen = eta_t;
          }

          // Determine equivalent Isentropic Efficiencies for ReHeating Turbine if given the Polytropic Efficiency 
          if (eta_trh < 0.0)
          {
              luis.isen_eta_from_poly_eta(luis,temp[11], pres[11], pres[6], Math.Abs(eta_trh), false, ref error_code, ref eta_trh_isen);
          }

          else
          {
              eta_trh_isen = eta_trh;
          }

          // Determine the outlet state and specific work for the main compressor and turbine.

          // Main Compressor conditions
          luis.calculate_turbomachine_outlet(luis,temp[0], pres[0], pres[1], eta_mc_isen, true, ref error_code, ref enth[0], ref entr[0], ref dens[0], ref temp[1], ref enth[1], ref entr[1], ref dens[1], ref w_mc);

          // Main Turbine conditions
          luis.calculate_turbomachine_outlet(luis,temp[5], pres[5], pres[10], eta_t_isen, false, ref error_code, ref enth[5], ref entr[5], ref dens[5], ref temp[10], ref enth[10], ref entr[10], ref dens[10], ref w_t);

          // ReHeating Turbine conditions
          luis.calculate_turbomachine_outlet(luis,temp[11], pres[11], pres[6], eta_trh_isen, false, ref error_code, ref enth[11], ref entr[11], ref dens[11], ref temp[6], ref enth[6], ref entr[6], ref dens[6], ref w_trh);

          //Determine equivalent Isentropic Efficiencies for ReCompressor if given the Polytropic Efficiency

          if (recomp_frac >= 1.0d - 12)
          {
              // Convert Polytropic Efficiency to Isentropic Efficiency
              if (eta_rc < 0.0)
              {
                  luis.isen_eta_from_poly_eta(luis,temp[1], pres[8], pres[9], Math.Abs(eta_rc), true, ref error_code, ref eta_rc_isen);
              }

              else
              {
                  eta_rc_isen = eta_rc;
              }

              // ReCompressor conditions
              Double enth1, entr1, dens1, temp8, enth8, entr8, dens8;
              enth1 = enth[1];
              entr1 = entr[1];
              dens1 = dens[1];
              temp8 = temp[8];
              enth8 = enth[8];
              entr8 = entr[8];
              dens8 = dens[8];

              luis.calculate_turbomachine_outlet(luis,temp[1], pres[8], pres[9], eta_rc_isen, true, ref error_code, ref enth[1], ref entr[1], ref dens[1], ref temp[8], ref enth[8], ref entr[8], ref dens[8], ref w_rc);

              enth[1] = enth1;
              entr[1] = entr1;
              dens[1] = dens1;
              temp[8] = temp8;
              enth[8] = enth8;
              entr[8] = entr8;
              dens[8] = dens8;
          }

          else
          {
              w_rc = 0.0;
          }

          // Outer iteration loop: temp(8), checking against UA_HT.
          if (UA_HT < 1.0e-12)         // no high-temperature recuperator
          {
              T8_lower_bound = temp[6];  // no iteration necessary
              T8_upper_bound = temp[6];  // no iteration necessary
              temp[7] = temp[6];
              UA_HT_calc = 0.0;
              last_HT_residual = 0.0;
              last_T8_guess = temp[6];
          }

          else
          {
              T8_lower_bound = temp[1];                            // the absolute lowest temp(8) could be
              T8_upper_bound = temp[6];                            // the absolutely highest temp(8) could be
              temp[7] = (T8_lower_bound + T8_upper_bound) * 0.5;   // bisect bounds for first guess
              UA_HT_calc = -1.0;
              last_HT_residual = UA_HT;      // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT - 0.0
              last_T8_guess = temp[6];
          }

          // Outer iteration loop: temp(8),
          for (int T8_iter = 0; T8_iter < max_iter; T8_iter++)
          {
          Outer1:

              // Fully define state 8.
              //call CO2_TP(T=temp(8), P=pres(8), error_code=error_code, enth=enth(8), entr=entr(8), dens=dens(8))
              luis.working_fluid.FindStateWithTP(temp[7], pres[7]);
              enth[7] = luis.working_fluid.Enthalpy;
              entr[7] = luis.working_fluid.Entropy;
              dens[7] = luis.working_fluid.Density;

              //Inner iteration loop: temp(9), checking against UA_LT.
              if (UA_LT < 1.0e-12)       // no low-temperature recuperator
              {
                  T9_lower_bound = temp[7];  // no iteration necessary
                  T9_upper_bound = temp[7];  // no iteration necessary
                  temp[8] = temp[7];
                  UA_LT_calc = 0.0;
                  last_LT_residual = 0.0;
                  last_T9_guess = temp[7];
              }

              else
              {
                  T9_lower_bound = temp[1];    // the absolute lowest temp(9) could be
                  T9_upper_bound = temp[7];    // the absolutely highest temp(9) could be
                  temp[8] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                  UA_LT_calc = -1.0;
                  last_LT_residual = UA_LT;    // know a priori that with T9 = T8, UA_calc = 0 therefore residual is UA_LT - 0
                  last_T9_guess = temp[7];
              }

              //Inner iteration loop: temp(9)
              for (int T9_iter = 0; T9_iter < max_iter; T9_iter++)
              {
              Outer:

                  //Determine the outlet state of the recompressing compressor and its specific work.
                  if (recomp_frac >= 1.0e-12)
                  {
                      if (eta_rc < 0.0)  // recalculate isentropic efficiency of recompressing compressor (because T9 changes)
                          luis.isen_eta_from_poly_eta(luis,temp[8], pres[8], pres[9], Math.Abs(eta_rc), true, ref error_code, ref eta_rc_isen);
                      else
                      {
                          eta_rc_isen = eta_rc;
                      }
                      luis.calculate_turbomachine_outlet(luis,temp[8], pres[8], pres[9], eta_rc_isen, true, ref error_code, ref enth[8], ref entr[8], ref dens[8], ref temp[9], ref enth[9], ref entr[9], ref dens[9], ref w_rc);
                  }

                  else
                  {
                      w_rc = 0.0;  // the recompressing compressor does not exist

                      //call CO2_TP(T=temp(9), P=pres(9), error_code=error_code, enth=enth(9), entr=entr(9), dens=dens(9));  // fully define state 9
                      luis.working_fluid.FindStateWithTP(temp[8], pres[8]);
                      enth[8] = luis.working_fluid.Enthalpy;
                      entr[8] = luis.working_fluid.Entropy;
                      dens[8] = luis.working_fluid.Density;

                      temp[9] = temp[8];  // assume state 10 is the same as state 9
                      enth[9] = enth[8];
                      entr[9] = entr[8];
                      dens[9] = dens[8];
                  }

                  //Knowing the specific work of the the recompressing compressor, the required mass flow rate can be calculated.
                  m_dot_t = W_dot_net / (w_mc * (1.0 - recomp_frac) + w_rc * recomp_frac + w_t + w_trh);  // required mass flow rate through turbine

                  m_dot_rc = m_dot_t * recomp_frac;  // apply definition of recompression fraction
                  m_dot_mc = m_dot_t - m_dot_rc;     // mass balance

                  //Calculate the UA value of the low-temperature recuperator.
                  if (UA_LT < 1.0e-12)  // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                  {
                      Q_dot_LT = 0.0;
                  }
                  else
                  {
                      Q_dot_LT = m_dot_t * (enth[7] - enth[8]);
                  }

                  luis.calculate_hxr_UA(N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, temp[1], temp[7], pres[1], pres[2], pres[7], pres[8],
                      ref error_code, ref UA_LT_calc, ref min_DT_LT, ref T_h, ref T_c,ref Effec, ref P_h, ref P_c, ref UA_local);

                  if (error_code > 0)
                  {
                      if (error_code == 11)  // second-law violation in hxr, therefore temp(9) is too low
                      {
                          T9_lower_bound = temp[8];
                          temp[8] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for next guess
                          T9_iter++;
                          goto Outer;
                      }
                      else
                      {
                          MessageBox.Show("Error LUIS Coco Enriquez, Line 442, file Recompression_Brayton_Cycle.cs");
                          return;
                      }
                  }

                  //Check for convergence and adjust T9 appropriately.
                  UA_LT_residual = UA_LT - UA_LT_calc;

                  if (Math.Abs(UA_LT_residual) < 1.0e-12)
                  {
                      break;  // catches no LT case
                  }

                  secant_guess = temp[8] - UA_LT_residual * (last_T9_guess - temp[8]) / (last_LT_residual - UA_LT_residual);  // next guess predicted using secant method

                  if (UA_LT_residual < 0.0)  // UA_LT_calc is too big, temp(9) needs to be higher
                  {
                      if (Math.Abs(UA_LT_residual) / UA_LT < tol)
                      {
                          break;  // UA_LT converged (residual is negative)
                      }

                      T9_lower_bound = temp[8];

                  }

                  else  // UA_LT_calc is too small, temp(9) needs to be lower
                  {
                      if (UA_LT_residual / UA_LT < tol)
                      {
                          break;  // UA_LT converged
                      }

                      if (min_DT_LT < temperature_tolerance)
                      {
                          break;  // UA_calc is still too low but there isn't anywhere to go so it's ok (catches huge UA values)
                      }

                      T9_upper_bound = temp[8];

                  }

                  last_LT_residual = UA_LT_residual;  // reset last stored residual value
                  last_T9_guess = temp[8];  // reset last stored guess value

                  // Check if the secant method overshoots and fall back to bisection if it does.

                  if ((secant_guess <= T9_lower_bound) || (secant_guess >= T9_upper_bound) || (secant_guess != secant_guess))  // secant method overshot (or is NaN), use bisection
                  {
                      temp[8] = (T9_lower_bound + T9_upper_bound) * 0.5;
                  }

                  else
                  {
                      temp[8] = secant_guess;
                  }

              } //END for Inner iteration loop: temp(9)

              //Check that T9_loop converged.
              //if (T9_iter >= max_iter)
              //{
              //   error_code = 31;
              //   return;
              //}

              //State 3 can now be fully defined.
              enth[2] = enth[1] + Q_dot_LT / m_dot_mc;  // energy balance on cold stream of low-temp recuperator

              wmm = luis.working_fluid.MolecularWeight;
              luis.working_fluid.FindStatueWithPH(pres[2], enth[2] * wmm);
              //call CO2_PH(P=pres(3), H=enth(3), error_code=error_code, temp=temp(3), entr=entr(3), dens=dens(3))
              temp[2] = luis.working_fluid.Temperature;
              entr[2] = luis.working_fluid.Entropy;
              dens[2] = luis.working_fluid.Density;

              //Go through the mixing valve.
              if (recomp_frac >= 1.0e-12)
              {
                  enth[3] = (1.0 - recomp_frac) * enth[2] + recomp_frac * enth[9];  // conservation of energy (both sides divided by m_dot_t)

                  //call CO2_PH(P=pres(4), H=enth(4), error_code=error_code, temp=temp(4), entr=entr(4), dens=dens(4))
                  luis.working_fluid.FindStatueWithPH(pres[3], enth[3] * wmm);
                  temp[3] = luis.working_fluid.Temperature;
                  entr[3] = luis.working_fluid.Entropy;
                  dens[3] = luis.working_fluid.Density;
              }

              else  // no mixing valve, therefore state 4 is equal to state 3
              {
                  temp[3] = temp[2];
                  enth[3] = enth[2];
                  entr[3] = entr[2];
                  dens[3] = dens[2];
              }

              //Check for a second law violation at the outlet of the high-temp recuperator.
              if (temp[3] >= temp[7])  // temp(8) is not valid and it must be increased
              {
                  T8_lower_bound = temp[7];
                  temp[7] = (T8_lower_bound + T8_upper_bound) * 0.5;
                  T8_iter++;
                  goto Outer1;

              }

              //Calculate the UA value of the high-temperature recuperator.
              if (UA_HT < 1.0e-12) // no high-temp recuperator
              {
                  Q_dot_HT = 0.0;
              }

              else
              {
                  Q_dot_HT = m_dot_t * (enth[6] - enth[7]);
              }

              luis.calculate_hxr_UA(N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, temp[3], temp[6], pres[3], pres[4], pres[6], pres[7],
                  ref error_code, ref UA_HT_calc, ref min_DT_HT, ref T_h, ref T_c,ref Effec, ref P_h, ref P_c, ref UA_local);

              if (error_code > 0)
              {
                  if (error_code == 11) // second-law violation in hxr, therefore temp(8) is too low
                  {
                      T8_lower_bound = temp[7];
                      temp[7] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                      T8_iter++;
                      goto Outer1;
                  }
                  else
                  {
                      return;
                  }
              }

              // Check for convergence and adjust T8 appropriately.
              UA_HT_residual = UA_HT - UA_HT_calc;

              if (Math.Abs(UA_HT_residual) < 1.0e-12)
              {
                  break;  // catches no HT case
              }

              secant_guess = temp[7] - UA_HT_residual * (last_T8_guess - temp[7]) / (last_HT_residual - UA_HT_residual);  // next guess predicted using secant method

              if (UA_HT_residual < 0.0)  // UA_HT_calc is too big, temp(8) needs to be higher
              {
                  if (Math.Abs(UA_HT_residual) / UA_HT < tol)
                  {
                      break;  // UA_HT converged (residual is negative)
                  }
                  T8_lower_bound = temp[7];
              }

              else  // UA_HT_calc is too small, temp(8) needs to be lower
              {
                  if (UA_HT_residual / UA_HT < tol)
                  {
                      break;  // UA_HT converged
                  }
                  if (min_DT_HT < temperature_tolerance)
                  {
                      break;  // UA_calc is still too low but there isn't anywhere to go so it's ok (catches huge UA values)
                  }
                  T8_upper_bound = temp[7];
              }

              last_HT_residual = UA_HT_residual;  // reset last stored residual value
              last_T8_guess = temp[7];  // reset last stored guess value

              // Check if the secant method overshoots and fall back to bisection if it does.
              if ((secant_guess <= T8_lower_bound) || (secant_guess >= T8_upper_bound))  // secant method overshot, use bisection
              {
                  temp[7] = (T8_lower_bound + T8_upper_bound) * 0.5;
              }

              else
              {
                  temp[7] = secant_guess;
              }

          } // End for Outer iteration loop: temp(8),

          //Check that T8_loop converged.
          //if (T8_iter >= max_iter)
          //{
          //    error_code = 35;
          //    return;
          // }

          //State 5 can now be fully defined.
          enth[4] = enth[3] + Q_dot_HT / m_dot_t;  // energy balance on cold stream of high-temp recuperator

          //call CO2_PH(P=pres(5), H=enth(5), error_code=error_code, temp=temp(5), entr=entr(5), dens=dens(5))
          luis.working_fluid.FindStatueWithPH(pres[4], enth[4] * wmm);
          temp[4] = luis.working_fluid.Temperature;
          entr[4] = luis.working_fluid.Entropy;
          dens[4] = luis.working_fluid.Density;

          // Set cycle state point properties.
          recomp_cycle.temp = temp;
          recomp_cycle.pres = pres;
          recomp_cycle.enth = enth;
          recomp_cycle.entr = entr;
          recomp_cycle.dens = dens;

          // Calculate performance metrics for LTR low-temperature recuperator.
          recomp_cycle.LT.C_dot_hot = m_dot_t * (enth[7] - enth[8]) / (temp[7] - temp[8]);   // LT recuperator hot stream capacitance rate
          recomp_cycle.LT.C_dot_cold = m_dot_mc * (enth[2] - enth[1]) / (temp[2] - temp[1]);  // LT recuperator cold stream capacitance rate
          C_dot_min = Math.Min(recomp_cycle.LT.C_dot_hot, recomp_cycle.LT.C_dot_cold);
          Q_dot_max = C_dot_min * (temp[7] - temp[1]);
          recomp_cycle.LT.eff = Q_dot_LT / Q_dot_max;  // definition of effectiveness
          recomp_cycle.LT.UA_design = UA_LT_calc;
          recomp_cycle.LT.UA = UA_LT_calc;
          recomp_cycle.LT.DP_design1 = pres[1] - pres[2];
          recomp_cycle.LT.DP_design2 = pres[7] - pres[8];
          recomp_cycle.LT.m_dot_design[0] = m_dot_mc;
          recomp_cycle.LT.m_dot_design[1] = m_dot_t;
          recomp_cycle.LT.T_c_in = temp[1];
          recomp_cycle.LT.T_h_in = temp[7];
          recomp_cycle.LT.P_c_in = pres[1];
          recomp_cycle.LT.P_h_in = pres[7];
          recomp_cycle.LT.P_c_out = pres[2];
          recomp_cycle.LT.P_h_out = pres[8];
          recomp_cycle.LT.Q_dot = Q_dot_LT;
          recomp_cycle.LT.min_DT = min_DT_LT;
          recomp_cycle.LT.N_sub = N_sub_hxrs;

          //Calculate performance metrics for HTR high-temperature recuperator.
          recomp_cycle.HT.C_dot_hot = m_dot_t * (enth[6] - enth[7]) / (temp[6] - temp[7]);   // HT recuperator hot stream capacitance rate
          recomp_cycle.HT.C_dot_cold = m_dot_t * (enth[4] - enth[3]) / (temp[4] - temp[3]);  // HT recuperator cold stream capacitance rate
          C_dot_min = Math.Min(recomp_cycle.HT.C_dot_hot, recomp_cycle.HT.C_dot_cold);
          Q_dot_max = C_dot_min * (temp[6] - temp[3]);
          recomp_cycle.HT.eff = Q_dot_HT / Q_dot_max;  // definition of effectiveness
          recomp_cycle.HT.UA_design = UA_HT_calc;
          recomp_cycle.HT.UA = UA_HT_calc;
          recomp_cycle.HT.DP_design1 = pres[3] - pres[4];
          recomp_cycle.HT.DP_design2 = pres[6] - pres[7];
          recomp_cycle.HT.m_dot_design [0] = m_dot_t;
          recomp_cycle.HT.m_dot_design[1] = m_dot_t;
          recomp_cycle.HT.T_c_in = temp[3];
          recomp_cycle.HT.T_h_in = temp[6];
          recomp_cycle.HT.P_c_in = pres[3];
          recomp_cycle.HT.P_h_in = pres[6];
          recomp_cycle.HT.P_c_out = pres[4];
          recomp_cycle.HT.P_h_out = pres[7];
          recomp_cycle.HT.Q_dot = Q_dot_HT;
          recomp_cycle.HT.min_DT = min_DT_HT;
          recomp_cycle.HT.N_sub = N_sub_hxrs;

          // Set relevant values for other heat exchangers (PHX, RHX, PC).
          recomp_cycle.PHX.Q_dot = m_dot_t * (enth[5] - enth[4]);
          recomp_cycle.PHX.DP_design1 = pres[4] - pres[5];
          recomp_cycle.PHX.DP_design2 = 0.0;
          //recomp_cycle%PHX%m_dot_design = [m_dot_t, 0.0_dp]

          recomp_cycle.RHX.Q_dot = m_dot_t * (enth[11] - enth[10]);
          recomp_cycle.RHX.DP_design1 = pres[10] - pres[11];
          recomp_cycle.RHX.DP_design2 = 0.0;
          //recomp_cycle%RHX%m_dot_design = [m_dot_t, 0.0_dp]

          recomp_cycle.PC.Q_dot = m_dot_mc * (enth[8] - enth[0]);
          recomp_cycle.PC.DP_design1 = 0.0;
          recomp_cycle.PC.DP_design2 = pres[8] - pres[0];
          //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

          // Calculate cycle performance metrics.
          recomp_cycle.recomp_frac = recomp_frac;

          recomp_cycle.W_dot_net = w_mc * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_trh * m_dot_t;

          recomp_cycle.eta_thermal = recomp_cycle.W_dot_net / (recomp_cycle.PHX.Q_dot + recomp_cycle.RHX.Q_dot);

          recomp_cycle.m_dot_turbine = m_dot_t;
          recomp_cycle.conv_tol = tol;
      }

      // Main Compressor or Recompressor ONE-Stage detail design (Type Sandia Laboratory snl_Compressor.f90 or snl_compressor_tsf.f90)
      public void Main_Compressor_Detail_Design(core luis, Double P1, Double T1, Double P2, Double T2,Double m_dot_turbine,
                                                Double recomp_frac, ref Double D_rotor, ref Double N, ref Double eta,
                                                ref Boolean surge, ref Double phi_min, ref Double phi_max, ref double phi)
      {
         Double snl_phi_design = 0.02971;  // design-point flow coefficient for Sandia compressor (corresponds to max eta)
         Double snl_phi_min = 0.02;        // approximate surge limit for SNL compressor
         Double snl_phi_max = 0.05;        // approximate x-intercept for SNL compressor
          
         wmm = luis.working_fluid.MolecularWeight;
        
         // Local Variables
         int error_code=0;
         Double N_design, eta_design, w_tip_ratio, D_in, h_in, s_in, s_in_mol, T_out, P_out, h_out, dens1,enth1,entr1,enth2,dens2; 
         Double D_out, ssnd_out, h_s_out, psi_design, m_dot, w_i, U_tip, N_rad_s;

         luis.working_fluid.FindStateWithTP(T1, P1);
         enth1 = working_fluid.Enthalpy;
         entr1 = working_fluid.Entropy;
         dens1 = working_fluid.Density;

         // Create references to cycle state properties for clarity.
         D_in = dens1;
         h_in = enth1;
         s_in = entr1;

         luis.working_fluid.FindStateWithTP(T2, P2);
         enth2 = working_fluid.Enthalpy;
         dens2 = working_fluid.Density;

         h_out = enth2;
         D_out = dens2;

         //call CO2_TD(T=T_out, D=D_out, error_code=error_code, ssnd=ssnd_out)  ! speed of sound at outlet
         luis.working_fluid.FindStateWithTD(T2, D_out/wmm);
         ssnd_out = luis.working_fluid.speedofsound;

         if (error_code != 0) 
         {
            return;
         }
    
         //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  // outlet specific enthalpy after isentropic compression
         s_in_mol = s_in * wmm;
         luis.working_fluid.FindStatueWithPS(P2, s_in_mol);
         h_s_out = luis.working_fluid.Enthalpy;

         if (error_code != 0)
         {
            return;
         }

         // Calculate psi at the design-point phi using Horner's method 
         psi_design = ((((-498626.0 * snl_phi_design) + 53224.0) * snl_phi_design - 2505.0) * snl_phi_design + 54.6) * snl_phi_design + 0.04049;
         // from dimensionless modified head curve (at design-point, psi and modified psi are equal)
   
         // Determine required size and speed of compressor.
         m_dot = m_dot_turbine * (1.0 - recomp_frac);  // mass flow rate through compressor (kg/s)
         w_i = h_s_out - h_in;  // positive isentropic specific work of compressor (kJ/kg)
         U_tip = Math.Sqrt(1000.0 * w_i / psi_design);  // rearranging definition of head coefficient and converting kJ to J
         D_rotor = Math.Sqrt(m_dot / (snl_phi_design * D_in * U_tip));  // rearranging definition of flow coefficient
         N_rad_s = U_tip * 2.0 / D_rotor;   // shaft speed in rad/s
         N_design = N_rad_s * 9.549296590;  // shaft speed in rpm

         // Set other compressor variables.
         w_tip_ratio = U_tip / ssnd_out;     // ratio of the tip speed to local (comp outlet) speed of sound
         eta_design = w_i/(h_out - h_in);  // definition of isentropic efficiency
         eta = eta_design;
         phi = snl_phi_design;
         phi_min = snl_phi_min;
         phi_max = snl_phi_max;
         N = N_design;
         surge = false;         
      }

      public void ReCompressor_Detail_Design(core luis, Double P1, Double T1, Double P2, Double T2, Double m_dot_turbine,
                                                Double recomp_frac, ref Double D_rotor, ref Double N, ref Double eta,
                                                ref Boolean surge, ref Double phi_min, ref Double phi_max, ref double phi)
      {
          Double snl_phi_design = 0.02971;  // design-point flow coefficient for Sandia compressor (corresponds to max eta)
          Double snl_phi_min = 0.02;        // approximate surge limit for SNL compressor
          Double snl_phi_max = 0.05;        // approximate x-intercept for SNL compressor

          wmm = luis.working_fluid.MolecularWeight;

          // Local Variables
          int error_code = 0;
          Double N_design, eta_design, w_tip_ratio, D_in, h_in, s_in, s_in_mol, T_out, P_out, h_out, dens1, enth1, entr1, enth2, dens2;
          Double D_out, ssnd_out, h_s_out, psi_design, m_dot, w_i, U_tip, N_rad_s;

          luis.working_fluid.FindStateWithTP(T1, P1);
          enth1 = working_fluid.Enthalpy;
          entr1 = working_fluid.Entropy;
          dens1 = working_fluid.Density;

          // Create references to cycle state properties for clarity.
          D_in = dens1;
          h_in = enth1;
          s_in = entr1;

          luis.working_fluid.FindStateWithTP(T2, P2);
          enth2 = working_fluid.Enthalpy;
          dens2 = working_fluid.Density;

          h_out = enth2;
          D_out = dens2;

          //call CO2_TD(T=T_out, D=D_out, error_code=error_code, ssnd=ssnd_out)  ! speed of sound at outlet
          luis.working_fluid.FindStateWithTD(T2, D_out / wmm);
          ssnd_out = luis.working_fluid.speedofsound;

          if (error_code != 0)
          {
              return;
          }

          //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  // outlet specific enthalpy after isentropic compression
          s_in_mol = s_in * wmm;
          luis.working_fluid.FindStatueWithPS(P2, s_in_mol);
          h_s_out = luis.working_fluid.Enthalpy;

          if (error_code != 0)
          {
              return;
          }

          // Calculate psi at the design-point phi using Horner's method 
          psi_design = ((((-498626.0 * snl_phi_design) + 53224.0) * snl_phi_design - 2505.0) * snl_phi_design + 54.6) * snl_phi_design + 0.04049;
          // from dimensionless modified head curve (at design-point, psi and modified psi are equal)

          // Determine required size and speed of compressor.
          m_dot = m_dot_turbine * recomp_frac;  // mass flow rate through compressor (kg/s)
          w_i = h_s_out - h_in;  // positive isentropic specific work of compressor (kJ/kg)
          U_tip = Math.Sqrt(1000.0 * w_i / psi_design);  // rearranging definition of head coefficient and converting kJ to J
          D_rotor = Math.Sqrt(m_dot / (snl_phi_design * D_in * U_tip));  // rearranging definition of flow coefficient
          N_rad_s = U_tip * 2.0 / D_rotor;   // shaft speed in rad/s
          N_design = N_rad_s * 9.549296590;  // shaft speed in rpm

          // Set other compressor variables.
          w_tip_ratio = U_tip / ssnd_out;     // ratio of the tip speed to local (comp outlet) speed of sound
          eta_design = w_i / (h_out - h_in);  // definition of isentropic efficiency
          eta = eta_design;
          phi = snl_phi_design;
          phi_min = snl_phi_min;
          phi_max = snl_phi_max;
          N = N_design;
          surge = false;
      }
      // ReCompressor TWO-STAGES Design-Point detail design (Type Sandia National Laboratory, Snl_Compressor_tsr)
      public void ReCompressor_TWO_Stages_Detail_Design(core luis, Double P1, Double T1, Double P2, Double T2, Double m_dot_turbine,
                                                Double recomp_frac, ref Double D_rotor_1, ref Double D_rotor_2, ref Double N1, ref Double eta1,
                                                ref Boolean surge1, ref Double phi1_min, ref Double phi1_max, ref double phi1)
      {
          // Parameters
          Int64 max_iter = 100;
          Double tolerance = 1.0e-8;  // absolute tolerance for phi and stage efficiency
          
          Double snl_phi_design = 0.02971;  // design-point flow coefficient for Sandia compressor (corresponds to max eta)
          Double snl_phi_min = 0.02;        // approximate surge limit for SNL compressor
          Double snl_phi_max = 0.05;        // approximate x-intercept for SNL compressor

          wmm = luis.working_fluid.MolecularWeight;

          // Local Variables
          int error_code = 0;
          Double N_design, eta_design, w_tip_ratio, D_in, h_in, s_in, s_in_mol, T_out, P_out, h_out, dens1, enth1, entr1, enth2, dens2,entr1mol;
          Double D_out, ssnd_out, h2_s_out, h1_s_out, h_s_out, psi_design, m_dot, w1_i, w2_i, U_tip_1, U_tip_2, N_rad_s, w, eta_2_req;
          Double P_int, D_int, h_int, s_int, s_int_mol, ssnd_int;
          Double last_residual, last_P_int, lower_bound, upper_bound, eta_stage, ssd1, residual, secant_step, P_secant;

          luis.working_fluid.FindStateWithTP(T1, P1);
          enth1 = working_fluid.Enthalpy;
          entr1 = working_fluid.Entropy;
          dens1 = working_fluid.Density;

          // Create references to cycle state properties for clarity.
          D_in = dens1;
          h_in = enth1;
          s_in = entr1;

          luis.working_fluid.FindStateWithTP(T2, P2);
          enth2 = working_fluid.Enthalpy;
          dens2 = working_fluid.Density;
          h_out = enth2;
          D_out = dens2;

          //call CO2_TD(T=T_out, D=D_out, error_code=error_code, ssnd=ssnd_out)  ! speed of sound at outlet
          luis.working_fluid.FindStateWithTD(T2, D_out / wmm);
          ssnd_out = luis.working_fluid.speedofsound;

          if (error_code != 0)
          {
              return;
          }

          //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  // outlet specific enthalpy after isentropic compression
          s_in_mol = s_in * wmm;
          luis.working_fluid.FindStatueWithPS(P2, s_in_mol);
          h_s_out = luis.working_fluid.Enthalpy;

          if (error_code != 0)
          {
              return;
          }

          // overall isentropic efficiency
          eta_design = (h_s_out - h_in) / (h_out - h_in);  
          // mass flow rate through recompressor (kg/s)
          m_dot = m_dot_turbine * recomp_frac;  
          // Calculate psi at the design-point phi using Horner's method 
          psi_design = ((((-498626.0 * snl_phi_design) + 53224.0) * snl_phi_design - 2505.0) * snl_phi_design + 54.6) * snl_phi_design + 0.04049;
          // from dimensionless modified head curve (at design-point, psi and modified psi are equal)

          // Prepare intermediate pressure iteration loop.
          last_residual = 0.0;
          last_P_int = 1.0e12;  // ensures bisection will be used for first step
          lower_bound = P1 + 1e-6;
          upper_bound = P2 - 1e-6;
          P_int = (lower_bound + upper_bound) * 0.5;
          eta_stage = eta_design;  // first guess for stage efficiency
         
          for (int b=0;b<max_iter;b++)
          {
          // First stage
          //call CO2_PS(P=P_int, S=s_in, error_code=error_code, enth=h_s_out)  ! ideal outlet specific enthalpy after first stage
          entr1mol = entr1 * wmm;
          luis.working_fluid.FindStatueWithPS(P_int, entr1mol);
          h1_s_out = luis.working_fluid.Enthalpy;
      
          w1_i = h1_s_out - h_in;  // positive isentropic specific work of first stage
          U_tip_1 = Math.Sqrt(1000.0 * w1_i / psi_design);  // rearranging definition of head coefficient and converting kJ to J
          D_rotor_1 = Math.Sqrt(m_dot / (snl_phi_design * D_in * U_tip_1));  // rearranging definition of flow coefficient
          N_rad_s = U_tip_1 * 2.0 / D_rotor_1;   // shaft speed in rad/s
          N_design = N_rad_s * 9.549296590;  // shaft speed in rpm
          N1 = N_design;
          w = w1_i / eta_stage;  // actual first-stage work
          h_int = h_in + w;  // energy balance on first stage

          //call CO2_PH(P=P_int, H=h_int, error_code=error_code, dens=D_int, entr=s_int, ssnd=ssnd_int)
          luis.working_fluid.FindStatueWithPH(P_int, h_int * wmm);
          D_int = luis.working_fluid.Density;
          s_int = luis.working_fluid.Entropy;
          ssnd_int = luis.working_fluid.speedofsound;

          //call CO2_PS(P=P_out, S=s_int, error_code=error_code, enth=h_s_out)  ! ideal outlet specific enthalpy after second stage
          s_int_mol = s_int* wmm;
          luis.working_fluid.FindStatueWithPS(P2, s_int_mol);
          h2_s_out = luis.working_fluid.Enthalpy;

          w2_i = h2_s_out - h_int;  // positive isentropic specific work of second stage
          U_tip_2 = Math.Sqrt(1000.0 * w2_i / psi_design);  // rearranging definition of head coefficient and converting kJ to J
          D_rotor_2 = 2.0 * U_tip_2 / (N_design * 0.104719755);  // required second-stage diameter
          phi1 = m_dot / (D_int * U_tip_2 * D_rotor_2 * D_rotor_2);  // required flow coefficient
          eta_2_req = w2_i / (h_out - h_int);  // required second stage efficiency to achieve overall eta_design

          // Check convergence and update guesses.
          residual = snl_phi_design - phi1;

        if (residual < 0.0)  // P_int guess is too high
        {
          if ((-residual <= tolerance) && (Math.Abs(eta_stage-eta_2_req) <= tolerance))
            {
                return;
            }
            upper_bound = P_int;
        }
        
        else  // P_int guess is too low
        {  
          if ((residual <= tolerance) & (Math.Abs(eta_stage-eta_2_req) <= tolerance))
            {
                return;
            }
            lower_bound = P_int;
        }

           secant_step = -residual * (last_P_int - P_int) / (last_residual - residual);
           P_secant = P_int + secant_step;
           last_P_int = P_int;
           last_residual = residual;
           
           if ((P_secant <= lower_bound) || (P_secant >= upper_bound))  // secant method overshot
           {
               P_int = (lower_bound + upper_bound) * 0.5;
           }
           else if (Math.Abs(secant_step) > Math.Abs((upper_bound - lower_bound) * 0.5))  // take the smaller step to ensure convergence
           {
               P_int = (lower_bound + upper_bound) * 0.5;
           }
           else
           {
              P_int = P_secant;  // use secant guess
           }

           eta_stage = 0.5 * (eta_stage + eta_2_req);  // update guess for stage efficienc

           eta1 = eta_stage;
         }

      }

      // SNL Radial Turbine Design-Point detail design (Type Sandia Laboratory SNL_Radial_Turbine)
      public void snl_radial_turbine(core luis, Double P1, Double T1, Double P2, Double T2,Double m_dot_turbine, Double N_design,
                                     ref Double D_turbine, ref Double A_nozzle, ref Double eta, ref Double N,
                                     ref Double nu, ref Double w_tip_ratio)
      {
          Double enth1, entr1, entr1mol, dens1, enth2, entr2, dens2, ssnd1, h_s_out;
          Double w_i, C_s, U_tip, eta_design;

          Double nu_design = 0.7476;  // maximizes efficiency for SNL turbine efficiency curve

          wmm = luis.working_fluid.MolecularWeight;

          luis.working_fluid.FindStateWithTP(T1, P1);
          enth1 = luis.working_fluid.Enthalpy;
          entr1 = luis.working_fluid.Entropy;
          dens1 = luis.working_fluid.Density;

          luis.working_fluid.FindStateWithTP(T2, P2);
          enth2 = luis.working_fluid.Enthalpy;
          entr2 = luis.working_fluid.Entropy;
          dens2 = luis.working_fluid.Density;

          //call CO2_TD(T=T_in, D=D_in, error_code=error_code, ssnd=ssnd_in)  ! speed of sound at inlet
          luis.working_fluid.FindStateWithTD(T1, dens1 / wmm);
          ssnd1 = luis.working_fluid.speedofsound;

          //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  ! outlet specific enthalpy after isentropic expansion
          entr1mol = entr1 * wmm;
          luis.working_fluid.FindStatueWithPS(P2, entr1mol);
          h_s_out = luis.working_fluid.Enthalpy;

          // Determine necessary turbine parameters.
          nu = nu_design;
          w_i = enth1 - h_s_out;  // isentropic specific work of turbine (kJ/kg)
          C_s = Math.Sqrt(2.0 * w_i * 1000.0);  // spouting velocity in m/s
          U_tip = nu * C_s;  // rearrange definition of nu
          D_turbine = U_tip / (0.5 * N_design * 0.104719755);  // turbine diameter in m
          A_nozzle = (m_dot_turbine / (C_s * dens1));  // turbine effective nozzle area in m2

          // Set other turbine variables.
          w_tip_ratio = U_tip / ssnd1;  // ratio of the tip speed to local (turbine inlet) speed of sound
          eta_design = (enth1 - enth2)/w_i;  // definition of isentropic efficiency
          eta = eta_design;
          N = N_design;
      }

      // Radial Turbine Design-Point detail design (Type Radial_Turbine)
      public void RadialTurbine(core luis, Double P1, Double T1, Double P2, Double T2, Double m_dot_turbine, Double N_design,
                                     ref Double D_turbine, ref Double A_nozzle, ref Double eta, ref Double N, ref Double nu, ref Double w_tip_ratio)
      {
          // Determine the turbine rotor diameter, effective nozzle area, and design-point shaft
          // speed and store values in recomp_cycle%t.
          //
          // Arguments:
          //   recomp_cycle -- a RecompCycle object that defines the simple/recompression cycle at the design point
          //   error_trace -- an ErrorTrace object
          //
          // Notes:
          //   1) The value for recomp_cycle%t%N_design is required to be set.  If it is <= 0.0 then
          //      the value for recomp_cycle%mc%N_design is used (i.e., link the compressor and turbine
          //      shafts).  For this reason, turbine_sizing must be called after compressor_sizing if
          //      the shafts are to be linked.

          Double enth1, entr1, entr1mol, dens1, enth2, entr2, dens2, ssnd1, h_s_out;
          Double w_i, C_s, U_tip, eta_design;

          Double nu_design = 0.707;  // maximizes efficiency for SNL turbine efficiency curve

          wmm = luis.working_fluid.MolecularWeight;

          luis.working_fluid.FindStateWithTP(T1, P1);
          enth1 = luis.working_fluid.Enthalpy;
          entr1 = luis.working_fluid.Entropy;
          dens1 = luis.working_fluid.Density;

          luis.working_fluid.FindStateWithTP(T2, P2);
          enth2 = luis.working_fluid.Enthalpy;
          entr2 = luis.working_fluid.Entropy;
          dens2 = luis.working_fluid.Density;

          //call CO2_TD(T=T_in, D=D_in, error_code=error_code, ssnd=ssnd_in)  ! speed of sound at inlet
          luis.working_fluid.FindStateWithTD(T1, dens1 / wmm);
          ssnd1 = luis.working_fluid.speedofsound;

          //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  ! outlet specific enthalpy after isentropic expansion
          entr1mol = entr1 * wmm;
          luis.working_fluid.FindStatueWithPS(P2, entr1mol);
          h_s_out = luis.working_fluid.Enthalpy;

          // Determine necessary turbine parameters.
          nu = nu_design;
          w_i = enth1 - h_s_out;  // isentropic specific work of turbine (kJ/kg)
          C_s = Math.Sqrt(2.0 * w_i * 1000.0);  // spouting velocity in m/s
          U_tip = nu * C_s;  // rearrange definition of nu
          D_turbine = U_tip / (0.5 * N_design * 0.104719755);  // turbine diameter in m
          A_nozzle = (m_dot_turbine / (C_s * dens2));  // turbine effective nozzle area in m2

          // Set other turbine variables.
          w_tip_ratio = U_tip / ssnd1;  // ratio of the tip speed to local (turbine inlet) speed of sound
          eta_design = (enth1 - enth2)/w_i;  // definition of isentropic efficiency
          eta = eta_design;
          N = N_design;
      }

      // Radial Turbine Off-Design performance (Type Radial_Turbine)
      public void RadialTurbine_OffDesign(core luis,ref core.Turbine Turbine_Design, Double P1_offdesign, Double T1_offdesign,
                                   Double P2_offdesign,Double N_offdesign, ref Double error_code, ref Double m_dot_offdesign,
                                   ref Double T2_offdesign)
      {
          wmm = luis.working_fluid.MolecularWeight;

          Double enth1_offdesign;
          Double entr1_offdesign;
          Double entr1_offdesign_mol;
          Double dens1_offdesign;
          Double ssnd1_offdesign;

          Double enth2_s_offdesign;
          Double enth2_offdesign;
          Double dens2_offdesign;

          Double C_s;
          Double U_tip;
          Double eta_0;
          Double nu_offdesign;

          //call CO2_TP(T=T_in, P=P_in, error_code=error_code, enth=h_in, entr=s_in, ssnd=ssnd_in)  ! properties at inlet of turbine at Off-Design Conditions
          luis.working_fluid.FindStateWithTP(T1_offdesign, P1_offdesign);
          enth1_offdesign = luis.working_fluid.Enthalpy;
          entr1_offdesign = luis.working_fluid.Entropy;
          ssnd1_offdesign = luis.working_fluid.speedofsound;
          dens1_offdesign = luis.working_fluid.Density;

          //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  ! enthalpy at the turbine outlet if the expansion is isentropic
          entr1_offdesign_mol = entr1_offdesign * wmm;
          luis.working_fluid.FindStatueWithPS(P2_offdesign, entr1_offdesign_mol);
          enth2_s_offdesign = luis.working_fluid.Enthalpy;

          // Apply the radial turbine equations for efficiency.
          C_s = Math.Sqrt(2.0 * (enth1_offdesign - enth2_s_offdesign) * 1000.0);  // spouting velocity (m/s)
          U_tip = Turbine_Design.D_rotor * 0.5 * N_offdesign * 0.104719755;  // turbine tip speed (m/s)
          nu_offdesign = U_tip / C_s;  // ratio of tip speed to spouting velocity

          if (Turbine_Design.nu < 1.0)
          {
              eta_0 = 2.0 * nu_offdesign * Math.Sqrt(1.0 - (nu_offdesign*nu_offdesign));  // efficiency from Baines (1.0 at design point)
          }
          
          else
          {
             eta_0 = 0.0;  // catches nu values just over 1, which leads to sqrt of negative number
          }
             
          Turbine_Design.eta= eta_0 * Turbine_Design.eta_design;// actual turbine efficiency

          // Calculate the outlet state and allowable mass flow rate.
          enth2_offdesign = enth1_offdesign - Turbine_Design.eta * (enth1_offdesign - enth2_s_offdesign);  // enthalpy at turbine outlet
         
          //call CO2_PH(P=P_out, H=h_out, error_code=error_code, temp=T_out, dens=D_out)
          luis.working_fluid.FindStatueWithPH(P2_offdesign, enth2_offdesign * wmm);
          T2_offdesign = luis.working_fluid.Temperature;
          dens2_offdesign = luis.working_fluid.Density;

          m_dot_offdesign = C_s * Turbine_Design.A_nozzle * dens2_offdesign;  // mass flow through turbine (kg/s)
          Turbine_Design.w_tip_ratio = U_tip / ssnd1_offdesign;   // ratio of the tip speed to the local (turbine inlet) speed of sound
          Turbine_Design.N = N_offdesign;
      }

      // SNL Turbine Off-Design performance (Type SNL Turbine)
      public void SNL_Turbine_OffDesign(core luis, ref core.Turbine Turbine_Design, Double P1_offdesign, Double T1_offdesign,
                               Double P2_offdesign, Double N_offdesign, ref Double error_code, ref Double m_dot_offdesign,
                               ref Double T2_offdesign)
      {
          wmm = luis.working_fluid.MolecularWeight;

          Double enth1_offdesign;
          Double entr1_offdesign;
          Double entr1_offdesign_mol;
          Double dens1_offdesign;
          Double ssnd1_offdesign;

          Double enth2_s_offdesign;
          Double enth2_offdesign;
          Double dens2_offdesign;

          Double C_s;
          Double U_tip;
          Double eta_0;
          Double nu_offdesign;

          //call CO2_TP(T=T_in, P=P_in, error_code=error_code, enth=h_in, entr=s_in, ssnd=ssnd_in)  ! properties at inlet of turbine at Off-Design Conditions
          luis.working_fluid.FindStateWithTP(T1_offdesign, P1_offdesign);
          enth1_offdesign = luis.working_fluid.Enthalpy;
          entr1_offdesign = luis.working_fluid.Entropy;
          ssnd1_offdesign = luis.working_fluid.speedofsound;
          dens1_offdesign = luis.working_fluid.Density;

          //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  ! enthalpy at the turbine outlet if the expansion is isentropic
          entr1_offdesign_mol = entr1_offdesign * wmm;
          luis.working_fluid.FindStatueWithPS(P2_offdesign, entr1_offdesign_mol);
          enth2_s_offdesign = luis.working_fluid.Enthalpy;

          // Apply the radial turbine equations for efficiency.
          C_s = Math.Sqrt(2.0 * (enth1_offdesign - enth2_s_offdesign) * 1000.0);  // spouting velocity (m/s)
          U_tip = Turbine_Design.D_rotor * 0.5 * N_offdesign * 0.104719755;  // turbine tip speed (m/s)
          nu_offdesign = U_tip / C_s;  // ratio of tip speed to spouting velocity

          //eta_0 = 0.179921180_dp + 1.3567_dp*turb%nu + 1.3668_dp*turb%nu**2 - 3.0874_dp*turb%nu**3 + 1.0626_dp*turb%nu**4
          eta_0 = (((1.0626 * Turbine_Design.nu - 3.0874) * Turbine_Design.nu + 1.3668) * Turbine_Design.nu + 1.3567) * Turbine_Design.nu + 0.179921180;
          eta_0 = Math.Max(eta_0, 0.0);
          eta_0 = Math.Min(eta_0, 1.0);
          Turbine_Design.eta = eta_0 * Turbine_Design.eta_design;  // actual turbine efficiency

          // Calculate the outlet state and allowable mass flow rate.
          enth2_offdesign = enth1_offdesign - Turbine_Design.eta * (enth1_offdesign - enth2_s_offdesign);  // enthalpy at turbine outlet

          //call CO2_PH(P=P_out, H=h_out, error_code=error_code, temp=T_out, dens=D_out)
          luis.working_fluid.FindStatueWithPH(P2_offdesign, enth2_offdesign * wmm);
          T2_offdesign = luis.working_fluid.Temperature;
          dens2_offdesign = luis.working_fluid.Density;

          m_dot_offdesign = C_s * Turbine_Design.A_nozzle * dens1_offdesign;  // mass flow through turbine (kg/s)
          Turbine_Design.w_tip_ratio = U_tip / ssnd1_offdesign;   // ratio of the tip speed to the local (turbine inlet) speed of sound
          Turbine_Design.N = N_offdesign;
      }

      // Main Compressor Off-Design performance (Type snl_compressor.f90)
      public void SNL_Compressor_OffDesign(core luis, ref core.Compressor One_Stage_Compressor_Design, Double P1_offdesign, Double T1_offdesign,
                                Double P2_offdesign, Double N_offdesign, ref Double error_code, ref Double m_dot_offdesign,
                                ref Double T2_offdesign)
      {
          wmm = luis.working_fluid.MolecularWeight;

          Double enth1_offdesign;
          Double entr1_offdesign;
          Double dens1_offdesign;

          Double enth2_s_offdesign;
          Double enth2_offdesign;
          Double ssdn2_offdesign;

          Double U_tip;
          Double eta_0;
          Double phi;
          Double phi_star;
          Double psi_star;
          Double eta_star;
          Double psi;
          Double dh_s;
          Double dh;

          //call CO2_TP(T=T_in, P=P_in, error_code=error_code, enth=h_in, entr=s_in, ssnd=ssnd_in)  ! properties at inlet of turbine at Off-Design Conditions
          luis.working_fluid.FindStateWithTP(T1_offdesign, P1_offdesign);
          enth1_offdesign = luis.working_fluid.Enthalpy;
          entr1_offdesign = luis.working_fluid.Entropy;
          dens1_offdesign = luis.working_fluid.Density;

          // Calculate the modified flow and head coefficients and efficiency for the SNL compressor.
          U_tip = One_Stage_Compressor_Design.D_rotor * 0.5 * N_offdesign * 0.104719755;  // tip speed in m/s, Dyreby Thesis page 17 equation 3.2
          phi = m_dot_offdesign / (dens1_offdesign * U_tip * Math.Pow(One_Stage_Compressor_Design.D_rotor, 2));    // flow coefficient, Dyreby Thesis page 17 equation 3.1

          if (phi < One_Stage_Compressor_Design.phi_min) // the compressor is operating in the surge region
          {
            One_Stage_Compressor_Design.surge = true;
            phi = One_Stage_Compressor_Design.phi_min;  // reset phi to to its minimum value; this sets psi and eta to be fixed at the values at the surge limit
          }
          else
          {
              One_Stage_Compressor_Design.surge = false;
          }

          phi_star = phi * Math.Pow((N_offdesign / One_Stage_Compressor_Design.N_design),0.2);  // modified flow coefficient, page 21 Thesis Dyreby, equation (3.12)
          psi_star = ((((-498626.0 * phi_star) + 53224.0) * phi_star - 2505.0) * phi_star + 54.6) * phi_star + 0.04049;  // from dimensionless modified head curve, page 22, Fig.3.4 Thesis Dyreby
          eta_star = ((((-1.638e6 * phi_star) + 182725.0) * phi_star - 8089.0) * phi_star + 168.6) * phi_star - 0.7069;  // from dimensionless modified efficiency curve, page 22, Fig.3.4 Thesis Dyreby
          psi = psi_star / Math.Pow((One_Stage_Compressor_Design.N_design / N_offdesign), Math.Pow((20.0 * phi_star), 3)); // modified head coefficient, page 21 Thesis Dyreby, equation (3.13)
          eta_0 = (eta_star * 1.47528) / (Math.Pow((One_Stage_Compressor_Design.N_design / N_offdesign), Math.Pow((20.0 * phi_star), 5)));  // efficiency is normalized so it equals 1.0 at snl_phi_design, page 21 Thesis Dyreby, equation (3.14)
          One_Stage_Compressor_Design.eta = Math.Max((eta_0 * One_Stage_Compressor_Design.eta_design), 0.0);  // the actual compressor efficiency, not allowed to go negative

          // Calculate the compressor outlet state.
          dh_s = psi * Math.Pow(U_tip,2) * 0.001;  // ideal enthalpy rise in compressor, from definition of head coefficient (kJ/kg), page 17 Thesis Dyreby, equation (3.3)
          dh = dh_s / One_Stage_Compressor_Design.eta;            // actual enthalpy rise in compressor
          enth2_s_offdesign = enth1_offdesign + dh_s;           // ideal enthalpy at compressor outlet
          enth2_offdesign = enth1_offdesign + dh;               // actual enthalpy at compressor outlet

          //call CO2_HS(H=h_s_out, S=s_in, error_code=error_code, pres=P_out)  ! get the compressor outlet pressure
          luis.working_fluid.FindStatueWithHS(enth2_s_offdesign*wmm, entr1_offdesign*wmm);
          P2_offdesign = luis.working_fluid.Pressure;

          //call CO2_PH(P=P_out, H=h_out, error_code=error_code, temp=T_out, ssnd=ssnd_out)  ! determines compressor outlet temperature and speed of sound
          luis.working_fluid.FindStatueWithPH(P2_offdesign, enth2_offdesign * wmm);
          T2_offdesign = luis.working_fluid.Temperature;
          ssdn2_offdesign = luis.working_fluid.speedofsound;

          // Set a few compressor variables.
          One_Stage_Compressor_Design.phi = phi;
          One_Stage_Compressor_Design.w_tip_ratio = U_tip / ssdn2_offdesign;     // ratio of the tip speed to local (comp outlet) speed of sound
      }

      // ReCompressor Off-Design performance (Type snl_compressor.f90)
      public void SNL_ReCompressor_OffDesign(core luis, ref core.Compressor One_Stage_Compressor_Design, Double P1_offdesign, Double T1_offdesign,
                                Double P2_offdesign, Double N_offdesign, ref Double error_code, ref Double m_dot_offdesign,
                                ref Double T2_offdesign)
      {
          wmm = luis.working_fluid.MolecularWeight;

          Double enth1_offdesign;
          Double entr1_offdesign;
          Double dens1_offdesign;
          Double ssdn1_offdesign;

          Double enth2_s_offdesign;
          Double enth2_offdesign;
          Double ssdn2_offdesign;

          Double U_tip;
          Double eta_0;
          Double phi;
          Double phi_star;
          Double psi_star;
          Double eta_star;
          Double psi;
          Double dh_s;
          Double dh;
          Double alpha;
          Double dh_s_calc;
          Double residual;
          Boolean first_pass;
          Int64 max_iterations = 1000;
          Double tolerance = 1.0e-33;  // absolute tolerance for phi
          Double last_phi = 0;
          Double next_phi;
          Double last_residual = 0;


          //call CO2_TP(T=T_in, P=P_in, error_code=error_code, enth=h_in, entr=s_in, ssnd=ssnd_in)  ! properties at inlet of turbine at Off-Design Conditions
          luis.working_fluid.FindStateWithTP(T1_offdesign, P1_offdesign);
          entr1_offdesign = luis.working_fluid.Entropy;
          enth1_offdesign = luis.working_fluid.Enthalpy;
          dens1_offdesign = luis.working_fluid.Density;
          ssdn1_offdesign = luis.working_fluid.speedofsound;

          //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  ! outlet enthalpy if compression/expansion is isentropic
          luis.working_fluid.FindStatueWithPS(P2_offdesign, entr1_offdesign * wmm);
          enth2_s_offdesign = luis.working_fluid.Enthalpy;

          dh_s = enth2_s_offdesign - enth1_offdesign;  // ideal enthalpy rise in compressor

          // Iterate on phi.
          alpha = m_dot_offdesign / (dens1_offdesign * Math.Pow(One_Stage_Compressor_Design.D_rotor, 2));  // used to reduce operation count in loop
          phi = One_Stage_Compressor_Design.phi_design;  // start with design-point value
          first_pass = true;

          for (int iter = 1; iter <= max_iterations; iter++)
          {
              U_tip = alpha / phi;  // flow coefficient rearranged (with alpha substitution)
              N_offdesign = (U_tip * 2.0 / One_Stage_Compressor_Design.D_rotor) * 9.549296590;  // shaft speed in rpm
              phi_star = phi * Math.Pow((N_offdesign / One_Stage_Compressor_Design.N_design), 0.2);  // modified flow coefficient
              psi_star = ((((-498626.0 * phi_star) + 53224.0) * phi_star - 2505.0) * phi_star + 54.6) * phi_star + 0.04049;  // from dimensionless modified head curve
              psi = psi_star / Math.Pow((One_Stage_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 3)));
              dh_s_calc = psi * Math.Pow(U_tip, 2) * 0.001;  // calculated ideal enthalpy rise in compressor, from definition of head coefficient (kJ/kg)
              residual = dh_s - dh_s_calc;

              if (Math.Abs(residual) <= tolerance)  // converged sufficiently
              {
                  MessageBox.Show("Please not introduce the EXACT solution from Design-Point in the Off Design-Point.");
                  return;
                  //goto outer;
              }

              if (first_pass == true)
              {
                  next_phi = phi * 1.0001; // take a small step
                  first_pass = false;
              }

              else
              {
                  next_phi = phi - residual * (last_phi - phi) / (last_residual - residual);  // next guess predicted using secant method
              }

              last_phi = phi;
              last_residual = residual;
              phi = next_phi;

              // Check for convergence.
              if (iter >= max_iterations) // did not converge
              {
                  MessageBox.Show("Please not introduce the EXACT solution from Design-Point in the Off Design-Point.");
                  return;
              }

              // Calculate efficiency and outlet state.
              eta_star = ((((-1.638e6 * phi_star) + 182725.0) * phi_star - 8089.0) * phi_star + 168.6) * phi_star - 0.7069;  // from dimensionless modified efficiency curve
              eta_0 = eta_star * 1.47528 / (Math.Pow((One_Stage_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 5))));  // efficiency is normalized so it equals 1.0 at snl_phi_design
              One_Stage_Compressor_Design.eta = Math.Max(eta_0 * One_Stage_Compressor_Design.eta_design, 0.0);  // the actual compressor efficiency, not allowed to go negative
              dh = dh_s / One_Stage_Compressor_Design.eta;              // actual enthalpy rise in compressor
              enth2_offdesign = enth1_offdesign + dh;                 // actual enthalpy at compressor outlet

              //call CO2_PH(P=P_out, H=h_out, error_code=error_code, temp=T_out, ssnd=ssnd_out)  ! determines compressor outlet temperature and speed of sound
              luis.working_fluid.FindStatueWithPH(P2_offdesign, enth2_offdesign * wmm);
              T2_offdesign = luis.working_fluid.Temperature;
              ssdn2_offdesign = luis.working_fluid.speedofsound;

              One_Stage_Compressor_Design.N = N_offdesign;
              One_Stage_Compressor_Design.phi = phi;
              One_Stage_Compressor_Design.w_tip_ratio = U_tip / ssdn2_offdesign; // ratio of the tip speed to local (comp outlet) speed of sound
          }

            //outer:

              //return;
     
      }

      // Main Compressor Off-Design performance (Type snl_compressor_tsr.f90)
      public void SNL_ReCompressor_TWO_Stages_OffDesign(core luis, ref core.Compressor TWO_Stages_Compressor_Design, Double P1_offdesign, Double T1_offdesign,
                                Double P2_offdesign, Double N_offdesign, ref Double error_code, ref Double m_dot_offdesign,
                                ref Double T2_offdesign)
      {
          wmm = luis.working_fluid.MolecularWeight;

          Int64 max_iter = 100;
          Double rel_tol = 1.0e-9;   // relative tolerance for pressure
          Double phi_1;
          Boolean first_pass;
          Double next_phi;
          Double last_phi_1=0;
          Double last_residual=0;

          Double entr1_offdesign,enth1_offdesign,dens1_offdesign,ssdn1_offdesign,ssdn2_offdesign;
          Double U_tip_1, eta_0, eta_stage_1, P_out_calc;
          Double phi_star, psi_star, psi, eta_star, P_int, D_int, s_int, ssnd_int;
          Double dh_s, dh, h_int, h_s_out, h_out, U_tip_2, phi_2, eta_stage_2, residual;

          //call CO2_TP(T=T_in, P=P_in, error_code=error_code, dens=rho_in, enth=h_in, entr=s_in)  ! fully define the inlet state of the compressor
          luis.working_fluid.FindStateWithTP(T1_offdesign, P1_offdesign);
          entr1_offdesign = luis.working_fluid.Entropy;
          enth1_offdesign = luis.working_fluid.Enthalpy;
          dens1_offdesign = luis.working_fluid.Density;
          ssdn1_offdesign = luis.working_fluid.speedofsound;

          // Iterate on first-stage phi.
          phi_1 = TWO_Stages_Compressor_Design.phi_design;  // start with design-point value
          first_pass = true;

          for (int iter = 1; iter <= max_iter; iter++)
          {
              // First stage - dh_s and eta_stage_1.
              U_tip_1 = m_dot_offdesign / (phi_1 * dens1_offdesign * Math.Pow(TWO_Stages_Compressor_Design.D_rotor, 2));  // flow coefficient rearranged
              N_offdesign = (U_tip_1 * 2.0 / TWO_Stages_Compressor_Design.D_rotor) * 9.549296590;  // shaft speed in rpm
              phi_star = phi_1 * (Math.Pow((N_offdesign / TWO_Stages_Compressor_Design.N_design), 0.2));  // modified flow coefficient
              psi_star = ((((-498626.0 * phi_star) + 53224.0) * phi_star - 2505.0) * phi_star + 54.6) * phi_star + 0.04049;  // from dimensionless modified head curve
              psi = psi_star / (Math.Pow((TWO_Stages_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 3))));
              dh_s = psi * Math.Pow(U_tip_1, 2) * 0.001;  // calculated ideal enthalpy rise in first stage of compressor, from definition of head coefficient (kJ/kg)
              eta_star = ((((-1.638e6 * phi_star) + 182725.0) * phi_star - 8089.0) * phi_star + 168.6) * phi_star - 0.7069;  // from dimensionless modified efficiency curve
              eta_0 = eta_star * 1.47528 / (Math.Pow((TWO_Stages_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 5))));  // stage efficiency is normalized so it equals 1.0 at snl_phi_design
              eta_stage_1 = Math.Max(eta_0 * TWO_Stages_Compressor_Design.eta_design, 0.0);  // the actual stage efficiency, not allowed to go negative

              // Calculate first-stage outlet (second-stage inlet) state.
              dh = dh_s / eta_stage_1;  // actual enthalpy rise in first stage
              h_s_out = enth1_offdesign + dh_s;    // ideal enthalpy between stages
              h_int = enth1_offdesign + dh;          // actual enthalpy between stages

              //call CO2_HS(H=h_s_out, S=s_in, error_code=error_code, pres=P_int)  ! get the first-stage outlet pressure (second-stage inlet pressure)
              luis.working_fluid.FindStatueWithHS(h_s_out * wmm, entr1_offdesign * wmm);
              P_int = luis.working_fluid.Pressure;

              //call CO2_PH(P=P_int, H=h_int, error_code=error_code, dens=D_int, entr=s_int, ssnd=ssnd_int)  ! get second-stage inlet properties
              luis.working_fluid.FindStatueWithPH(P_int, h_int * wmm);
              D_int = luis.working_fluid.Density;
              s_int = luis.working_fluid.Entropy;
              ssnd_int = luis.working_fluid.speedofsound;

              // Second stage - dh_s and eta_stage_2.
              U_tip_2 = TWO_Stages_Compressor_Design.D_rotor_2 * 0.5 * N_offdesign * 0.104719755;  // second-stage tip speed in m/s
              phi_2 = m_dot_offdesign / (D_int * U_tip_2 * Math.Pow(TWO_Stages_Compressor_Design.D_rotor_2, 2));   // second-stage flow coefficient
              phi_star = phi_2 * (Math.Pow((N_offdesign / TWO_Stages_Compressor_Design.N_design), 0.2));  // modified flow coefficient
              psi_star = ((((-498626.0 * phi_star) + 53224.0) * phi_star - 2505.0) * phi_star + 54.6) * phi_star + 0.04049;  //from dimensionless modified head curve
              psi = psi_star / (Math.Pow((TWO_Stages_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 3))));
              dh_s = psi * Math.Pow(U_tip_2, 2) * 0.001;  // calculated ideal enthalpy rise in second stage of compressor, from definition of head coefficient (kJ/kg)
              eta_star = ((((-1.638e6 * phi_star) + 182725.0) * phi_star - 8089.0) * phi_star + 168.6) * phi_star - 0.7069;  // from dimensionless modified efficiency curve
              eta_0 = eta_star * 1.47528 / (Math.Pow((TWO_Stages_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 5))));  // stage efficiency is normalized so it equals 1.0 at snl_phi_design
              eta_stage_2 = Math.Max(eta_0 * TWO_Stages_Compressor_Design.eta_design, 0.0);  // the actual stage efficiency, not allowed to go negative

              // Calculate second-stage outlet state.
              dh = dh_s / eta_stage_2;  // actual enthalpy rise in second stage
              h_s_out = h_int + dh_s;   // ideal enthalpy at compressor outlet
              h_out = h_int + dh;       // actual enthalpy at compressor outlet

              //call CO2_HS(H=h_s_out, S=s_int, error_code=error_code, pres=P_out_calc)  ! get the calculated compressor outlet pressure
              luis.working_fluid.FindStatueWithHS(h_s_out * wmm, s_int * wmm);
              P_out_calc = luis.working_fluid.Pressure;

              // Check for convergence and adjust phi_1 guess.
              residual = P2_offdesign - P_out_calc;
              if (Math.Abs(residual) / P2_offdesign <= rel_tol)
              {
                  return;  // converged sufficiently
              }

              if (first_pass==true)
              {
                 next_phi = phi_1 * 1.0001;  // take a small step
                 first_pass = false;
              }

              else
              {
                 next_phi = phi_1 - residual * (last_phi_1 - phi_1) / (last_residual - residual);  //next guess predicted using secant method
              }

              last_phi_1 = phi_1;
              last_residual = residual;
              phi_1 = next_phi;

              // Check for convergence.
              if (iter >= max_iter)   // did not converge
              {
                return;
              }
    
              // Determine outlet temperature and speed of sound.
              //call CO2_PH(P=P_out_calc, H=h_out, error_code=error_code, temp=T_out, ssnd=ssnd_out)
              luis.working_fluid.FindStatueWithPH(P_out_calc, h_out* wmm);
              T2_offdesign = luis.working_fluid.Temperature;
              ssdn2_offdesign = luis.working_fluid.speedofsound;

              //call CO2_PS(P=P_out_calc, S=s_in, error_code=error_code, enth=h_s_out)  ! outlet specific enthalpy after isentropic compression
              luis.working_fluid.FindStatueWithPS(P_out_calc, entr1_offdesign * wmm);
              h_s_out = luis.working_fluid.Enthalpy;

              // Set relevant recompressor variables.
              TWO_Stages_Compressor_Design.N = N_offdesign;
              TWO_Stages_Compressor_Design.eta = (h_s_out - enth1_offdesign) / (h_out - enth1_offdesign);  // use overall isentropic efficiency
              TWO_Stages_Compressor_Design.phi = phi_1;
              TWO_Stages_Compressor_Design.phi_2 = phi_2;
              TWO_Stages_Compressor_Design.w_tip_ratio = Math.Max(U_tip_1 / ssnd_int, U_tip_2 / ssdn2_offdesign);  // store maximum ratio
              TWO_Stages_Compressor_Design.surge = ((phi_1 < TWO_Stages_Compressor_Design.phi_min) || (phi_2 < TWO_Stages_Compressor_Design.phi_min));
          }
      }

    public Double off_Design_Point(core luis, ref core.RecompCycle recomp_cycle,Double T_mc_in, Double T_t_in, Double T_trh_in,
                                     Double P_trh_in, Double P_mc_in, Double recomp_frac, Double N_mc, Double N_t, Double N_sub_hxrs,
                                     Double tol, Double error_code) 
      {
          //Parameters
          Boolean surge_allowed = true;
          Boolean supersonic_tip_speed_allowed = true;

          // Local Variables
          Int64 m_dot_iter, T9_iter, T8_iter, index;
          m_dot_iter = 1;
          Double rho_in, C_dot_min, Q_dot_max, m_dot_residual, partial_phi, tip_speed;
          Double m_dot_lower_bound, m_dot_upper_bound, m_dot_mc_guess, m_dot_mc_max;
          Double last_m_dot_guess=0;
          Double last_m_dot_residual=0;
          Double m_dot_t_allowed=0;
          Double T9_lower_bound, T9_upper_bound, T8_lower_bound, T8_upper_bound, last_LT_residual, last_T9_guess;
          Double last_HT_residual, last_T8_guess, secant_guess;
          Double m_dot_t, m_dot_mc, m_dot_rc, UA_LT, UA_HT, w_mc, w_rc, w_t, w_trh;
          Double min_DT_LT, min_DT_HT, UA_LT_calc, UA_HT_calc, Q_dot_LT, Q_dot_HT, UA_HT_residual, UA_LT_residual;
          m_dot_mc = 0;
          
          Double [] temp = new Double[12];
          Double [] pres = new Double[12];
          Double [] enth = new Double[12];
          Double [] entr = new Double[12];
          Double [] dens = new Double[12];

          Double [] DP_LT = new Double[2];
          Double [] DP_HT = new Double[2];
          Double [] DP_PC = new Double[2];
          Double [] DP_PHX = new Double[2];
          Double [] DP_RHX = new Double[2];
          
          Double [] m_dots = new Double[2];

          Boolean first_pass=false;

          // Parameters
          Int64 max_iter;
          Double temperature_tolerance;  // temperature differences below this are considered zero

          max_iter = 500;
          temperature_tolerance = 1.0e-6;

          // Initialize a few variables.
          temp[0] = T_mc_in;
          pres[0] = P_mc_in;
          temp[5] = T_t_in;
          temp[11] = T_trh_in;
          pres[11] = P_trh_in;
          recomp_cycle.mc.N = N_mc;
          recomp_cycle.t.N = N_t;
          recomp_cycle.conv_tol = tol;

          // Prepare the mass flow rate iteration loop.
          //call CO2_TP(T=temp(1), P=pres(1), error_code=error_code, dens=rho_in)

          luis.working_fluid.FindStateWithTP(temp[0],pres[0]);
          dens[0] = luis.working_fluid.Density;

          tip_speed = recomp_cycle.mc.D_rotor * 0.5 * N_mc * 0.10471975512;  // main compressor tip speed in m/s
          partial_phi = dens[0] * Math.Pow(recomp_cycle.mc.D_rotor,2)* tip_speed;           // reduces computation on next two lines
          m_dot_mc_guess = recomp_cycle.mc.phi_design * partial_phi;               // mass flow rate corresponding to design-point phi in main compressor
          m_dot_mc_max = recomp_cycle.mc.phi_max * partial_phi * 1.2;           // largest possible mass flow rate in main compressor (with safety factor)
          m_dot_t = m_dot_mc_guess / (1.0 - recomp_frac);                       // first guess for mass flow rate through turbine
          m_dot_upper_bound = m_dot_mc_max / (1.0 - recomp_frac);               // largest possible mass flow rate through turbine
          m_dot_lower_bound = 0.0;                                             // this lower bound allows for surge (checked after iteration)
          first_pass = true;

          for (int j = 1; j < max_iter; j++)
          {            
             m_dot_rc = m_dot_t * recomp_frac;  // mass flow rate through recompressing compressor
             m_dot_mc = m_dot_t - m_dot_rc;     // mass flow rate through compressor

             // Calculate the pressure rise through the main compressor.
             luis.SNL_Compressor_OffDesign(luis,ref recomp_cycle.mc,pres[0],temp[0],pres[1],N_mc,ref error_code,
                                           ref m_dot_mc,ref temp[1]);

             if (error_code == 1)  // m_dot is too high because the given shaft speed is not possible
             {
                 m_dot_upper_bound = m_dot_t;
                 m_dot_t = (m_dot_lower_bound + m_dot_upper_bound) * 0.5;  // use bisection for new mass flow rate guess
                 break;
             }

             else if (error_code == 2)  // m_dot is too low because P_out is (likely) above properties limits
             {    
                 m_dot_lower_bound = m_dot_t;
                 m_dot_t = (m_dot_lower_bound + m_dot_upper_bound) * 0.5;  // use bisection for new mass flow rate guess
                 break;
             }
            
             else if (error_code != 0)  // unexpected error
             {
               MessageBox.Show("Error en Off-Design function");
               return 0;
             }                     

           // Calculate scaled pressure drops through heat exchangers.
           m_dots[0] = m_dot_mc;
           m_dots[1] = m_dot_t;
           DP_LT = luis.hxr_pressure_drops(ref recomp_cycle.LT, m_dots);
           m_dots[0] = m_dot_t;
           m_dots[1] = m_dot_t;
           DP_HT = hxr_pressure_drops(ref recomp_cycle.HT, m_dots);
           m_dots[0] = m_dot_t;
           m_dots[1] = 0;
           DP_PHX = hxr_pressure_drops(ref recomp_cycle.PHX, m_dots);
           m_dots[0] = m_dot_t;
           m_dots[1] = 0;
           DP_RHX = hxr_pressure_drops(ref recomp_cycle.RHX, m_dots);
           m_dots[0] = 0;
           m_dots[1] = m_dot_mc;
           DP_RHX = hxr_pressure_drops(ref recomp_cycle.PC, m_dots); 

           // Apply pressure drops to heat exchangers, fully defining the pressures at all states.
           pres[2]  = pres[1] - DP_LT[0];   // LT recuperator [cold stream]
           pres[3]  = pres[2];              // assume no pressure drop in mixing valve
           pres[9] = pres[2];              // assume no pressure drop in mixing valve
           pres[4]  = pres[3] - DP_HT[0];   // HT recuperator [cold stream]
           pres[5]  = pres[4] - DP_PHX[0];  // PHX
           pres[10]  = pres[11] + DP_RHX[0]; //RHX
           pres[8]  = pres[0] + DP_PC[1];   // precooler
           pres[7]  = pres[8] + DP_LT[1];   // LT recuperator [hot stream]
           pres[6]  = pres[7] + DP_HT[1];   // HT recuperator [hot stream]

            // Calculate the mass flow rate through the Main turbine.
        //call off_design_turbine(       &
        //    turb = recomp_cycle%t,     &
        //    T_in = temp(6),            &
        //    P_in = pres(6),            &
        //    P_out = pres(11),           &
        //    N = N_t,                   &
        //    error_trace = error_trace, &
        //    m_dot = m_dot_t_allowed,   &
        //    T_out = temp(11)            &
        //    )

           luis.SNL_Turbine_OffDesign(luis, ref recomp_cycle.t, pres[5], temp[5], pres[10], N_t, ref error_code,
                                      ref m_dot_t_allowed, ref temp[10]);

          // Determine the mass flow rate residual and prepare the next iteration.
          m_dot_residual = m_dot_t - m_dot_t_allowed;
          secant_guess = m_dot_t - m_dot_residual * (last_m_dot_guess - m_dot_t) / (last_m_dot_residual - m_dot_residual);  // next guess predicted using secant method
        
          if (m_dot_residual > 0.0)  // pressure rise is too small, so m_dot_t is too big
          {
            if (m_dot_residual / m_dot_t < tol)
            { 
                break;  // residual is positive; check for convergence
            }
                m_dot_upper_bound = m_dot_t;   // reset upper bound
          }

         else  // pressure rise is too high, so m_dot_t is too small
         {
            if (-m_dot_residual / m_dot_t < tol)
            {
                break; // residual is negative; check for convergence
            }
             
            m_dot_lower_bound = m_dot_t;   // reset lower bound
          }
         
            last_m_dot_residual = m_dot_residual;                                // reset last stored residual value
            last_m_dot_guess = m_dot_t;                                   // reset last stored guess value

        // Check if the secant method overshoots and fall back to bisection if it does.
        if (first_pass)
        {
            m_dot_t = (m_dot_upper_bound + m_dot_lower_bound) * 0.5;
            first_pass =false;
        }
        else if ((secant_guess < m_dot_lower_bound) || (secant_guess > m_dot_upper_bound))  // secant method overshot, use bisection
        {
            m_dot_t = (m_dot_upper_bound + m_dot_lower_bound) * 0.5;
        }
        else
        {    
              m_dot_t = secant_guess;
        }

        m_dot_iter = m_dot_iter + 1;

      } // End m_dot_loop

         // Check for convergence.
        if (m_dot_iter >= max_iter)
        {
          error_code = 42;
          MessageBox.Show("Error in Off-Design function, above max_iter");
          return 0;
        }

        luis.SNL_Turbine_OffDesign(luis, ref recomp_cycle.t, pres[5], temp[5], pres[10], N_t, ref error_code,
                                      ref m_dot_t_allowed, ref temp[10]);

        luis.SNL_Turbine_OffDesign(luis, ref recomp_cycle.t_rh, pres[11], temp[11], pres[6], N_t, ref error_code,
                                    ref m_dot_t_allowed, ref temp[6]);

        luis.working_fluid.FindStateWithTP(temp[0], pres[0]);
        enth[0] = luis.working_fluid.Enthalpy;
        entr[0] = luis.working_fluid.Entropy;
        dens[0] = luis.working_fluid.Density;

        luis.working_fluid.FindStateWithTP(temp[1], pres[1]);
        enth[1] = luis.working_fluid.Enthalpy;
        entr[1] = luis.working_fluid.Entropy;
        dens[1] = luis.working_fluid.Density;

        luis.working_fluid.FindStateWithTP(temp[5], pres[5]);
        enth[5] = luis.working_fluid.Enthalpy;
        entr[5] = luis.working_fluid.Entropy;
        dens[5] = luis.working_fluid.Density;

        luis.working_fluid.FindStateWithTP(temp[10], pres[10]);
        enth[10] = luis.working_fluid.Enthalpy;
        entr[10] = luis.working_fluid.Entropy;
        dens[10] = luis.working_fluid.Density;

        luis.working_fluid.FindStateWithTP(temp[11], pres[11]);
        enth[11] = luis.working_fluid.Enthalpy;
        entr[11] = luis.working_fluid.Entropy;
        dens[11] = luis.working_fluid.Density;

        luis.working_fluid.FindStateWithTP(temp[6], pres[6]);
        enth[6] = luis.working_fluid.Enthalpy;
        entr[6] = luis.working_fluid.Entropy;
        dens[6] = luis.working_fluid.Density;

        // Get the recuperator conductances corresponding to the converged mass flow rates.
        m_dots[0] = m_dot_mc;
        m_dots[1] = m_dot_t;
        UA_LT = luis.hxr_conductance(ref recomp_cycle.LT, m_dots);
        m_dots[0] = m_dot_t;
        m_dots[1] = m_dot_t;
        UA_HT = luis.hxr_conductance(ref recomp_cycle.HT, m_dots); 
      

        // Outer iteration loop: temp(8), checking against UA_HT.
        if (UA_HT < 1.0e-12)  // no high-temperature recuperator
        {
            T8_lower_bound = temp[6];  // no iteration necessary
            T8_upper_bound = temp[6];  // no iteration necessary
            temp[7] = temp[6];
            UA_HT_calc = 0.0;
            last_HT_residual = 0.0;
            last_T8_guess = temp[6];
        }

        else
        {
            T8_lower_bound = temp[1];   // the absolute lowest temp[8] could be
            T8_upper_bound = temp[6];    // the absolutely highest temp[8] could be
            temp[7] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for first guess
            UA_HT_calc = -1.0;
            last_HT_residual = UA_HT;    // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT - 0.0
            last_T8_guess = temp[6];
        }


       // T8 and T9 loops 

              return 0;
      }

     
    // Return an array of the scaled pressure drops (in kPa) for the two streams of the heat exchanger defined by 'hxr'.
    //
    // Inputs:
    //   hxr -- a HeatExchanger type with design-point values set
    //   m_dots -- mass flow rates of the two streams (kg/s) [1: cold, 2: hot]
    //
     public Double [] hxr_pressure_drops(ref core.HeatExchanger HX, Double [] m_dots)
     {
         Double[] hxr_pressure_drops= new Double[2];
         m_dots = new Double[2];

         hxr_pressure_drops[0] = HX.DP_design1 * Math.Pow((m_dots[0] / HX.m_dot_design[0]),1.75);  //Pressure drop Cold Side
         hxr_pressure_drops[1] = HX.DP_design1 * Math.Pow((m_dots[1] / HX.m_dot_design[1]), 1.75);  //Pressure drop Hot Side

         return hxr_pressure_drops;
     } 

    // Return the scaled conductance (in kW/K) of the heat exchanger defined by 'hxr'.
    //
    // Inputs:
    //   hxr -- a HeatExchanger type with design-point values set
    //   m_dots -- mass flow rates of the two streams (kg/s) [1: cold, 2: hot]

     public Double hxr_conductance(ref core.HeatExchanger HX, Double[] m_dots)
     {
         Double m_dot_ratio;
         m_dots = new Double[2];
         Double hxr_conductance_result;

         m_dot_ratio = ((m_dots[0] / HX.m_dot_design[0] )+ (m_dots[1] / HX.m_dot_design[1])) * 0.5;  // average the two streams
         hxr_conductance_result = HX.UA_design * Math.Pow(m_dot_ratio, 0.8);

         return hxr_conductance_result;
     }


     //Function for calculating Heat Exchanger Conductance (UA) for supercritical Brayton power cycles
     //Next step will be fixing the Effectiveness in Heat Exchangers
     public void calculate_PHX_UA(Double Cp_HTF,Int64 N_sub_hxrs, Double Q_dot, Double m_dot_c, Double m_dot_h, Double T_c_in, Double T_h_in, Double P_c_in, Double P_c_out, Double P_h_in,
         Double P_h_out, ref Int64 error_code, ref Double UA, ref Double min_DT, ref Double[] Th1, ref Double[] Tc1, ref Double Effec, ref Double[] Ph1, ref Double[] Pc1, ref Double[] UA_local)
     {
         wmm = working_fluid.MolecularWeight;

         // Calculate the conductance (UA value) and minimum temperature difference of a heat exchanger
         // given its mass flow rates, inlet temperatures, and a rate of heat transfer.
         //
         // Inputs:
         //   N_sub_hxrs -- the number of sub-heat exchangers to use for discretization
         //   Q_dot -- rate of heat transfer in the heat exchanger (kW)
         //   m_dot_c -- cold stream mass flow rate (kg/s)
         //   m_dot_h -- hot stream mass flow rate (kg/s)
         //   T_c_in -- cold stream inlet temperature (K)
         //   T_h_in -- hot stream inlet temperature (K)
         //   P_c_in -- cold stream inlet pressure (kPa)
         //   P_c_out -- cold stream outlet pressure (kPa)
         //   P_h_in -- hot stream inlet pressure (kPa)
         //   P_h_out -- hot stream outlet pressure (kPa)
         //
         // Outputs:
         //   error_trace -- an ErrorTrace object
         //   UA -- heat exchanger conductance (kW/K)
         //   min_DT -- minimum temperature difference ("pinch point") between hot and cold streams in heat exchanger (K)
         //
         // Notes:
         //   1) Total pressure drop for each stream is divided equally among the sub-heat exchangers (i.e., DP is a linear distribution).


         //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
         Double TempH, TempC, h_c_in_mol;
         // Local Variables
         Double h_c_in, h_h_in, h_c_out, h_h_out;
         Double[] P_c = new Double[N_sub_hxrs + 1];
         Double[] P_h = new Double[N_sub_hxrs + 1];
         Double[] T_c = new Double[N_sub_hxrs + 1];
         Double[] T_h = new Double[N_sub_hxrs + 1];
         Double[] h_c = new Double[N_sub_hxrs + 1];
         Double[] h_h = new Double[N_sub_hxrs + 1];
         Double[] tempdifferences = new Double[N_sub_hxrs + 1];

         Double[] C_dot_c = new Double[N_sub_hxrs];
         Double[] C_dot_h = new Double[N_sub_hxrs];
         Double[] C_dot_min = new Double[N_sub_hxrs];
         Double[] C_dot_max = new Double[N_sub_hxrs];
         Double[] C_R = new Double[N_sub_hxrs];
         Double[] eff = new Double[N_sub_hxrs];
         Double[] NTU = new Double[N_sub_hxrs];

         // Check inputs.
         if (T_h_in < T_c_in)
         {
             error_code = 5;
             return;
         }

         if (P_h_in < P_h_out)
         {
             error_code = 6;
             return;
         }

         if (P_c_in < P_c_out)
         {
             error_code = 7;
             return;
         }

         if (Math.Abs(Q_dot) <= 1d - 12)  // very low Q_dot; assume it is zero
         {
             UA = 0.0;
             min_DT = T_h_in - T_c_in;
             return;
         }

         // Assume pressure varies linearly through heat exchanger.
         for (int a = 0; a <= N_sub_hxrs; a++)
         {
             P_c[a] = P_c_out + a * (P_c_in - P_c_out) / N_sub_hxrs;
             P_h[a] = P_h_in + a * (P_h_in - P_h_out) / N_sub_hxrs;

             Pc1[a] = P_c[a];
             Ph1[a] = P_h[a];
         }

         // Calculate inlet enthalpies from known state points.

         //if (present(enth)) enth = enth_mol / wmm
         //if (present(entr)) entr = entr_mol / wmm
         //if (present(ssnd)) ssnd = ssnd_RP


         //call CO2_TP(T=T_c_in, P=P_c(N_sub_hxrs+1), error_code=error_code, enth=h_c_in)
         working_fluid.FindStateWithTP(T_c_in, P_c[N_sub_hxrs]);
         h_c_in = working_fluid.Enthalpy;

         //call CO2_TP(T=T_h_in, P=P_h(1), error_code=error_code, enth=h_h_in)
         //working_fluid.FindStateWithTP(T_h_in, P_h[0]);
         h_h_in = Cp_HTF * T_h_in;

         // Calculate outlet enthalpies from energy balances supporsing 100% Heat transferred
         h_c_out = h_c_in + Q_dot / m_dot_c;
         h_h_out = h_h_in - Q_dot / m_dot_h;

         // Set up the enthalpy vectors and loop through the sub-heat exchangers, calculating temperatures.
         for (int b = 0; b <= N_sub_hxrs; b++)
         {
             h_c[b] = h_c_out + b * (h_c_in - h_c_out) / N_sub_hxrs;  // create linear vector of cold stream enthalpies, with index 1 at the cold stream outlet
             h_h[b] = h_h_in - b * (h_h_in - h_h_out) / N_sub_hxrs;   // create linear vector of hot stream enthalpies, with index 1 at the hot stream inlet
         }

         T_h[0] = T_h_in;  //hot stream inlet temperature

         wmm = working_fluid.MolecularWeight;


         //call CO2_PH(P=P_c(1), H=h_c(1), error_code=error_code, temp=T_c(1))  ! cold stream outlet temperature
         TempC = h_c[0] * wmm;

         //call CO2_PH(P=P_c(1), H=h_c(1), error_code=error_code, temp=T_c(1))  ! cold stream outlet temperature
         working_fluid.FindStatueWithPH(P_c[0], TempC);
         T_c[0] = working_fluid.Temperature;

         if (T_c[0] >= T_h[0])  // there was a second law violation in this sub-heat exchanger
         {
             error_code = 11;
             return;
         }

         //IMPORTANT!!!: When calling call CO2_PH is necessary before converting the Enthalpy units from kJ/Kg to J/mol

         for (int c = 0; c <= N_sub_hxrs; c++)
         {
             // call CO2_PH(P=P_h(i), H=h_h(i), error_code=error_code, temp=T_h(i))
             //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
             //TempH = h_h[c] * wmm;  // convert enthalpy to molar basis
             //working_fluid.FindStatueWithPH(P_h[c], TempH);
             T_h[c] = h_h[c] / Cp_HTF;

             // call CO2_PH(P=P_c(i), H=h_c(i), error_code=error_code, temp=T_c(i))
             //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
             TempC = h_c[c] * wmm;  // convert enthalpy to molar basis
             working_fluid.FindStatueWithPH(P_c[c], TempC);
             T_c[c] = working_fluid.Temperature;

             if (T_c[c] >= T_h[c])  // there was a second law violation in this sub-heat exchanger
             {
                 error_code = 11;
                 return;
             }
         }

         //UP TO HERE VALIDATED Temperatures and Enthapies

         // Perform effectiveness-NTU and UA calculations (note: the below are all array operations).
         for (int d = 0; d < N_sub_hxrs; d++)
         {
             C_dot_h[d] = m_dot_h * (h_h[d] - h_h[d + 1]) / (T_h[d] - T_h[d + 1]);  // hot stream capacitance rate
         }

         for (int e = 0; e < N_sub_hxrs; e++)
         {
             C_dot_c[e] = m_dot_c * (h_c[e] - h_c[e + 1]) / (T_c[e] - T_c[e + 1]);  // cold stream capacitance rate
         }

         for (int f = 0; f <= N_sub_hxrs - 1; f++)
         {
             C_dot_min[f] = Math.Min(C_dot_h[f], C_dot_c[f]);  // minimum capacitance stream
             C_dot_max[f] = Math.Max(C_dot_h[f], C_dot_c[f]);  // maximum capacitance stream
             C_R[f] = C_dot_min[f] / C_dot_max[f];
             eff[f] = Q_dot / ((N_sub_hxrs * C_dot_min[f] * (T_h[f] - T_c[f + 1])));  // effectiveness of each sub-heat exchanger

             if (C_R[f] == 1)
             {
                 NTU[f] = eff[f] / (1 - eff[f]);
             }

             else
             {
                 NTU[f] = Math.Log((1 - eff[f] * C_R[f]) / (1 - eff[f])) / (1 - C_R[f]);  // NTU if C_R does not equal 1
             }
         }

         UA = 0;

         for (int g = 0; g <= N_sub_hxrs - 1; g++)
         {
             UA_local[g] = NTU[g] * C_dot_min[g];
             UA = UA + NTU[g] * C_dot_min[g];  // calculate total UA value for the heat exchanger
         }

         for (int h = 0; h <= N_sub_hxrs; h++)
         {
             tempdifferences[h] = T_h[h] - T_c[h]; // temperatures differences within the heat exchanger
         }

         min_DT = tempdifferences[0];

         for (int i = 0; i <= N_sub_hxrs; i++)
         {
             if (tempdifferences[i] < min_DT)
             {
                 min_DT = tempdifferences[i]; // find the smallest temperature difference within the heat exchanger
             }

             Th1[i] = T_h[i];
             Tc1[i] = T_c[i];
         }

         // Calculate PHX Effectiveness
         Double C_dot_hot, C_dot_cold, C_dot_min1, Q_dot_max;

        C_dot_hot = m_dot_h * (h_h_in - h_h_out) / (T_h[0] -T_h[15] );   // PHX recuperator hot stream capacitance rate
        C_dot_cold = m_dot_c * (h_c_out - h_c_in) / (T_c[0] - T_c[15]);  // PXH recuperator cold stream capacitance rate
        C_dot_min1 = Math.Min(C_dot_hot, C_dot_cold);
        Q_dot_max = C_dot_min1 * (T_h[0] - T_c[15]);
        Effec = Q_dot / Q_dot_max;  // Definition of effectiveness
     }

     //Function for calculating Heat Exchanger Conductance (UA) for supercritical Brayton power cycles
     //Next step will be fixing the Effectiveness in Heat Exchangers
     public void calculate_Precooler_UA(Double Cp_HTF, Int64 N_sub_hxrs, Double Q_dot, Double m_dot_c, Double m_dot_h, Double T_c_in, Double T_h_in, Double P_c_in, Double P_c_out, Double P_h_in,
             Double P_h_out, ref Int64 error_code, ref Double UA, ref Double min_DT, ref Double[] Th1, ref Double[] Tc1, ref Double Effec, ref Double[] Ph1, ref Double[] Pc1, ref Double[] UA_local)
     {

         wmm = working_fluid.MolecularWeight;

         // Calculate the conductance (UA value) and minimum temperature difference of a heat exchanger
         // given its mass flow rates, inlet temperatures, and a rate of heat transfer.
         //
         // Inputs:
         //   N_sub_hxrs -- the number of sub-heat exchangers to use for discretization
         //   Q_dot -- rate of heat transfer in the heat exchanger (kW)
         //   m_dot_c -- cold stream mass flow rate (kg/s)
         //   m_dot_h -- hot stream mass flow rate (kg/s)
         //   T_c_in -- cold stream inlet temperature (K)
         //   T_h_in -- hot stream inlet temperature (K)
         //   P_c_in -- cold stream inlet pressure (kPa)
         //   P_c_out -- cold stream outlet pressure (kPa)
         //   P_h_in -- hot stream inlet pressure (kPa)
         //   P_h_out -- hot stream outlet pressure (kPa)
         //
         // Outputs:
         //   error_trace -- an ErrorTrace object
         //   UA -- heat exchanger conductance (kW/K)
         //   min_DT -- minimum temperature difference ("pinch point") between hot and cold streams in heat exchanger (K)
         //
         // Notes:
         //   1) Total pressure drop for each stream is divided equally among the sub-heat exchangers (i.e., DP is a linear distribution).


         //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
         Double TempH, TempC, h_c_in_mol;
         // Local Variables
         Double h_c_in, h_h_in, h_c_out, h_h_out;
         Double[] P_c = new Double[N_sub_hxrs + 1];
         Double[] P_h = new Double[N_sub_hxrs + 1];
         Double[] T_c = new Double[N_sub_hxrs + 1];
         Double[] T_h = new Double[N_sub_hxrs + 1];
         Double[] h_c = new Double[N_sub_hxrs + 1];
         Double[] h_h = new Double[N_sub_hxrs + 1];
         Double[] tempdifferences = new Double[N_sub_hxrs + 1];

         Double[] C_dot_c = new Double[N_sub_hxrs];
         Double[] C_dot_h = new Double[N_sub_hxrs];
         Double[] C_dot_min = new Double[N_sub_hxrs];
         Double[] C_dot_max = new Double[N_sub_hxrs];
         Double[] C_R = new Double[N_sub_hxrs];
         Double[] eff = new Double[N_sub_hxrs];
         Double[] NTU = new Double[N_sub_hxrs];

         // Check inputs.
         if (T_h_in < T_c_in)
         {
             error_code = 5;
             return;
         }

         if (P_h_in < P_h_out)
         {
             error_code = 6;
             return;
         }

         if (P_c_in < P_c_out)
         {
             error_code = 7;
             return;
         }

         if (Math.Abs(Q_dot) <= 1d - 12)  // very low Q_dot; assume it is zero
         {
             UA = 0.0;
             min_DT = T_h_in - T_c_in;
             return;
         }

         // Assume pressure varies linearly through heat exchanger.
         for (int a = 0; a <= N_sub_hxrs; a++)
         {
             P_c[a] = P_c_out + a * (P_c_in - P_c_out) / N_sub_hxrs;
             P_h[a] = P_h_in + a * (P_h_in - P_h_out) / N_sub_hxrs;

             Pc1[a] = P_c[a];
             Ph1[a] = P_h[a];
         }

         // Calculate inlet enthalpies from known state points.

         //if (present(enth)) enth = enth_mol / wmm
         //if (present(entr)) entr = entr_mol / wmm
         //if (present(ssnd)) ssnd = ssnd_RP


         //call CO2_TP(T=T_c_in, P=P_c(N_sub_hxrs+1), error_code=error_code, enth=h_c_in)
         //working_fluid.FindStateWithTP(T_c_in, P_c[N_sub_hxrs]);
         h_c_in = Cp_HTF * T_c_in;

         //call CO2_TP(T=T_h_in, P=P_h(1), error_code=error_code, enth=h_h_in)
         working_fluid.FindStateWithTP(T_h_in, P_h[0]);
         h_h_in = working_fluid.Enthalpy;

         // Calculate outlet enthalpies from energy balances supporsing 100% Heat transferred
         h_c_out = h_c_in + Q_dot / m_dot_c;
         h_h_out = h_h_in - Q_dot / m_dot_h;

         // Set up the enthalpy vectors and loop through the sub-heat exchangers, calculating temperatures.
         for (int b = 0; b <= N_sub_hxrs; b++)
         {
             h_c[b] = h_c_out + b * (h_c_in - h_c_out) / N_sub_hxrs;  // create linear vector of cold stream enthalpies, with index 1 at the cold stream outlet
             h_h[b] = h_h_in - b * (h_h_in - h_h_out) / N_sub_hxrs;   // create linear vector of hot stream enthalpies, with index 1 at the hot stream inlet
         }

         T_h[0] = T_h_in;  //hot stream inlet temperature

         wmm = working_fluid.MolecularWeight;


         //call CO2_PH(P=P_c(1), H=h_c(1), error_code=error_code, temp=T_c(1))  ! cold stream outlet temperature
         //TempC = h_c[0] * wmm;

         //call CO2_PH(P=P_c(1), H=h_c(1), error_code=error_code, temp=T_c(1))  ! cold stream outlet temperature
         //working_fluid.FindStatueWithPH(P_c[0], TempC);
         T_c[0] = h_c[0]/Cp_HTF;

         if (T_c[0] >= T_h[0])  // there was a second law violation in this sub-heat exchanger
         {
             error_code = 11;
             return;
         }

         //IMPORTANT!!!: When calling call CO2_PH is necessary before converting the Enthalpy units from kJ/Kg to J/mol

         for (int c = 0; c <= N_sub_hxrs; c++)
         {
             // call CO2_PH(P=P_h(i), H=h_h(i), error_code=error_code, temp=T_h(i))
             //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
             TempH = h_h[c] * wmm;  // convert enthalpy to molar basis
             working_fluid.FindStatueWithPH(P_h[c], TempH);
             T_h[c] = working_fluid.Temperature;

             // call CO2_PH(P=P_c(i), H=h_c(i), error_code=error_code, temp=T_c(i))
             //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
             TempC = h_c[c] * wmm;  // convert enthalpy to molar basis
             working_fluid.FindStatueWithPH(P_c[c], TempC);
             T_c[c] = h_c[c] / Cp_HTF;

             if (T_c[c] >= T_h[c])  // there was a second law violation in this sub-heat exchanger
             {
                 error_code = 11;
                 return;
             }
         }

         //UP TO HERE VALIDATED Temperatures and Enthapies

         // Perform effectiveness-NTU and UA calculations (note: the below are all array operations).
         for (int d = 0; d < N_sub_hxrs; d++)
         {
             C_dot_h[d] = m_dot_h * (h_h[d] - h_h[d + 1]) / (T_h[d] - T_h[d + 1]);  // hot stream capacitance rate
         }

         for (int e = 0; e < N_sub_hxrs; e++)
         {
             C_dot_c[e] = m_dot_c * (h_c[e] - h_c[e + 1]) / (T_c[e] - T_c[e + 1]);  // cold stream capacitance rate
         }

         for (int f = 0; f <= N_sub_hxrs - 1; f++)
         {
             C_dot_min[f] = Math.Min(C_dot_h[f], C_dot_c[f]);  // minimum capacitance stream
             C_dot_max[f] = Math.Max(C_dot_h[f], C_dot_c[f]);  // maximum capacitance stream
             C_R[f] = C_dot_min[f] / C_dot_max[f];
             eff[f] = Q_dot / ((N_sub_hxrs * C_dot_min[f] * (T_h[f] - T_c[f + 1])));  // effectiveness of each sub-heat exchanger

             if (C_R[f] == 1)
             {
                 NTU[f] = eff[f] / (1 - eff[f]);
             }

             else
             {
                 NTU[f] = Math.Log((1 - eff[f] * C_R[f]) / (1 - eff[f])) / (1 - C_R[f]);  // NTU if C_R does not equal 1
             }
         }

         UA = 0;

         for (int g = 0; g <= N_sub_hxrs - 1; g++)
         {
             UA_local[g] = NTU[g] * C_dot_min[g];
             UA = UA + NTU[g] * C_dot_min[g];  // calculate total UA value for the heat exchanger
         }

         for (int h = 0; h <= N_sub_hxrs; h++)
         {
             tempdifferences[h] = T_h[h] - T_c[h]; // temperatures differences within the heat exchanger
         }

         min_DT = tempdifferences[0];

         for (int i = 0; i <= N_sub_hxrs; i++)
         {
             if (tempdifferences[i] < min_DT)
             {
                 min_DT = tempdifferences[i]; // find the smallest temperature difference within the heat exchanger
             }

             Th1[i] = T_h[i];
             Tc1[i] = T_c[i];
         }

         // Calculate PHX Effectiveness
         Double C_dot_hot, C_dot_cold, C_dot_min1, Q_dot_max;

         C_dot_hot = m_dot_h * (h_h_in - h_h_out) / (T_h[0] - T_h[15]);   // PHX recuperator hot stream capacitance rate
         C_dot_cold = m_dot_c * (h_c_out - h_c_in) / (T_c[0] - T_c[15]);  // PXH recuperator cold stream capacitance rate
         C_dot_min1 = Math.Min(C_dot_hot, C_dot_cold);
         Q_dot_max = C_dot_min1 * (T_h[0] - T_c[15]);
         Effec = Q_dot / Q_dot_max;  // Definition of effectiveness
     }
   }
}
