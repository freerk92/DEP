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

        public abstract void Draw(PaintEventArgs e);



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
            // Drawthis by its start, end points and direction
            public override void Draw(PaintEventArgs e)
            {
                switch (this.direction)
                {
                    case Direction.One:
                        e.Graphics.DrawEllipse(Pens.Black, new Rectangle(new Point(this.start.X,
                           this.end.Y), new Size(this.end.X - this.start.X,
                           this.start.Y - this.end.Y)));
                        break;
                    case Direction.Two:
                        e.Graphics.DrawEllipse(Pens.Black, new Rectangle(this.end,
                            new Size(this.start.X - this.end.X,
                               this.start.Y - this.end.Y)));
                        break;
                    case Direction.Three:
                        e.Graphics.DrawEllipse(Pens.Black, new Rectangle(new Point(this.end.X,
                           this.start.Y), new Size(this.start.X - this.end.X,
                           this.end.Y - this.start.Y)));
                        break;
                    case Direction.Four:
                        e.Graphics.DrawEllipse(Pens.Black, new Rectangle(this.start,
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
            // Drawthis by its start, end points and direction
            public override void Draw(PaintEventArgs e)
            {
                switch (this.direction)
                {
                    case Direction.One:
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(new Point(this.start.X,
                            this.end.Y), new Size(this.end.X - this.start.X,
                            this.start.Y - this.end.Y)));
                        break;
                    case Direction.Two:
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(this.end,
                            new Size(this.start.X - this.end.X,
                                this.start.Y - this.end.Y)));
                        break;
                    case Direction.Three:
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(new Point(this.end.X,
                            this.start.Y), new Size(this.start.X - this.end.X,
                            this.end.Y - this.start.Y)));
                        break;
                    case Direction.Four:
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(this.start,
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