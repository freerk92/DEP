using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEP
{
    class Receiver
    {
        SaveData DataLink = SaveData.Instance;

        public void Undo()
        {
            if(DataLink.HistoryList.Count > 0)
            {
                DataLink.FutureList.Add(new List<Figure>(DataLink.Figures));
                DataLink.Figures = new List<Figure>(DataLink.HistoryList.Last());
                DataLink.HistoryList.Remove(DataLink.HistoryList.Last());
            }
        }

        public void Redo()
        {
            if(DataLink.FutureList.Count > 0)
            {
                DataLink.HistoryList.Add(new List<Figure>(DataLink.Figures));
                DataLink.Figures = new List<Figure>(DataLink.FutureList.Last());
                DataLink.FutureList.Remove(DataLink.FutureList.Last());
            }
        }
        
        public void Save()
        {
            FileManager manager = new FileManager();
            manager.Save();
        }

        public void Load()
        {
            FileManager manager = new FileManager();
            manager.Load();
        }
    }
}
