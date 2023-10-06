﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2_Liceth_Sarmiento
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Boton calcular
        private void button1_Click(object sender, EventArgs e)
        {
            string ecuacion1 = textBox1.Text.Trim();
            string ecuacion2 = textBox2.Text.Trim();

            if (!validarFunciones(ecuacion1) || !validarFunciones(ecuacion2))
            {
                solucion.Text = "Piense en lo que ingresó y vuelva a intentarlo.";
                return;
            }

            bool f1 = ObtenerVal(ecuacion1, out double m1, out double b1);
            bool f2 = ObtenerVal(ecuacion2, out double m2, out double b2);

            if (!f1 || !f2)
            {
                solucion.Text = "Las ecuaciones ingresadas no son validas, por favor piensa en lo que escribió y vuelva a intentarlo.";
                return;
            }

            if (m1 == m2)
            {
                solucion.Text = b1 == b2 ? "Las líneas son idénticas." : "Las líneas son paralelas.";
            }
            else if (m1 * m2 + 1 == 0)
            {
                solucion.Text = "Las líneas son perpendiculares.";
            }
            else
            {
                double x = (b2 - b1) / (m1 - m2);
                double y = m1 * x + b1;
                solucion.Text = $"Las líneas se intersectan en: ({x} , {y})";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!validarFunciones(textBox1.Text))
            {
                textBox1.ForeColor = Color.Black;
            }
            else
            {
                textBox1.ForeColor = Color.Black;
            }
        }

        

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!validarFunciones(textBox1.Text))
            {
                textBox1.ForeColor = Color.Black;
            }
            else
            {
                textBox1.ForeColor = Color.Black;
            }
        }
        private bool validarFunciones(string funciones)
        {
            funciones = funciones.Replace(" ", String.Empty);

            string puntoPendiente = @"^y=(-?\d*\.?\d*)x([+\-]\d*)?$";
            string pendiIntercepto = @"^y([+\-]\d*\.?\d*)=(-?\d*\.?\d*)\(x([+\-]\d*\.?\d*)\)$";

            return Regex.IsMatch(funciones, puntoPendiente, RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(funciones, pendiIntercepto, RegexOptions.IgnoreCase);
        }
        private bool ObtenerVal(string formula, out double m, out double b)
        {
            m = 0;
            b = 0;
            formula = formula.ToLower().Replace(" ", "");

            if (extraerValPenInter(formula, out m, out b) ||
                extraerValPuntoPendiente(formula, out m, out b))
            {
                return true;
            }

            return false;
        }

        private bool extraerValPenInter (string formula, out double m, out double b)
        {
            m = 0;
            b = 0;

            var match = Regex.Match(formula, @"y=(-?\d*\.?\d*)x([+\-]\d*\.?\d*)?$");
            if (match.Success)
            {
                m = double.Parse(match.Groups[1].Value);
                b = match.Groups[2].Success ? double.Parse(match.Groups[2].Value) : 0;
                return true;
            }

            return false;
        }

        private bool extraerValPuntoPendiente(string formula, out double m, out double b)
        {
            m = 0;
            b = 0;

            var match = Regex.Match(formula, @"y([+\-]\d*\.?\d*)=(-?\d*\.?\d*)\(x([+\-]\d*\.?\d*)\)$");
            if (match.Success)
            {
                double y1 = double.Parse(match.Groups[1].Value);
                m = double.Parse(match.Groups[2].Value);
                double x1 = double.Parse(match.Groups[3].Value);
                b = y1 - m * x1;
                return true;
            }

            return false;
        }

        private void solucion_Click(object sender, EventArgs e)
        {

        }
    }

}