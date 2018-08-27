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

        private List<List<Figure>> historyList = new List<List<Figure>>();

        public List<List<Figure>> HistoryList
        {
            get
            {
                return historyList;
            }
            set
            {
                historyList = value;
            }
        }

        private List<List<Figure>> futureList = new List<List<Figure>>();

        public List<List<Figure>> FutureList
        {
            get
            {
                return futureList;
            }
            set
            {
                futureList = value;
            }
        }


        private List<Group> groups = new List<Group>();

        public List<Group> Groups
        {
            get
            {
                return groups;
            }
            set
            {
                groups = value;
            }
        }
    }
}
