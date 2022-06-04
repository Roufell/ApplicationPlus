using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationPlus
{
    public partial class Calculator : Form
    {
        const string divideByZero = "Нельзя делить на ноль!";
        const string syntaxErr = "Синтаксическая ошибка!";
        bool decimalPointActive = false;

        public Calculator()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Возвращается в начальное меню
        /// </summary>
        private void backButton(object sender, EventArgs e)
        {
            Hide();
            Form1 frm = new Form1();
            frm.Show();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDisplay.Text)) return;
            Clipboard.SetText(txtDisplay.Text);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            decimalPointActive = false;
            PreCheck_ButtonClick();
            previousOperation = Operation.None;
            txtDisplay.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            decimalPointActive = false;
            PreCheck_ButtonClick();
            if (txtDisplay.Text.Length > 0)
            {
                double d;
                if (!double.TryParse(txtDisplay.Text[txtDisplay.Text.Length - 1].ToString(), out d))
                {
                    previousOperation = Operation.None;
                }

                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1, 1);
            }
            if (txtDisplay.Text.Length == 0)
            {
                previousOperation = Operation.None;
            }
            if (previousOperation != Operation.None)
            {
                currentOperation = previousOperation;
            }
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            PreCheck_ButtonClick();
            currentOperation = Operation.Div;
            PerformCalculation(previousOperation);

            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }

        private void btnMul_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            PreCheck_ButtonClick();
            currentOperation = Operation.Mul;
            PerformCalculation(previousOperation);
            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0 || previousOperation == Operation.Sub) return;
            PreCheck_ButtonClick();
            currentOperation = Operation.Sub;
            PerformCalculation(previousOperation);

            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            PreCheck_ButtonClick();
            currentOperation = Operation.Add;
            PerformCalculation(previousOperation);

            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }
        private void PerformCalculation(Operation previousOperation)
        {
            try
            {
                if (previousOperation == Operation.None)
                    return;
                List<double> lstNums = null;

                switch (previousOperation)
                {
                    case Operation.Add:
                        if (currentOperation == Operation.Sub)
                        {
                            currentOperation = Operation.Add;
                            return;
                        }
                        lstNums = txtDisplay.Text.Split('+').Select(double.Parse).ToList();
                        txtDisplay.Text = (lstNums[0] + lstNums[1]).ToString();
                        break;
                    case Operation.Sub:
                        int idx = txtDisplay.Text.LastIndexOf('-'); // To handle ex: -9-2
                        if (idx > 0)
                        {
                            var op1 = Convert.ToDouble(txtDisplay.Text.Substring(0, idx));
                            var op2 = Convert.ToDouble(txtDisplay.Text.Substring(idx + 1));
                            txtDisplay.Text = (op1 - op2).ToString();
                        }
                        break;
                    case Operation.Mul:
                        if (currentOperation == Operation.Sub)
                        {
                            currentOperation = Operation.Mul;
                            return;
                        }
                        lstNums = txtDisplay.Text.Split('*').Select(double.Parse).ToList();
                        txtDisplay.Text = (lstNums[0] * lstNums[1]).ToString();
                        break;
                    case Operation.Div:
                        if (currentOperation == Operation.Sub)
                        {
                            currentOperation = Operation.Div;
                            return;
                        }
                        try
                        {
                            lstNums = txtDisplay.Text.Split('/').Select(double.Parse).ToList();
                            if (lstNums[1] == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            txtDisplay.Text = (lstNums[0] / lstNums[1]).ToString();
                        }
                        catch (DivideByZeroException)
                        {
                            txtDisplay.Text = divideByZero;
                        }
                        break;
                    case Operation.None:
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                txtDisplay.Text = syntaxErr;
            }
        }

        private void BtnNun_Click(object btn, EventArgs e)
        {
            if (txtDisplay.Text == syntaxErr || txtDisplay.Text == divideByZero)
            {
                txtDisplay.Text = string.Empty;
            }
            EnableOperatorButtons();
            PreCheck_ButtonClick();
            txtDisplay.Text += (btn as Button).Text;
        }
        private void PreCheck_ButtonClick()
        {
            if (txtDisplay.Text == divideByZero || txtDisplay.Text == syntaxErr)
                txtDisplay.Clear();
            if (previousOperation != Operation.None)
            {
                EnableOperatorButtons();
            }
        }
        private void EnableOperatorButtons(bool enable = true)
        {
            btnMul.Enabled = enable;
            btnDiv.Enabled = enable;
            btnAdd.Enabled = enable;
            if (!enable)
            {
                decimalPointActive = false;
            }
            //btnSub.Enabled = enable;
        }
        enum Operation
        {
            Add,
            Sub,
            Mul,
            Div,
            None
        }

        Operation previousOperation = Operation.None;
        Operation currentOperation = Operation.None;

        private void btnRes_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

            previousOperation = Operation.None;
        }

        private void btnDecimal_Click(object sender, EventArgs e)
        {
            if (decimalPointActive) return;
            if (txtDisplay.Text == syntaxErr || txtDisplay.Text == divideByZero)
            {
                txtDisplay.Text = string.Empty;
            }
            EnableOperatorButtons();
            PreCheck_ButtonClick();
            txtDisplay.Text += (sender as Button).Text;
            decimalPointActive = true;
        }
        /// <summary>
        /// Полностью выключает программу
        /// </summary>
        private void _Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
