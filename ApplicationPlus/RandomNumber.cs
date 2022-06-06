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
            int min = 0, max = 0;
            try
            {
                min = int.Parse(textBox1.Text);
                max = int.Parse(textBox2.Text);
                txtDisplay.Text = randomizer.Next(min,max+1).ToString(); 
            }
            catch (ArgumentOutOfRangeException) //если перепутали местами min и max
            {
                int _min = min;
                min = max;
                max = _min;
                MessageBox.Show("местами путать не надо!!", "Успех!");
                textBox1.Text = min.ToString();
                textBox2.Text = max.ToString();
                txtDisplay.Text = randomizer.Next(min, max + 1).ToString();
            }
            catch (FormatException) //буква или пустое поле ввода
            {
                min = 1;
                textBox1.Text = min.ToString();
                max = 10;
                textBox2.Text = max.ToString();
                txtDisplay.Text = randomizer.Next(min, max + 1).ToString();
            }
            catch (OverflowException) //слишком много циферок для инта
            {
                MessageBox.Show("Слишком мно-о-о-го", "Успех!");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
    }
}
