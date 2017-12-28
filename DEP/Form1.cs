using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static DEP.Figure;

namespace DEP
{
    public partial class Form1 : Form
    {

        List<Figure> Figures = new List<Figure>();

        Figure figure;
        // Whether Mouse is Down to draw ellipse
        bool mDrawing;

        public Form1()
        {
            InitializeComponent();
        }





        // Determine the direction of ellipse by start and end points

        private void DetermineDirection(MouseEventArgs e)

        {

            if (e.Y < 0 && e.X < 0)

            {

                figure.end.X = 0;
                figure.end.Y = 0;
            }
            else if (e.X < 0)
            {
                figure.end.X = 0;
                figure.end.Y = e.Y;
            }
            else if (e.Y < 0)
            {
                figure.end.X = e.X;
                figure.end.Y = 0;
            }
            else
                figure.end = e.Location;

            if (figure.end.X > figure.start.X && figure.end.Y <= figure.start.Y)
                figure.direction = Direction.One;
            else if (figure.end.X <= figure.start.X && figure.end.Y < figure.start.Y)
                figure.direction = Direction.Two;
            else if (figure.end.X < figure.start.X && figure.end.Y >= figure.start.Y)
                figure.direction = Direction.Three;
            else if (figure.end.X >= figure.start.X && figure.end.Y > figure.start.Y)
                figure.direction = Direction.Four;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.Refresh();
            if (mDrawing)
            {
                figure.Draw(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (Figure figuretje in Figures)
            {
                figuretje.Draw(e);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Select.Checked)
            {
                Select_Down(e);
            }
            else if (Move.Checked)
            {
                Move_Down(e);
            }
            else
            {
                Figure_Down(e);
            }
        }

        private void Move_Down(MouseEventArgs e)
        {
            if (figure != null)
            {
                figure.Move(e.Location);
            }

            this.Refresh();
        }

        private void Select_Down(MouseEventArgs e)
        {
            figure = Figures.FirstOrDefault(figure =>
            {
                if (Enumerable.Range(figure.start.X, figure.end.X).Contains(e.Location.X) || Enumerable.Range(figure.end.X, figure.start.X).Contains(e.Location.X))
                {
                    if (Enumerable.Range(figure.start.Y, figure.end.Y).Contains(e.Location.Y) || Enumerable.Range(figure.end.Y, figure.start.Y).Contains(e.Location.Y))
                    {
                        return true;
                    }
                }
                return false;
            });

            if (figure == null)
            {
                Circle.Checked = true;
            }
        }


        private void Figure_Down(MouseEventArgs e)
        {
            if (Circle.Checked)
            {
                figure = new Ellipse();
            }
            else
            {
                figure = new xRectangle();
            }
            figure.start = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                mDrawing = true;
            }
            else
            {
                this.Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Figure_Up(e);
        }

        private void Figure_Up(MouseEventArgs e)
        {
            if (!mDrawing) return;
            mDrawing = false;
            this.DetermineDirection(e);
            // Add the newly created ellipse into ellipse list
            Figures.Add(figure);
            this.Invalidate();
        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Figure_Move(e);

        }

        private void Figure_Move(MouseEventArgs e)
        {
            if (!mDrawing) return;
            // When Mouse Move, determine the direction of an ellipse
            // constantly
            this.DetermineDirection(e);
            this.Invalidate();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            FileManager manager = new FileManager();
            manager.Save(Figures);
        }

        private void Load_Click(object sender, EventArgs e)
        {
            FileManager manager = new FileManager();
            var x = manager.Load();
            Figures = x;
            Refresh();
        }
    }
}
