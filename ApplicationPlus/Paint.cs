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
    public partial class Paint : Form
    {
        private bool isMouse = false; //Проверка: Зажата ли левая кнопка мыши.

        public Paint()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //Когда зажали левою кнопку мыши.
            isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //Когда отпустили левою кнопку мыши.
            isMouse = false;
        }
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
        private void backButton_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 frm = new Form1();
            frm.Show();
        }
    }
}
