using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static DEP.Figure;
using DEP.Commands;
using System.Drawing;

namespace DEP
{
    public partial class Form1 : Form
    {
        static Receiver receiver = new Receiver();
        SaveCommand save = new SaveCommand(receiver);
        LoadCommand load = new LoadCommand(receiver);
        UndoCommand undo = new UndoCommand(receiver);
        RedoCommand redo = new RedoCommand(receiver);

        Invoker inv = new Invoker();

        public List<Group> Groups = new List<Group>();

        Figure figure;
        Group SelectedGroup;
        Figure SelectedFigure;
        // Whether Mouse is Down to draw ellipse
        bool mDrawing;
        bool figureSelected= true;

        public Form1()
        {
            SaveData.Instance.HistoryList.Add(new List<Figure>());
            inv.InsertCommands(save);
            inv.InsertCommands(load);
            inv.InsertCommands(undo);
            inv.InsertCommands(redo);
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
            Refresh();
            if (mDrawing)
            {
                figure.Draw(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (Figure figuretje in SaveData.Instance.Figures)
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
            else if(Resize.Checked)
            {
                Resize_Down(e);
            }
            else
            {
                Figure_Down(e);
            }
        }

        private void Resize_Down(MouseEventArgs e)
        {
            var currentFigures = SaveData.Instance.Figures;
            if (figureSelected && SelectedFigure != null)
            {
                foreach (var item in currentFigures)
                {
                    if (item == SelectedFigure)
                    {
                        item.end = e.Location;
                    }
                }
            }
            else if(!figureSelected && SelectedGroup != null)
            {
                var mainFigure = SelectedGroup.Figures.First();
                var width = mainFigure.end.X-mainFigure.start.X;
                var height = mainFigure.end.Y - mainFigure.start.Y;
                var EndPoint = mainFigure.end;
                var StartPoint = mainFigure.start;

                var XDifference = 1.0 - ((double)EndPoint.X - (double)e.X)/width;
                var YDifference = 1.0 - ((double)EndPoint.Y - (double)e.Y)/height;
                


                foreach (var item in SelectedGroup.Figures)
                {
                    var index = currentFigures.IndexOf(item);
                    var resizedItem = resizeFigure(item, XDifference, YDifference);
                    if (!item.IsMainGroupFigure)
                        resizedItem = AddOffsetToResize(resizedItem, XDifference, YDifference);
                    currentFigures[index] = resizedItem;
                }               
            }

            StoreChange(currentFigures);
            this.Refresh();
        }

        private Figure AddOffsetToResize(Figure item, double xDifference, double yDifference)
        {
            var mainItem = SelectedGroup.Figures.First();
            var offsetX = item.start.X - mainItem.start.X;
            var offSetY = item.start.Y - mainItem.start.Y;
            var newOffsetX = offsetX * Math.Abs(xDifference);
            var newOffsetY = offsetX * Math.Abs(yDifference);

            int newStartX;
            int newEndX;
            int newStartY;
            int newEndY;

            var compareX = (int)(xDifference * 100);
            var compareY = (int)(yDifference * 100); 
            if (compareX < 100)
            {
                newStartX = (int)(item.start.X - newOffsetX);
                newEndX = (int)(item.end.X - newOffsetX);
            }
            else
            {
                newStartX = (int)(item.start.X + newOffsetX);
                newEndX = (int)(item.end.X + newOffsetX);
            }

            if (compareY < 100)
            {
                newStartY = (int)(item.start.Y - newOffsetY);
                newEndY = (int)(item.end.Y - newOffsetY);
            }
            else
            {
                newStartY = (int)(item.start.Y + newOffsetY);
                newEndY = (int)(item.end.Y + newOffsetY);
            }

            var offsetStart = new Point(newStartX, newStartY);
            var offsetEnd = new Point(newEndX, newEndY);

            item.start = offsetStart;
            item.end = offsetEnd;

            return item;
        }

        public void StoreChange(List<Figure> currentFigures)
        {
            SaveData.Instance.HistoryList.Add(new List<Figure>(SaveData.Instance.Figures));
            SaveData.Instance.Figures = new List<Figure>(currentFigures);
        }

        private Figure resizeFigure(Figure item, double xDifference, double yDifference)
        {
            var width = item.end.X - item.start.X;
            var height = (item.end.Y - item.start.Y);
            var ItemSizeX = Math.Abs(width*xDifference);
            var ItemSizeY = Math.Abs(height * yDifference);
            int newX = Math.Abs((int)(item.start.X + ItemSizeX));
            int newY = Math.Abs((int)(item.start.Y + ItemSizeY));
            item.end = new Point(newX, newY);
            return item;
        }

        private void Move_Down(MouseEventArgs e)
        {
            if (figureSelected && SelectedFigure != null)
            {
                var currentFigures = SaveData.Instance.Figures;
                foreach (var item in currentFigures)
                {
                    if(item == SelectedFigure)
                    {
                        item.Move(e.Location);
                    }
                }
                StoreChange(currentFigures);
            }
            else if(!figureSelected && SelectedGroup != null)
            {
                var currentFigures = SaveData.Instance.Figures;
                var StartPoint = SelectedGroup.Figures[0].start;
                var EndPoint = e.Location;
                var XDifference = StartPoint.X - EndPoint.X;
                var YDifference = StartPoint.Y - EndPoint.Y;

                foreach (var item in SelectedGroup.Figures)
                {
                    foreach (var figure in currentFigures)
                    {
                        if (figure == item)
                        {
                            figure.start = new Point(figure.start.X - XDifference, figure.start.Y - YDifference);
                            figure.end = new Point(figure.end.X - XDifference, figure.end.Y - YDifference);
                        }
                    }
                }
                StoreChange(currentFigures);
            }
            this.Refresh();
        }

        private void Select_Down(MouseEventArgs e)
        {
            SelectedFigure = SaveData.Instance.Figures.FirstOrDefault(figure =>
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

            if (SelectedFigure != null)
            {
                SelectedFigure.IsSelected = true;
            }

            foreach (var item in SaveData.Instance.Figures)
            {
                if(item != SelectedFigure)
                {
                    item.IsSelected = false;
                }
            }
            Refresh();
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
            SaveData.Instance.Figures.Add(figure);
            SaveData.Instance.HistoryList.Add(new List<Figure>(SaveData.Instance.Figures));
            SaveData.Instance.FutureList.Clear();
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
            inv.PressButtonOn(0);
        }

        private void Load_Click(object sender, EventArgs e)
        {
            inv.PressButtonOn(1);
            Refresh();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if(SaveData.Instance.Figures.Count > 0)
            {
                inv.PressButtonOn(2);
                Refresh();
            }
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            inv.PressButtonOn(3);
            Refresh();
        }

        private void AddToGroupButton_Click(object sender, EventArgs e)
        {
            int index = (int)numericUpDown1.Value;
            int? existingIndex = null;
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].ID == index)
                {
                    existingIndex = i;
                }
            }

            if(figureSelected && figure != null)
            {
                if (existingIndex.HasValue)
                {
                    if (!Groups[existingIndex.Value].Figures.Contains(SelectedFigure))
                        Groups[existingIndex.Value].AddToGroup(SelectedFigure);
                }
                else
                {
                    Groups.Add(new Group(index));
                    Groups.Last().Figures.Add(SelectedFigure);
                }

                var list = SaveData.Instance.Figures;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Equals(SelectedFigure))
                    {
                        list[i].group = GetGroupWithID(index);
                    }
                }
            }
            else if(!figureSelected && SelectedGroup != null)
            {
                if (SelectedGroup.IsInGroup == null)
                {
                    Groups[existingIndex.Value].AddToGroup(SelectedGroup);
                    SelectedGroup.IsInGroup = existingIndex;
                }


            }


        }

        private Group GetGroupWithID(int index)
        {
            Group group = null;
            foreach (var item in Groups)
            {
                if (item.ID == index)
                    group = item;
            }

            return group;
        }

        private void SelectGroup_Click(object sender, EventArgs e)
        {
            
            int index = (int)numericUpDown1.Value;
            int? existingIndex = null;
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].ID == index)
                {
                    existingIndex = i;
                }
            }

            if(existingIndex.HasValue)
            {
                SelectedGroup = Groups[existingIndex.Value];
                Color_Group(SelectedGroup);
            }
            this.Refresh();
        }

        private void ResetColors()
        {
            foreach (var item in SaveData.Instance.Figures)
            {
                item.IsSelected = false;
                item.IsMainGroupFigure = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if(checkBox.Checked)
            {
                checkBox.Text = "Group";
                figureSelected = false;
                if (SelectedGroup != null)
                    Color_Group(SelectedGroup);
            }
            else
            {
                checkBox.Text = "Figure";
                figureSelected = true;
                var currentFigures = SaveData.Instance.Figures;
                Color_Figures(currentFigures);           
            }
            Refresh();
        }

        private void Color_Group(Group group)
        {
            var FirstItem = SelectedGroup.Figures[0];
            var list = SaveData.Instance.Figures;
            foreach (var item in list)
            {
                if (item.group != null && item.group == group)
                {
                    if (FirstItem == item)
                        item.IsMainGroupFigure = true;
                    
                    item.IsSelected = true;

                }
                else
                {
                    item.IsSelected = false;
                }
            }
        }

        public void Color_Figures(List<Figure> FiguresList)
        {
            foreach (var item in FiguresList)
            {
                if (item == SelectedFigure)
                {
                    item.IsSelected = true;
                }
                else
                {
                    item.IsSelected = false;
                }
            }
        }
    }
}
