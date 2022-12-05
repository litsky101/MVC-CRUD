using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIDC
{
    public partial class ArithmeticOperations : Form
    {
        public ArithmeticOperations()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string operation = string.Empty;
            decimal firstNumber = 0;
            decimal secondNumber = 0;
            decimal result = 0;

            if (string.IsNullOrEmpty(txtNum1.Text))
            {
                MessageBox.Show(this, "Please enter first number.", "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(txtNum2.Text))
            {
                MessageBox.Show(this, "Please enter second number.", "Warning",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning);
                return;
            }
            else
            {
                firstNumber = Convert.ToDecimal(txtNum1.Text);
                secondNumber = Convert.ToDecimal(txtNum2.Text);
            }

            operation = cboOperation.Text;

            switch (operation)
            {
                case "Addition":
                    result = firstNumber + secondNumber;
                    break;
                case "Subtraction":
                    result = firstNumber - secondNumber;
                    break;
                case "Multiplication":
                    result = firstNumber * secondNumber;
                    break;
                case "Division":

                    if(secondNumber == 0)
                    {
                        MessageBox.Show(this, "Second number cannot be zero in division.", "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                        return;
                    }

                    result = firstNumber / secondNumber;
                    break;
                default:
                    MessageBox.Show(this, "Please select operation", "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    break;
            }

            lblResult.Text = result.ToString("N2");
        }
    }
}
