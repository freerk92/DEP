using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEP
{
    class Receiver
    {
        public void Undo()
        {
            SaveData.Instance.UndoStack.Add(SaveData.Instance.Figures.Last());
            SaveData.Instance.Figures.Remove(SaveData.Instance.Figures.Last());
        }

        public void Redo()
        {
            SaveData.Instance.Figures.Add(SaveData.Instance.UndoStack.Last());
            SaveData.Instance.UndoStack.Remove(SaveData.Instance.UndoStack.Last());
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
