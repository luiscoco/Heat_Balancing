﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Tablas_Vapor_ASME;
using Tablas_Vapor_ASME1;
using Tablas_Vapor_ASME2;
using Tablas_Vapor_ASME3;
using Tablas_Vapor_ASME4;
using Tablas_Vapor_ASME5;

using NumericalMethods;
using NumericalMethods.FourthBlog;


namespace Bifasico
{
    //Clase de Pérdida de Carga en Fluido Bifásico Boiling
    public class BifasicoBoilingChen
    {
        public RegionSelection selecregion = new RegionSelection();

        public Region1 region1 = new Region1();
        public Region2 region2 = new Region2();
        public Region_3 region3 = new Region_3();
        public Region_4 region4 = new Region_4();
        public Region5 region5 = new Region5();

        public double caudalmasico = 0;
        public double relacionviscosidades = 0;

        public double temperaturein = 0;
        public double pressurein = 0;

        public double densityinliquido = 0;
        public double densityinvapor = 0;
        public double densityintotal = 0;

        public double viscosidaddinamicaliquido = 0;
        public double viscosidaddinamicavapor = 0;

        public double viscosidadcinematicaliquido = 0;
        public double viscosidadcinematicavapor = 0;

        public double numreynoldliquido = 0;
        public double numreynoldvapor = 0;

        public double diametrointerior = 0;
        public double areafluido = 0;
        public double perimetrofluido = 0;
        public double diametrohidraulico = 0;

        public double velocidad = 0;
        public string tipoflujoLiquido = "";
        public string tipoflujoVapor = "";

        public double coefdarcyliquido = 0;
        public double coefdarcyvapor = 0;

        public double coefpeliculaDittusBoelter=0;

        public double titulo = 0;

        public double rugosidad = 0;

        public double calorespecifico = 0;
        public double conductividadtermica = 0;

        public double numeroPrandtl = 0;

        public double surfacetension = 0;

        //Cálculo de la Temperatura de Saturación
        public double calculotempsaturacion(double pressurein1)
        {
            temperaturein = region4.T4_p(pressurein1) - 273.15;
            return temperaturein;
        }

        //Cálculo de la Presión de Saturación
        public double calculopresionsaturacion(double temperaturein1)
        {
            pressurein = region4.p4_T(temperaturein1) / 10;
            return pressurein;
        }

        //Cálculo de la Density Líquido Saturado
        public double calculodensidadliquido(double temperaturein, double pressurein)
        {
            //Funciones originales de la Hoja Excel XSteam
            //rhoL_p
            // 1/vL_P(p)
            if ((pressurein > 0.000611657) && (pressurein < 22.06395))
            {
                if (pressurein < 16.529)
                {
                    densityinliquido = 1 / (region1.v1_pT(pressurein, region4.T4_p(pressurein)));
                }
                else
                {
                    densityinliquido = 1 / (region3.v3_ph(pressurein, region4.h4L_p(pressurein)));
                }
            }

            return densityinliquido;
        }

        //Cálculo de la Density Vapor Saturado
        public double calculodensidadvapor(double temperaturein, double pressurein)
        {
            //Funciones originales de la Hoja Excel XSteam
            //rhoV_p
            // 1/vV_P(p)
            if ((pressurein > 0.000611657) && (pressurein < 22.06395))
            {
                if (pressurein < 16.529)
                {
                    densityinvapor = 1 / (region2.v2_pT(pressurein, region4.T4_p(pressurein)));
                }

                else
                {
                    densityinvapor = 1 / (region3.v3_ph(pressurein, region4.h4V_p(pressurein)));
                }
            }

            return densityinvapor;
        }

