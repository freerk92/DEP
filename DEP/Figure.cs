using System;
using System.Drawing;
using System.Windows.Forms;

namespace DEP
{
    public abstract class Figure
    {
        // The point user press Mouse Down
        public Point start;
        // The point user let Mouse Up
        public Point end;
        // Direction comparing the end point and the start point
        public Direction direction;
        public bool IsSelected { get; set; }
        public abstract void Draw(PaintEventArgs e);

        public void Move(Point location)
        {
            int x = Math.Abs(start.X - end.X);
            int y = Math.Abs(start.Y - end.Y);

            start = location;
            end = new Point(location.X + x, location.Y + y);
        }


        // Four quadrants in coordinate
        public enum Direction
        {
            One,
            Two,
            Three,
            Four
        }

        public class Ellipse : Figure
        {
            public bool IsSelected { get; set; }
            // Drawthis by its start, end points and direction
            public override void Draw(PaintEventArgs e)
            {
                Pen color = IsSelected ? Pens.Red : Pens.Black;

                switch (this.direction)
                {
                    case Direction.One:
                        e.Graphics.DrawEllipse(color, new Rectangle(new Point(this.start.X,
                           this.end.Y), new Size(this.end.X - this.start.X,
                           this.start.Y - this.end.Y)));
                        break;
                    case Direction.Two:
                        e.Graphics.DrawEllipse(color, new Rectangle(this.end,
                            new Size(this.start.X - this.end.X,
                               this.start.Y - this.end.Y)));
                        break;
                    case Direction.Three:
                        e.Graphics.DrawEllipse(color, new Rectangle(new Point(this.end.X,
                           this.start.Y), new Size(this.start.X - this.end.X,
                           this.end.Y - this.start.Y)));
                        break;
                    case Direction.Four:
                        e.Graphics.DrawEllipse(color, new Rectangle(this.start,
                            new Size(this.end.X - this.start.X,
                               this.end.Y - this.start.Y)));
                        break;
                    default:
                        MessageBox.Show("Error");
                        break;
                }
            }
        }

        public class xRectangle : Figure
        {
            // Draw this by its start, end points and direction
            public override void Draw(PaintEventArgs e)
            {
                Pen color = IsSelected ? Pens.Red : Pens.Black;

                switch (this.direction)
                {
                    case Direction.One:
                        e.Graphics.DrawRectangle(color, new Rectangle(new Point(this.start.X,
                           this.end.Y), new Size(this.end.X - this.start.X,
                           this.start.Y - this.end.Y)));
                        break;
                    case Direction.Two:
                        e.Graphics.DrawRectangle(color, new Rectangle(this.end,
                            new Size(this.start.X - this.end.X,
                               this.start.Y - this.end.Y)));
                        break;
                    case Direction.Three:
                        e.Graphics.DrawRectangle(color, new Rectangle(new Point(this.end.X,
                           this.start.Y), new Size(this.start.X - this.end.X,
                           this.end.Y - this.start.Y)));
                        break;
                    case Direction.Four:
                        e.Graphics.DrawRectangle(color, new Rectangle(this.start,
                            new Size(this.end.X - this.start.X,
                               this.end.Y - this.start.Y)));
                        break;
                    default:
                        MessageBox.Show("Error");
                        break;
                }
            }

        }
    }
}