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
    public partial class RandomColor : Form
    {
        public RandomColor()
        {
            InitializeComponent();
        }
        Random randomizer = new Random();

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
        /// Полностью закрывает программу
        /// </summary>
        private void _Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            int r = randomizer.Next(0, 256); //Получаем случайное число 'r'.
            int g = randomizer.Next(0, 256); //Получаем случайное число 'g'.
            int b = randomizer.Next(0, 256); //Получаем случайное число 'b'.

            pictureBox1.BackColor = Color.FromArgb(255, r, g, b); //Выводим цвет на экран.

            string hexR = Convert.ToString(r, 16).PadLeft(2, '0'); //Получаем 'r' код цвета в шестнадцатеричном виде.
            string hexG = Convert.ToString(g, 16).PadLeft(2, '0'); //Получаем 'g' код цвета в шестнадцатеричном виде.
            string hexB = Convert.ToString(b, 16).PadLeft(2, '0'); //Получаем 'b' код цвета в шестнадцатеричном виде.

            //Выводим номера полученного цвета.
            info.Text = $"{r},{g},{b}"; //Пример: 106,47,79
            //Выводим полученный шестнадцатеричный код на экран, большими буквами.
            hexInfo.Text = $"#{hexR}{hexG}{hexB}".ToUpper(); //Пример: #6A2F4F
        }
    }
}