        //Cálculo de la Viscosidad Vapor Saturado (IAPWS formulation 1985, Revised 2003)
        public double calculoviscosidaddinamicavapor(double densityinvapor1, double temperaturein, double pressurein)
        {
            //Variables de referencia
            //Temperatura 647.096 K
            double reftemperature = 647.226;
            //Densidad 322.0 Kg/m3
            double refdensity = 317.763;
            //Presión 22.064 MPa
            double refpressure = 22.115;

            //Variables de entrada del Usuario          

            //Densidad rhoin vapor saturado            
            densityinvapor1 = densityinvapor;

            //Variables adimensionales
            //Temperature T
            double temperature = 0;
            temperature = temperaturein / reftemperature;
            //Density rho
            double density = 0;
            density = densityinvapor1 / refdensity;
            //Pressure P
            double pressure = 0;
            pressure = pressurein / refpressure;

            double[] ho = new double[6];
            ho[0] = 0.5132047;
            ho[1] = 0.3205656;
            ho[2] = 0;
            ho[3] = 0;
            ho[4] = -0.7782567;
            ho[5] = 0.1885447;

            double[] h1 = new double[6];
            h1[0] = 0.2151778;
            h1[1] = 0.7317883;
            h1[2] = 1.241044;
            h1[3] = 1.476783;
            h1[4] = 0;
            h1[5] = 0;

            double[] h2 = new double[6];
            h2[0] = -0.2818107;
            h2[1] = -1.070786;
            h2[2] = -1.263184;
            h2[3] = 0;
            h2[4] = 0;
            h2[5] = 0;

            double[] h3 = new double[6];
            h3[0] = 0.1778064;
            h3[1] = 0.460504;
            h3[2] = 0.2340379;
            h3[3] = -0.4924179;
            h3[4] = 0;
            h3[5] = 0;

            double[] h4 = new double[6];
            h4[0] = -0.0417661;
            h4[1] = 0;
            h4[2] = 0;
            h4[3] = 0.1600435;
            h4[4] = 0;
            h4[5] = 0;

            double[] h5 = new double[6];
            h5[0] = 0;
            h5[1] = -0.01578386;
            h5[2] = 0;
            h5[3] = 0;
            h5[4] = 0;
            h5[5] = 0;

            double[] h6 = new double[6];
            h6[0] = 0;
            h6[1] = 0;
            h6[2] = 0;
            h6[3] = -0.003629481;
            h6[4] = 0;
            h6[5] = 0;

            //Cálculo de uo(temperature)
            double uo = 0;
            uo = Math.Pow(temperature, 0.5) / (1 + (0.978197 / temperature) + (0.579829 / (Math.Pow(temperature, 2))) - (0.202354 / (Math.Pow(temperature, 3))));

            //Cálculo de u1(temperature)
            double u1 = 0;

            double sum = 0;

            for (int i = 0; i <= 5; i++)
            {
                sum = sum + (ho[i] * Math.Pow((1 / temperature - 1), i)) + (h1[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 1)) + (h2[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 2)) + (h3[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 3)) + (h4[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 4)) + (h5[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 5)) + (h6[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 6));
            }

            u1 = Math.Exp(density * sum);

            viscosidaddinamicavapor = uo * u1 * 0.000055071;

            //Cálculo de la VISCOSIDAD VAPOR SATURADO (IAPWS formulation Revised 2008)
            //PENDIENTE IMPLEMENTAR

            return viscosidaddinamicavapor;
        }

