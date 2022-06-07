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
        public Paint()
        {
            InitializeComponent();
            SetSize();
        }
        private class ArrayPoints
        {
            private int index = 0; //Номер текущей точки в массиве.
            private Point[] points; //В этом массиве храняться точки.
            /// <summary>
            /// Размер кисти
            /// </summary>
            /// <param name="size"></param>
            public ArrayPoints(int size)
            {
                if (size <= 0) size = 2; //Проверка на минус.
                points = new Point[size]; 
            }
            /// <summary>
            /// Принимает координаты x и y
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public void SetPoint(int x, int y)
            {
                if (index >= points.Length) //Проверка: выходим ли мы за границы массива.
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }
            /// <summary>
            /// Метод который сбрасивает точки
            /// </summary>
            public void ResetPoints()
            {
                index = 0;
            }
            /// <summary>
            /// Метод который возращает индекс
            /// </summary>
            /// <returns>текущий индекс</returns>
            public int GetCountPoints()
            {
                return index;
            }
            /// <summary>
            /// Метод который возращает массив
            /// </summary>
            /// <returns>возращает массив</returns>
            public Point[] GetPoints()
            {
                return points;
            }
        }

        private bool isMouse = false; //Проверка: Зажата ли левая кнопка мыши.
        private ArrayPoints arrayPoints = new ArrayPoints(2);

        Bitmap map = new Bitmap(100, 100); //переменная которая отвечает за хранение нашего изображения.
        Graphics graphics;

        Pen pen = new Pen(Color.Black, 3f); //Принимает цвет и толщину кисти.

        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

            //Округляет линию, чтобы она была более гладкая.
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }
        /// <summary>
        /// Пользователь зажал левою кнопки мыши.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
            arrayPoints.ResetPoints();
        }
        /// <summary>
        /// Пользователь отпустил левою кнопку мыши.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse) return; //Проверяем зажат ли у пользователя левая кнопка мыши.

            arrayPoints.SetPoint(e.X, e.Y); //Обращается к методу SetPoint в него передаются координаты x и y
            if(arrayPoints.GetCountPoints() >= 2) //Проверяем заполнили ли мы наши две точки
            {
                graphics.DrawLines(pen,arrayPoints.GetPoints());
                pictureBox1.Image = map;
                arrayPoints.SetPoint(e.X, e.Y);
            }
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

        /// <summary>
        /// Метод выбора цвета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorButton_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        /// <summary>
        /// Метод для кнопки "палитра"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Palette_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }
        /// <summary>
        /// Метод для кнопки "очистить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
        }
        /// <summary>
        /// Метод для парамента "выбор толщины"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }
        /// <summary>
        /// Метод для кнопки "сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(pictureBox1.Image == null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }
        }
    }
}
