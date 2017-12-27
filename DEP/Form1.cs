using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEP
{
    public partial class Form1 : Form
    {


        // Define variables to determine a ellipse
        struct Ellipse
        {
            // The point user press Mouse Down
            public Point start;
            // The point user let Mouse Up
            public Point end;
            // Direction comparing the end point and the start point
            public Direction direction;
        }

        struct xRectangle
        {
            // The point user press Mouse Down
            public Point start;
            // The point user let Mouse Up
            public Point end;
            // Direction comparing the end point and the start point
            public Direction direction;
        }

        // Four quadrants in coordinate
        enum Direction
        {
            One,
            Two,
            Three,
            Four
        }

        // Store all the ellipses
        List<Ellipse> mEllipses = new List<Ellipse>();
        List<xRectangle> mRectangles = new List<xRectangle>();
            // The current drawing ellipse
        Ellipse mEllipse;

        xRectangle mRectangle;
        // Whether Mouse is Down to draw ellipse
        bool mDrawing;

        public Form1()
        {
            InitializeComponent();
        }

        // Draw ellipse by its start, end points and direction
        private void DrawEllipse(Ellipse ellipse, PaintEventArgs e)
        {
            switch (ellipse.direction)
            {
                case Direction.One:
                    e.Graphics.DrawEllipse(Pens.Black, new Rectangle(new Point(ellipse.start.X,
                        ellipse.end.Y), new Size(ellipse.end.X - ellipse.start.X,
                        ellipse.start.Y - ellipse.end.Y)));
                    break;
                case Direction.Two:
                    e.Graphics.DrawEllipse(Pens.Black, new Rectangle(ellipse.end,
                        new Size(ellipse.start.X - ellipse.end.X,
                            ellipse.start.Y - ellipse.end.Y)));
                    break;
                case Direction.Three:
                    e.Graphics.DrawEllipse(Pens.Black, new Rectangle(new Point(ellipse.end.X,
                        ellipse.start.Y), new Size(ellipse.start.X - ellipse.end.X,
                        ellipse.end.Y - ellipse.start.Y)));
                    break;
                case Direction.Four:
                    e.Graphics.DrawEllipse(Pens.Black, new Rectangle(ellipse.start,
                        new Size(ellipse.end.X - ellipse.start.X,
                            ellipse.end.Y - ellipse.start.Y)));
                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }
        }


        // Draw ellipse by its start, end points and direction
        private void DrawRectangle(xRectangle rectangle, PaintEventArgs e)
        {
            switch (rectangle.direction)
            {
                case Direction.One:
                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(new Point(rectangle.start.X,
                        rectangle.end.Y), new Size(rectangle.end.X - rectangle.start.X,
                        rectangle.start.Y - rectangle.end.Y)));
                    break;
                case Direction.Two:
                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(rectangle.end,
                        new Size(rectangle.start.X - rectangle.end.X,
                            rectangle.start.Y - rectangle.end.Y)));
                    break;
                case Direction.Three:
                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(new Point(rectangle.end.X,
                        rectangle.start.Y), new Size(rectangle.start.X - rectangle.end.X,
                        rectangle.end.Y - rectangle.start.Y)));
                    break;
                case Direction.Four:
                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(rectangle.start,
                        new Size(rectangle.end.X - rectangle.start.X,
                            rectangle.end.Y - rectangle.start.Y)));
                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }
        }



        // Determine the direction of ellipse by start and end points
        private void DetermineDirection(MouseEventArgs e)
        {
            if (e.Y < 0 && e.X < 0)
            {
                mEllipse.end.X = 0;
                mEllipse.end.Y = 0;
            }
            else if (e.X < 0)
            {
                mEllipse.end.X = 0;
                mEllipse.end.Y = e.Y;
            }
            else if (e.Y < 0)
            {
                mEllipse.end.X = e.X;
                mEllipse.end.Y = 0;
            }
            else
                mEllipse.end = e.Location;

            if (mEllipse.end.X > mEllipse.start.X && mEllipse.end.Y <= mEllipse.start.Y)
                mEllipse.direction = Direction.One;
            else if (mEllipse.end.X <= mEllipse.start.X && mEllipse.end.Y < mEllipse.start.Y)
                mEllipse.direction = Direction.Two;
            else if (mEllipse.end.X < mEllipse.start.X && mEllipse.end.Y >= mEllipse.start.Y)
                mEllipse.direction = Direction.Three;
            else if (mEllipse.end.X >= mEllipse.start.X && mEllipse.end.Y > mEllipse.start.Y)
                mEllipse.direction = Direction.Four;
        }


        // Determine the direction of ellipse by start and end points
        private void DetermineDirectionSquare(MouseEventArgs e)
        {
            if (e.Y < 0 && e.X < 0)
            {
                mRectangle.end.X = 0;
                mRectangle.end.Y = 0;
            }
            else if (e.X < 0)
            {
                mRectangle.end.X = 0;
                mRectangle.end.Y = e.Y;
            }
            else if (e.Y < 0)
            {
                mRectangle.end.X = e.X;
                mRectangle.end.Y = 0;
            }
            else
                mRectangle.end = e.Location;

            if (mRectangle.end.X > mRectangle.start.X && mRectangle.end.Y <= mRectangle.start.Y)
                mRectangle.direction = Direction.One;
            else if (mRectangle.end.X <= mRectangle.start.X && mRectangle.end.Y < mRectangle.start.Y)
                mRectangle.direction = Direction.Two;
            else if (mRectangle.end.X < mRectangle.start.X && mRectangle.end.Y >= mRectangle.start.Y)
                mRectangle.direction = Direction.Three;
            else if (mRectangle.end.X >= mRectangle.start.X && mRectangle.end.Y > mRectangle.start.Y)
                mRectangle.direction = Direction.Four;
        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Ellipse ellipse in mEllipses)
            {
                this.DrawEllipse(ellipse, e);
            }
            foreach (xRectangle rectangle in mRectangles)
            {
                this.DrawRectangle(rectangle, e);
            }


            if (mDrawing)
            {
                if (Circle.Checked)
                {
                    this.DrawEllipse(mEllipse, e);
                }
                else
                {
                    this.DrawRectangle(mRectangle, e);
                }
            }
        }
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Circle.Checked)
            {
                Circle_Down(e);
            }
            else if(Square.Checked)
            {
                Square_Down(e);
            }
            else if (Select.Checked)
            {
                Select_Down(e);
            }
        }

        private void Select_Down(MouseEventArgs e)
        {
            foreach (Ellipse el in mEllipses)
            {
                int avgX = (el.start.X + el.end.X) / 2;
                int avgY = (el.start.Y + el.end.Y) / 2;
                Point p = new Point(avgX, avgY);
                if (p == e.Location)
                {
                    mEllipses.Remove(el);
                    break;
                }
            }
            
        }


        private void Square_Down(MouseEventArgs e)
        {
            mRectangle.start = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                mDrawing = true;
            }
            else
            {
                this.Invalidate();
            }
        }



        private void Circle_Down(MouseEventArgs e)
        {
        // Mouse Down to determine start point of an ellipse
            mEllipse.start = e.Location;
            if (e.Button == MouseButtons.Left)
                mDrawing = true;
            else
                this.Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Circle.Checked)
            {
                Circle_Up(e);
            }
            else
            {
                Square_Up(e);
            }
        }

        private void Square_Up(MouseEventArgs e)
        {
            if (!mDrawing) return;
            mDrawing = false;
            this.DetermineDirectionSquare(e);
            // Add the newly created ellipse into ellipse list
            mRectangles.Add(mRectangle);
            this.Invalidate();
        }


        private void Circle_Up(MouseEventArgs e)
        {
            if (!mDrawing) return;
            mDrawing = false;
            this.DetermineDirection(e);
            // Add the newly created ellipse into ellipse list
            mEllipses.Add(mEllipse);
            this.Invalidate();
        }
        
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Circle.Checked)
            {
                Circle_Move(e);
            }
            else
            {
                Square_Move(e);
            }
        }

        private void Square_Move(MouseEventArgs e)
        {
            if (!mDrawing) return;
            // When Mouse Move, determine the direction of an ellipse 
            // constantly
            this.DetermineDirectionSquare(e);
            this.Invalidate();
        }
        
        private void Circle_Move(MouseEventArgs e)
        {
            if (!mDrawing) return;
            // When Mouse Move, determine the direction of an ellipse 
            // constantly
            this.DetermineDirection(e);
            this.Invalidate();
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