        //Cálculo de la Viscosidad Líquido Saturado (IAPWS formulation 1985, Revised 2003)
        public double calculoviscosidaddinamicaliquido(double densityinliquido1, double temperaturein, double pressurein)
        {
            //Variables de referencia
            //Temperatura 647.096 K
            double reftemperature = 647.226;
            //Densidad 322.0 Kg/m3
            double refdensity = 317.763;
            //Presión 22.064 MPa
            double refpressure = 22.115;

            //Variables de entrada del Usuario          

            //Densidad rhoin vapor saturado
            densityinliquido1 = densityinliquido;

            //Variables adimensionales
            //Temperature T
            double temperature = 0;
            temperature = temperaturein / reftemperature;
            //Density rho
            double density = 0;
            density = densityinliquido1 / refdensity;
            //Pressure P
            double pressure = 0;
            pressure = pressurein / refpressure;

            double[] ho = new double[6];
            ho[0] = 0.5132047;
            ho[1] = 0.3205656;
            ho[2] = 0;
            ho[3] = 0;
            ho[4] = -0.7782567;
            ho[5] = 0.1885447;

            double[] h1 = new double[6];
            h1[0] = 0.2151778;
            h1[1] = 0.7317883;
            h1[2] = 1.241044;
            h1[3] = 1.476783;
            h1[4] = 0;
            h1[5] = 0;

            double[] h2 = new double[6];
            h2[0] = -0.2818107;
            h2[1] = -1.070786;
            h2[2] = -1.263184;
            h2[3] = 0;
            h2[4] = 0;
            h2[5] = 0;

            double[] h3 = new double[6];
            h3[0] = 0.1778064;
            h3[1] = 0.460504;
            h3[2] = 0.2340379;
            h3[3] = -0.4924179;
            h3[4] = 0;
            h3[5] = 0;

            double[] h4 = new double[6];
            h4[0] = -0.0417661;
            h4[1] = 0;
            h4[2] = 0;
            h4[3] = 0.1600435;
            h4[4] = 0;
            h4[5] = 0;

            double[] h5 = new double[6];
            h5[0] = 0;
            h5[1] = -0.01578386;
            h5[2] = 0;
            h5[3] = 0;
            h5[4] = 0;
            h5[5] = 0;

            double[] h6 = new double[6];
            h6[0] = 0;
            h6[1] = 0;
            h6[2] = 0;
            h6[3] = -0.003629481;
            h6[4] = 0;
            h6[5] = 0;

            //Cálculo de uo(temperature)
            double uo = 0;
            uo = Math.Pow(temperature, 0.5) / (1 + (0.978197 / temperature) + (0.579829 / (Math.Pow(temperature, 2))) - (0.202354 / (Math.Pow(temperature, 3))));

            //Cálculo de u1(temperature)
            double u1 = 0;

            double sum = 0;

            for (int i = 0; i <= 5; i++)
            {
                sum = sum + (ho[i] * Math.Pow((1 / temperature - 1), i)) + (h1[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 1)) + (h2[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 2)) + (h3[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 3)) + (h4[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 4)) + (h5[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 5)) + (h6[i] * Math.Pow((1 / temperature - 1), i) * Math.Pow((density - 1), 6));
            }

            u1 = Math.Exp(density * sum);

            viscosidaddinamicaliquido = uo * u1 * 0.000055071;

            //Cálculo de la VISCOSIDAD VAPOR SATURADO (IAPWS formulation Revised 2008)
            //PENDIENTE IMPLEMENTAR

            return viscosidaddinamicaliquido;
        }

        //Cálculo de la VELOCIDAD
        public double calculovelocidad(double caudalmasico, double areafluido, double densityin1)
        {
            velocidad = caudalmasico / (areafluido * densityin1);
            return velocidad;
        }

        //Cálculo del DIÁMETRO HIDRÁULICO
        public double calculodiametrohidraulico(double diametrointerior)
        {
            areafluido = (3.1416 * diametrointerior * diametrointerior) / 4;
            perimetrofluido = 3.1416 * diametrointerior;
            diametrohidraulico = (4 * areafluido) / perimetrofluido;
            return diametrohidraulico;
        }

        //Cálculo del Número de Reynolds Liquido Saturado
        public double calculonumreynoldsliquido(double densityin, double velocidad, double diametrohidraulico, double viscosidaddinamica)
        {
            numreynoldliquido = (densityin * velocidad * diametrohidraulico) / viscosidaddinamica;

            if (numreynoldliquido > 4000)
            {
                tipoflujoLiquido = "Turbulento";
            }

            else if (numreynoldliquido < 2300)
            {
                tipoflujoLiquido = "Laminar";
            }

            else if ((2300 < numreynoldliquido) && (numreynoldliquido < 4000))
            {
                tipoflujoLiquido = "Transitorio";
            }

            return numreynoldliquido;
        }

        //Cálculo del Número de Reynolds Vapor Saturado
        public double calculonumreynoldsvapor(double densityin, double velocidad, double diametrohidraulico, double viscosidaddinamica)
        {
            numreynoldvapor = (densityin * velocidad * diametrohidraulico) / viscosidaddinamica;

            if (numreynoldvapor > 4000)
            {
                tipoflujoVapor = "Turbulento";
            }

            else if (numreynoldvapor < 2300)
            {
                tipoflujoVapor = "Laminar";
            }

            else if ((2300 < numreynoldvapor) && (numreynoldvapor < 4000))
            {
                tipoflujoVapor = "Transitorio";
            }

            return numreynoldvapor;
        }


