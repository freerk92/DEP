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
    public partial class DecoratorPopUp : Form
    {
        Figure figure;
        Group group;
        Form form;
        public DecoratorPopUp(Figure figure, Form form)
        {
            this.form = form;
            this.figure = figure;
            InitializeComponent();
        }

        public DecoratorPopUp(Group group, Form form)
        {
            this.form = form;
            this.group = group;
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void Save_Click(object sender, EventArgs e)
        {
            SaveData.Instance.HistoryList.Add(CloneDrawState());
            var newDrawState = new DrawState(CloneDrawState());

            if(figure != null)
            {
                newDrawState = AddDecoratorToFigure(newDrawState);
            }
            else{
                newDrawState = AddDecoratorToGroup(newDrawState);
            }
            SaveData.Instance.CurrentDrawState = new DrawState(newDrawState);
            form.Refresh();
            this.Hide();
        }

        private DrawState AddDecoratorToGroup(DrawState newDrawState)
        {
            foreach (var item in newDrawState.Groups)
            {
                if (item.ID == group.ID)
                {
                    DecorationPosition position = GetPosition();
                    Decoration decoration = new Decoration(group, DecoratorText.Text, position);
                    newDrawState.Decorations.Add(decoration);
                }
            }
            return newDrawState;
        }

        private DrawState AddDecoratorToFigure(DrawState newDrawState)
        {
            foreach (var item in newDrawState.Figures)
            {
                if (item.Equals(figure))
                {
                    DecorationPosition position = GetPosition();
                    Decoration decoration = new Decoration(figure, DecoratorText.Text, position);
                    newDrawState.Decorations.Add(decoration);
                }
            }
            return newDrawState;
        }

        private DecorationPosition GetPosition()
        {
            if (Bottom.Checked)
                return DecorationPosition.Bottom;
            if (Left.Checked)
                return DecorationPosition.Left;
            if (Right.Checked)
                return DecorationPosition.Right;

            return DecorationPosition.Top;
        }

        public DrawState CloneDrawState()
        {
            var OriginalDrawState = SaveData.Instance.CurrentDrawState;
            var figures = new List<Figure>();
            foreach (var item in OriginalDrawState.Figures)
            {
                figures.Add(new Figure(item));
            }

            var groups = new List<Group>();
            foreach (var item in OriginalDrawState.Groups)
            {
                groups.Add(new Group(item));
            }

            var decorations = new List<Decoration>();
            foreach (var item in OriginalDrawState.Decorations)
            {
                decorations.Add(new Decoration(item));
            }

            var drawState = new DrawState(figures, groups, decorations);
            return drawState;
        }
    }
}
