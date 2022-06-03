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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Кнопка для перехода в "Калькулятор"
        /// </summary>
        private void TransitionCalculatorButton(object sender, EventArgs e)
        {
            Hide();
            Calculator frm = new Calculator();
            frm.Show();
        }
        /// <summary>
        /// Кнопка для перехода в "Рандомный цвет"
        /// </summary>
        private void TransitionRandomColorButton(object sender, EventArgs e)
        {
            Hide();
            RandomColor frm = new RandomColor();
            frm.Show();
        }
        /// <summary>
        /// Кнопка для перехода в "Генератор случайных чисел"
        /// </summary>
        private void TransitionRandomNumberButton(object sender, EventArgs e)
        {
            Hide();
            RandomNumber frm = new RandomNumber();
            frm.Show();
        }
        /// <summary>
        /// Кнопка для перехода в "Календарь"
        /// </summary>
        private void TransitionCalendarButton(object sender, EventArgs e)
        {
            Hide();
            Calendar frm = new Calendar();
            frm.Show();
        }
        /// <summary>
        /// Кнопка для перехода в "Рисовалка"
        /// </summary>
        private void TransitionPaintButton(object sender, EventArgs e)
        {
            Hide();
            Paint frm = new Paint();
            frm.Show();
        }
        /// <summary>
        /// Полностью закрывает программу
        /// </summary>
        private void _Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
