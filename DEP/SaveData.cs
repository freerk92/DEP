using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEP
{
    public sealed class SaveData
    {
        private static readonly SaveData instance = new SaveData();

        private SaveData() { }

        public static SaveData Instance
        {
            get
            {
                return instance;
            }
        }

        private List<Figure> figures = new List<Figure>();

        public List<Figure> Figures
        {
            get
            {
                return figures;
            }
            set
            {
                figures = value;
            }
        }

        private List<Figure> undoStack = new List<Figure>();

        public List<Figure> UndoStack
        {
            get
            {
                return undoStack;
            }
            set
            {
                undoStack = value;
            }
        }
    }
}
