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

        Figure figure;
        Group SelectedGroup;
        Figure SelectedFigure;
        // Whether Mouse is Down to draw ellipse
        bool mDrawing;
        bool figureSelected= true;

        public Form1()
        {
            SaveData.Instance.CurrentDrawState = new DrawState(new List<Figure>(), new List<Group>());
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
            foreach (Figure drawFigure in SaveData.Instance.CurrentDrawState.Figures)
            {
                drawFigure.Draw(e);
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

        public DrawState CloneDrawState()
        {
            var figures = new List<Figure>();
            foreach (var item in SaveData.Instance.CurrentDrawState.Figures)
            {
                if (item.GetType().Equals(typeof(xRectangle)))
                {
                    figures.Add(new xRectangle(item));
                }
                else
                {
                    figures.Add(new Ellipse(item));
                }
            }

            var groups = new List<Group>();
            foreach(var item in SaveData.Instance.CurrentDrawState.Groups)
            {
                groups.Add(new Group(item));
            }


            var drawState = new DrawState(figures, groups);
            return drawState;
        }

        private void Resize_Down(MouseEventArgs e)
        {
            SaveData.Instance.HistoryList.Add(CloneDrawState());
            var newFigures = new List<Figure>(SaveData.Instance.CurrentDrawState.Figures);
            var visitor = new ResizeVisitor(e);

            if (figureSelected && SelectedFigure != null)
            {
                foreach (var item in newFigures)
                {
                    if (item.Equals(SelectedFigure))
                    {
                        item.Accept(visitor);
                        SelectedFigure = SetSelectedFigure(item);
                    }
                }
            }
            else if(!figureSelected && SelectedGroup != null)
            {
                try{
                    SelectedGroup.Accept(visitor);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Negatief resizen is voor groepen niet ondersteund", "Deze actie is niet mogelijk", MessageBoxButtons.OK);
                }
         
            }

            this.Refresh();
        }

        private Figure AddOffsetToResize(Figure mainItem, Figure item, double xDifference, double yDifference)
        {
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

        private void Move_Down(MouseEventArgs e)
        {
            SaveData.Instance.HistoryList.Add(CloneDrawState());
            var newFigures = new List<Figure>(SaveData.Instance.CurrentDrawState.Figures);
            var visitor = new MoveVisitor(e);



            if (figureSelected && SelectedFigure != null)
            {
                foreach (var item in newFigures)
                {
                    if(item.Equals(SelectedFigure))
                    {
                        item.Accept(visitor);
                        SelectedFigure = SetSelectedFigure(item);
                    }
                }
            }
            else if(!figureSelected && SelectedGroup != null)
            {
                SelectedGroup.Accept(visitor);
                SetSelectedGroup();
                Color_Group();
            }
            this.Refresh();
        }

        private void Select_Down(MouseEventArgs e)
        { 
            var selectedFigure = SaveData.Instance.CurrentDrawState.Figures.FirstOrDefault(figure =>
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


            SelectedFigure = SetSelectedFigure(selectedFigure);


            foreach (var item in SaveData.Instance.CurrentDrawState.Figures)
            {
                if(!item.Equals(SelectedFigure))
                {
                    item.IsSelected = false;
                }
                else
                {
                    item.IsSelected = true;
                }
            }
            Refresh();
        }

        private Figure SetSelectedFigure(Figure selectedFigure)
        {
            if(selectedFigure.GetType().Equals(typeof(xRectangle)))
            {
                return new xRectangle(selectedFigure);
            }

            return new Ellipse(selectedFigure);
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

            SaveData.Instance.HistoryList.Add(CloneDrawState());
            var newFigures = CloneDrawState();
            newFigures.Figures.Add(figure);
            SaveData.Instance.CurrentDrawState = newFigures;
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
            var v = (Button)sender;
            v.Click -= Undo_Click;
            if(SaveData.Instance.CurrentDrawState.Figures.Count > 0)
            {
                inv.PressButtonOn(2);
                Refresh();
            }
            Undo.Click += Undo_Click;
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            Redo.Click -= Redo_Click;
            if(SaveData.Instance.FutureList.Count > 0)
            inv.PressButtonOn(3);
            Refresh();
            Redo.Click += Redo_Click;
        }

        private void AddToGroupButton_Click(object sender, EventArgs e)
        {
            SaveData.Instance.HistoryList.Add(CloneDrawState());
            var newDrawState = new DrawState(SaveData.Instance.CurrentDrawState);

            //Check of de groep al bestaat, als de groep bestaat wordt wordt de index opgeslagen in existingIndex
            int index = (int)numericUpDown1.Value;
            bool existingIndex = false;
            int? GroupIndex = null;
            for (int i = 0; i < newDrawState.Groups.Count; i++)
            {
                if (newDrawState.Groups[i].ID == index)
                {
                    existingIndex = true;
                    GroupIndex = i;
                }
            }

            //Voeg figuur toe aan groep
            if(figureSelected && figure != null)
            {
                //Groep bestaat
                if (!existingIndex)
                {
                   newDrawState.Groups.Add(new Group(index));
                }

                for (int i = 0; i < newDrawState.Figures.Count; i++)
                {
                    if (newDrawState.Figures[i].Equals(SelectedFigure))
                    {
                        newDrawState.Figures[i].group = GetGroupWithID(index);
                    }
                }
            }
            //Voeg groep toe aan groep
            else if(!figureSelected && SelectedGroup != null)
            {
                if (SelectedGroup.IsInGroup == null)
                {
                    newDrawState.Groups[GroupIndex.Value].AddToGroup(SelectedGroup);
                    SelectedGroup.IsInGroup = GroupIndex;
                }
            }

            SaveData.Instance.CurrentDrawState = new DrawState(newDrawState);
        }

        private Group GetGroupWithID(int index)
        {
            Group group = null;
            var currentDrawState = new DrawState(SaveData.Instance.CurrentDrawState);
            foreach (var item in currentDrawState.Groups)
            {
                if (item.ID == index)
                    group = item;
            }

            return group;
        }

        private void SelectGroup_Click(object sender, EventArgs e)
        {
            SetSelectedGroup();
            if(SelectedGroup != null)
            {
                Color_Group();
            }
            this.Refresh();
        }

        private void ResetColors()
        {
            foreach (var item in SaveData.Instance.CurrentDrawState.Figures)
            {
                item.IsSelected = false;
                item.IsMainGroupFigure = false;
                item.IsUnderlyingGroup = false;
                item.IsSelectedInGroup = false;
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
                {
                    SetSelectedGroup();
                    Color_Group();
                }
            }
            else
            {
                checkBox.Text = "Figure";
                figureSelected = true;
                var currentDrawState = SaveData.Instance.CurrentDrawState;
                Color_Figures(currentDrawState);           
            }
            Refresh();
        }

        private void SetSelectedGroup()
        {
            var currentDrawState = new DrawState(SaveData.Instance.CurrentDrawState);
            int index = (int)numericUpDown1.Value;
            int? existingIndex = null;
            for (int i = 0; i < currentDrawState.Groups.Count; i++)
            {
                if (currentDrawState.Groups[i].ID == index)
                {
                    existingIndex = i;
                }
            }

            if (existingIndex.HasValue)
            {
                SelectedGroup = currentDrawState.Groups[existingIndex.Value];
            }
        }

        private void Color_Group()
        {
            bool FirstItem = true;
            var Figures = SaveData.Instance.CurrentDrawState.Figures;
            var GroupIDsList = FindUnderlyingGroupIDS(SelectedGroup);
            ResetColors();

            foreach (var item in Figures)
            {
                if (item.group != null && item.group.ID == SelectedGroup.ID)
                {
                    if (FirstItem)
                    {
                        item.IsMainGroupFigure = true;
                        FirstItem = false;
                    }
                    item.IsSelectedInGroup = true;
                }
                else if(item.group != null && GroupIDsList.Contains(item.group.ID))
                {
                    item.IsUnderlyingGroup = true;
                }
            }
        }

        public List<int> FindUnderlyingGroupIDS(Group group)
        {
            var list = new List<int>();

            foreach (var item in group.Groups)
            {
                list.Add(item.ID);
                list.AddRange(FindUnderlyingGroupIDS(item));
            }
            return list;
        }

        public void Color_Figures(DrawState currentDrawState)
        {
            foreach (var item in currentDrawState.Figures)
            {
                if (item.Equals(SelectedFigure))
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
