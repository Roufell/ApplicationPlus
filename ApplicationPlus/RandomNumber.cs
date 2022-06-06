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
    public partial class RandomNumber : Form
    {
        public RandomNumber()
        {
            InitializeComponent();
        }
        Random randomizer = new Random();


        /// <summary>
        /// Полностью закрывает программу
        /// </summary>
        private void _Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                int min = int.Parse(textBox1.Text);
                int max = int.Parse(textBox2.Text);
                txtDisplay.Text = randomizer.Next(min,max+1).ToString(); 
            }
            catch
            {
                MessageBox.Show("Ошибка","Успех");
            }
        }
    }
}
