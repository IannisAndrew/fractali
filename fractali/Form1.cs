using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fractali
{
    public partial class Form1 : Form
    {
        private double zoom;
        private double centerX;
        private double centerY;
        private int MaxIterations = 1000;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // To reduce flickering

            zoom = 200;
            centerX = -0.5;
            centerY = 0;

           // this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
        }

        private int Mandelbrot(double real, double imaginary, int maxIterations)
        {
            double zr = 0.0;
            double zi = 0.0;
            int iterations = 0;

            while (zr * zr + zi * zi < 4.0 && iterations < maxIterations)
            {
                double temp = zr * zr - zi * zi + real;
                zi = 2.0 * zr * zi + imaginary;
                zr = temp;
                iterations++;
            }

            return iterations;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //DrawMandelbrot(e.Graphics);
        }

        private void DrawMandelbrot(Graphics graphics)
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double real = centerX + (x - width / 2) / zoom;
                    double imaginary = centerY + (y - height / 2) / zoom;
                    int iterations = Mandelbrot(real, imaginary, MaxIterations);

                    Color color;
                    if (iterations == MaxIterations)
                    {
                        color = Color.Black;
                    }
                    else
                    {
                        int c = 255 - (int)(255.0 * iterations / MaxIterations);
                        color = Color.FromArgb(255, c,Math.Abs(c-200), 255 - c);
                    }

                    using (Pen pen = new Pen(color))
                    {
                        graphics.DrawLine(pen, x, y, x + 1, y); // Draw a line of 1 pixel length
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

 
        private void button1_Click(object sender, EventArgs e)
        {
            MaxIterations=int.Parse(textBox1.Text);
            zoom=int.Parse(textBox2.Text);
            //Invalidate();
            DrawMandelbrot(this.CreateGraphics());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Invalidate();
        }
    }

}
