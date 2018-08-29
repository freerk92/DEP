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

        private DrawState currentDrawState = new DrawState();

        public DrawState CurrentDrawState
        {
            get
            {
                return currentDrawState;
            }
            set
            {
                currentDrawState = value;
            }
        }

        private List<DrawState> historyList = new List<DrawState>();

        public List<DrawState> HistoryList
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

        private List<DrawState> futureList = new List<DrawState>();

        public List<DrawState> FutureList
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

    }
}
