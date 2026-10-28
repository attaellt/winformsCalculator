using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        List<double> numbersDouble = new List<double>();
        List<string> numbersStringList = new List<string>();
        List<char> operations = new List<char>();
        bool warningOfZero = false;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            numbersStringList.Clear();
            operations.Clear();

            if (warningOfZero)
            {
                textBox1.Text = textBox1.Text[textBox1.Text.Length - 1].ToString();
                warningOfZero = false;
            }

            string equation = textBox1.Text;
            char[] delimiters = { '+', '-', '÷', '·' };

            foreach (char c in equation)
            {
                if (c == '+' || c == '-' || c == '÷' || c == '·')
                    operations.Add(c);
            }

            string[] numbersStringArray = equation.Split(delimiters);

            numbersStringList.AddRange(numbersStringArray);

            numbersDouble = numbersStringList.Select(s => double.TryParse(s, out double d) ? (double?)d : null)
                    .Where(d => d.HasValue)
                    .Select(d => d.Value)
                    .ToList();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text += 1;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text += 2;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text += 3;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text += 4;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text += 5;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text += 6;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += 7;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += 8;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text += 9;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox1.Text += 0;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox1.Text += "-";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox1.Text += ",";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "+";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "-";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "÷";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "·";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "²";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "√";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            else
                 textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (operations.Count == 0 || numbersDouble.Count == 0)
                return;

            List<double> numbersDoubleInButton = new List<double>();
            List<char> operationsInButton = new List<char>();

            double result = -1;
            bool priority = false;

            for (int i = 0; i < operations.Count; i++ )
            {
                if (operations[i] == '÷' || operations[i] == '·')
                {
                    if (priority)
                    {
                        result = ToCount(result, numbersDouble[i + 1], operations[i]);
                        numbersDoubleInButton.RemoveAt(numbersDoubleInButton.Count - 1);
                    }
                    else
                        result = ToCount(numbersDouble[i], numbersDouble[i + 1], operations[i]);

                    numbersDoubleInButton.Add(result);
                    priority = true;
                }
                else
                {
                    if (!priority)
                    {
                        numbersDoubleInButton.Add(numbersDouble[i]);
                    }

                    priority = false;
                    operationsInButton.Add(operations[i]);
                    

                    if (i == operations.Count - 1)
                        numbersDoubleInButton.Add(numbersDouble[i + 1]);
                }
                
            }
            if (numbersDoubleInButton.Count == 1)
                textBox1.Text = numbersDoubleInButton[0].ToString();
            else
            {
                result = ToCount(numbersDoubleInButton[0], numbersDoubleInButton[1], operationsInButton[0]);


                if (numbersDoubleInButton.Count == 2)
                {
                    if (warningOfZero)
                        textBox1.Text = "Nelze dělit 0";
                    else
                        textBox1.Text = result.ToString();
                }
                else
                {
                    for (int i = 2; i < numbersDoubleInButton.Count; i++)
                    {
                        result = ToCount(result, numbersDoubleInButton[i], operationsInButton[i - 1]);
                    }

                    if (warningOfZero)
                        textBox1.Text = "Nelze dělit 0";
                    else
                        textBox1.Text = result.ToString();
                }
            }
        }

        public double ToCount(double x, double y, char operation)
        {
            double result = -1;

            switch (operation)
            {
                case '+':
                    result = x + y;
                    break;
                case '-':
                    result = x - y;
                    break;
                case '÷':
                    if (y == 0)
                        warningOfZero = true;
                    else
                        result = x / y;
                    break;
                case '·':
                    result = x * y;
                    break;
            }

            return result;
        }
    }
}
