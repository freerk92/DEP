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
            if(figure != null)
            {
                foreach (var item in SaveData.Instance.CurrentDrawState.Figures)
                {
                    if(item.Equals(figure))
                    {
                        TopDecoration decoration = new TopDecoration(figure, DecoratorText.Text);
                        SaveData.Instance.DecorationList.Add(decoration);
                    }
                }
            }
            form.Refresh();
            this.Hide();
        }

        public DrawState CloneDrawState()
        {
            var figures = new List<Figure>();
            foreach (var item in SaveData.Instance.CurrentDrawState.Figures)
            {
                figures.Add(new Figure(item));
            }

            var groups = new List<Group>();
            foreach (var item in SaveData.Instance.CurrentDrawState.Groups)
            {
                groups.Add(new Group(item));
            }


            var drawState = new DrawState(figures, groups);
            return drawState;
        }
    }
}
