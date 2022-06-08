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

        /// <summary>
        /// метод для кнопки 'Copy'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDisplay.Text)) return;
            Clipboard.SetText(txtDisplay.Text);
        }
        /// <summary>
        /// метод для кнопки 'Reset'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            decimalPointActive = false;
            PreCheck_ButtonClick();
            previousOperation = Operation.None;
            txtDisplay.Clear();
        }
        /// <summary>
        /// метод для кнопки 'Clear'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            decimalPointActive = false;
            PreCheck_ButtonClick();
            if (txtDisplay.Text.Length > 0) //Если длина текста на экране больше нуля
            {
                double d;
                string lastSymbol = txtDisplay.Text[txtDisplay.Text.Length - 1].ToString(); //Получаем последний знак с экрана.
                if (!double.TryParse(lastSymbol, out d)) //Пытаемся преобразовать последний символ из экрана, и получаем его в переменную d.
                { //Если не получается преобразовать,
                    previousOperation = Operation.None; //то забывается предыдущий знак.
                }

                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1, 1);
            }
            if (txtDisplay.Text.Length == 0) //Если длина текста равна нулю,
            {
                previousOperation = Operation.None; //то забывается предыдущий знак.
            }
            if (previousOperation != Operation.None) //Если предыдущая операция не пустая,
            {
                currentOperation = previousOperation; //то предыдущая операция становится текущей.
            }
        }
        /// <summary>
        /// метод для кнопки 'диление'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDiv_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return; //Если длина текста на экране равна нулю, то метод закрывается.
            PreCheck_ButtonClick();
            currentOperation = Operation.Div;
            PerformCalculation(previousOperation);

            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }
        /// <summary>
        /// метод для кнопки 'умножение'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMul_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return; //Если длина текста на экране равна нулю, то метод закрывается.
            PreCheck_ButtonClick();
            currentOperation = Operation.Mul;
            PerformCalculation(previousOperation);
            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }
        /// <summary>
        /// метод для кнопки 'минус'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSub_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0 || previousOperation == Operation.Sub) return; //Если длина текста на экране равна нулю или
                                                                                          //предыдущая операция равна "-", то метод закрывается.
            PreCheck_ButtonClick();
            currentOperation = Operation.Sub;
            PerformCalculation(previousOperation);

            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }
        /// <summary>
        /// метод для кнопки 'плюс'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return; //Если длина текста на экране равна нулю, то метод закрывается.
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
                if (previousOperation == Operation.None) //Если предыдущая операция пустая,
                    return; //то выходим из метода.  
                List<double> lstNums = null; //об. пустой коллекции.

                switch (previousOperation)
                {
                    case Operation.Add: //Плюс
                        if (currentOperation == Operation.Sub)  //Если текущая операция "равно",
                        {
                            currentOperation = Operation.Add; //то текущая операция присваивает операцию "+" и выходит из метода.
                            return;
                        }
                        lstNums = txtDisplay.Text.Split('+').Select(double.Parse).ToList();
                        txtDisplay.Text = (lstNums[0] + lstNums[1]).ToString();
                        break;
                    case Operation.Sub: //Минус
                        int idx = txtDisplay.Text.LastIndexOf('-'); // To handle ex: -9-2
                        if (idx > 0)
                        {
                            var op1 = Convert.ToDouble(txtDisplay.Text.Substring(0, idx));
                            var op2 = Convert.ToDouble(txtDisplay.Text.Substring(idx + 1));
                            txtDisplay.Text = (op1 - op2).ToString();
                        }
                        break;
                    case Operation.Mul: //Умножения
                        if (currentOperation == Operation.Sub) //Если текущая опреация "равно",
                        {
                            currentOperation = Operation.Mul; //то текущая операция присваивает операцию "*" и выходит из метода.
                            return;
                        }
                        lstNums = txtDisplay.Text.Split('*').Select(double.Parse).ToList();
                        txtDisplay.Text = (lstNums[0] * lstNums[1]).ToString();
                        break;
                    case Operation.Div: //Диления
                        if (currentOperation == Operation.Sub) //Если текущая опреация "равно",
                        {
                            currentOperation = Operation.Div; //то текущая операция присваивает операцию "/" и выходит из метода.
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
        /// <summary>
        /// метод для всех цифр
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="e"></param>
        private void BtnNun_Click(object btn, EventArgs e)
        {
            if (txtDisplay.Text == syntaxErr || txtDisplay.Text == divideByZero)
            {
                txtDisplay.Text = string.Empty; //Выводит ошибку на экран
            }
            EnableOperatorButtons();
            PreCheck_ButtonClick();
            txtDisplay.Text += (btn as Button).Text;
        }
        /// <summary>
        /// Предварительная проверка
        /// </summary>
        private void PreCheck_ButtonClick()
        {
            if (txtDisplay.Text == divideByZero || txtDisplay.Text == syntaxErr) txtDisplay.Clear(); //Если будет ошибка, то экран ввода очиститься

            if (previousOperation != Operation.None)
            {
                EnableOperatorButtons();
            }
        }
        /// <summary>
        /// Включение кнопок оператора
        /// </summary>
        /// <param name="enable"></param>
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

        Operation previousOperation = Operation.None; //Предыдущая операция
        Operation currentOperation = Operation.None; //Текущая операция
        /// <summary>
        /// метод для кнопки 'равно'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRes_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

            previousOperation = Operation.None;
        }
        /// <summary>
        /// метод для конопки "запятая(Не целые числа)" 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
