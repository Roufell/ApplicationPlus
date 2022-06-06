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
            int r = randomizer.Next(0, 256);
            int g = randomizer.Next(0, 256);
            int b = randomizer.Next(0, 256);
            pictureBox1.BackColor = Color.FromArgb(255, r, g, b);
            info.Text = $"{r},{g},{b}";
        }
    }
}