        //Cálculo del CALOR ESPECÍFICO ISOBÁRICO Cp
        public double calculocalorespisob(double pressurein, double temperaturein)
        {
            selecregion.region_pT(pressurein, temperaturein);
            //Segundo Cálculo de la Densidad

            if (selecregion.regionpT == 1)
            {
                calorespecifico = region1.Cp1_pT(pressurein, temperaturein);
            }

            else if (selecregion.regionpT == 2)
            {
                calorespecifico = region2.Cp2_pT(pressurein, temperaturein);
            }

            else if (selecregion.regionpT == 3)
            {
                MessageBox.Show("Region 3: no hay datos para el cálculo de a Densidad.");
            }

            else if (selecregion.regionpT == 4)
            {
                MessageBox.Show("Region 4: no hay datos para el cálculo de a Densidad.");
            }

            else if (selecregion.regionpT == 5)
            {
                calorespecifico = region5.Cp5_pT(pressurein, temperaturein);
            }
            else
            {
                MessageBox.Show("Error al seleccionar la Región.");
            }

            return calorespecifico;
        }

        //Cálculo de la CONDUCTIVIDAD TÉRMICA K
        public double calculoconductividad(double temperature, double rho)
        {
            double tc0 = 0;
            double tc1 = 0;
            double tc2 = 0;
            double dT = 0;
            double Q = 0;
            double s = 0;

            temperature = temperature / 647.26;
            rho = rho / 317.7;

            tc0 = Math.Pow(temperature, 0.5) * (0.0102811 + 0.0299621 * temperature + 0.0156146 * Math.Pow(temperature, 2) - 0.00422464 * Math.Pow(temperature, 3));
            tc1 = -0.39707 + (0.400302 * rho) + 1.06 * Math.Exp(-0.171587 * Math.Pow((rho + 2.39219), 2));
            dT = Math.Abs(temperature - 1) + 0.00308976;
            Q = 2 + 0.0822994 / (Math.Pow(dT, (3.0 / 5.0)));

            if (temperature >= 1)
            {
                s = 1 / dT;
            }

            else
            {
                s = 10.0932 / Math.Pow(dT, (3.0 / 5.0));
            }

            tc2 = (((0.0701309 / (Math.Pow(temperature, 10))) + 0.011852) * Math.Pow(rho, (9.0 / 5.0)) * Math.Exp(0.642857 * (1 - Math.Pow(rho, (14.0 / 5.0))))) + (0.00169937 * s * Math.Pow(rho, Q) * Math.Exp((Q / (1 + Q)) * (1 - Math.Pow(rho, (1 + Q))))) - (1.02 * Math.Exp(-4.11717 * Math.Pow(temperature, (3.0 / 2.0)) - (6.17937 / Math.Pow(rho, 5))));

            conductividadtermica = tc0 + tc1 + tc2;

            return conductividadtermica;
        }

        //Cálculo del Número de PRANDTL
        public double calculonumprandtl(double calorespecifico, double viscosidaddinamica, double conductividadtermica)
        {
            numeroPrandtl = (calorespecifico * 1000 * viscosidaddinamica) / conductividadtermica;
            return numeroPrandtl;
        }

        //Cálculo Tensión Superficial LockHart Martinelli
        public double calculotensionsuperficial(double temperaturein1)
        {
            //IAPWS Release on Surface Tension of Ordinary Water Substance,September 1994
            double tau = 0;
            double tc = 647.096;
            double b = 0.2358;
            double bb = -0.625;
            double my = 1.256;

            if ((temperaturein1 < 0.01) || (temperaturein1 > tc))
            {
                MessageBox.Show("Out of valid region");
            }

            tau = 1 - (temperaturein1 / tc);

            surfacetension = b * Math.Pow(tau, my) * (1 + bb * tau);

            return surfacetension;
        }
        
        //Cálculo del HTC según DITTUS-BOELTER
        public double calculoHTCDittusBoelter(double numreynold1, double numprandtl1, double conductividad1, double diahidraulico1)
        {
            numreynold1 = numreynoldliquido;
            numprandtl1 = numeroPrandtl;
            conductividad1 = conductividadtermica;
            diahidraulico1 = diametrohidraulico;

            coefpeliculaDittusBoelter = 0.023 * Math.Pow(numreynold1, 0.8) * Math.Pow(numprandtl1, 0.4) * (conductividad1 / diahidraulico1);

            return coefpeliculaDittusBoelter;
        }



    }
}