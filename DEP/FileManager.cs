using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static DEP.Figure;

namespace DEP
{
    public class FileManager
    {
        public FileManager()
        {
            
        }

        public void Save()
        {
            var figures = SaveData.Instance.Figures;
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "txt files (*.txt)|*.txt";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;
                dialog.DefaultExt = "txt";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Can use dialog.FileName
                    using (StreamWriter stream = new StreamWriter(dialog.FileName))
                    {
                        // Save data
                        var saveData = ConvertListToIO(figures);
                        stream.WriteLine(saveData);
                    }
                }
            }
        }

        public void Load()
        {
            var test = new List<Figure>();
            using(OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Txt files (*.txt)|*.txt";
                // Show the dialog and get result.
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    using(StreamReader stream = new StreamReader(dialog.FileName))
                    {
                        var SaveFileStream = stream.ReadToEnd();
                        test = DecryptIO(SaveFileStream);
                        Console.WriteLine(SaveFileStream);
                    }
                }
            }
            SaveData.Instance.Figures = test;
            SaveData.Instance.HistoryList.Add(new List<Figure>(test));
        }

        private List<Figure> DecryptIO(string SaveFile)
        {
            var tempList = SaveFile.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
            );

            var returnList = new List<Figure>();

            foreach (var item in tempList)
            {
                if (string.IsNullOrEmpty(item))
                    break;
                Figure figure;
                var itemList = item.Split(' ');
                var startX = int.Parse(itemList[1]);
                var startY = int.Parse(itemList[2]);
                var width = int.Parse(itemList[3]);
                var height = int.Parse(itemList[4]);


                if(item.Contains("rectangle"))
                {
                    figure = new xRectangle();
                }
                else if(item.Contains("ellipse")){
                    figure = new Ellipse();
                }
                else{
                    figure = new Ellipse();
                }
                figure.start = new Point(startX, startY);
                figure.end = new Point(startX + width, startY + height);
                figure.direction = DetermineDirection(figure);
                returnList.Add(figure);
            }



            return returnList;
        }

        private Direction DetermineDirection(Figure figure)
        {
            if (figure.end.X > figure.start.X && figure.end.Y <= figure.start.Y)
                return Direction.One;
            else if (figure.end.X <= figure.start.X && figure.end.Y < figure.start.Y)
                return Direction.Two;
            else if (figure.end.X < figure.start.X && figure.end.Y >= figure.start.Y)
                return Direction.Three;
            else if (figure.end.X >= figure.start.X && figure.end.Y > figure.start.Y)
                return Direction.Four;

            return Direction.One;
        }


        private string ConvertListToIO(List<Figure> figures)
        {
            string SaveData = "";
            List<Figure> GrouplessFigures = new List<Figure>();
            List<Group> Groups = new List<Group>();

            foreach(var item in figures)
            {
                if (item.group == null)
                    GrouplessFigures.Add(item);
            }

            SaveData = GrouplessFiguresToSaveData(GrouplessFigures, 0);
            SaveData += GroupedFiguresToSaveData();
            
            return SaveData;
        }

        private string GrouplessFiguresToSaveData(List<Figure> GrouplessFigures, int Tabs)
        {
            string SaveData = "";
            foreach (var item in GrouplessFigures)
            {
                int width = Math.Abs(item.start.X - item.end.X);
                int height = Math.Abs(item.start.Y - item.end.Y);
                int startX = item.start.X < item.end.X ? item.start.X : item.end.X;
                int startY = item.start.Y < item.end.Y ? item.start.Y : item.end.Y;

                if (item is xRectangle)
                {
                    SaveData += AddTabs(Tabs);
                    SaveData += $"rectangle {startX} {startY} {width} {height}{Environment.NewLine}";
                }
                else if (item is Ellipse)
                {
                    SaveData += AddTabs(Tabs);
                    SaveData += $"ellipse {startX} {startY} {width} {height}{Environment.NewLine}";
                }
            }

            return SaveData;
        }

        private string AddTabs(int tabs)
        {
            string Tabs = "";
            for (int i = 0; i < tabs; i++)
            {
                Tabs += "\t";
            }
            return Tabs;
        }

        private string GroupedFiguresToSaveData()
        {
            var Groups = SaveData.Instance.Groups;
            string saveData = "";
            int tabs = 1;
            foreach (var item in Groups)
            {
                if (item.IsInGroup == null)
                {
                    saveData += $"group {item.Figures.Count+item.Groups.Count}{Environment.NewLine}";
                    saveData += GrouplessFiguresToSaveData(item.Figures, tabs);
                    if (item.Groups.Count > 0)
                        saveData += RecursiveIO(item.Groups, tabs);
                }
            }
            
            
            return saveData;
        }

        private string RecursiveIO(List<Group> groups, int tabs)
        {
            string saveData = "";
            foreach (var item in groups)
            {
                saveData += AddTabs(tabs);
                saveData += $"group {item.Figures.Count + item.Groups.Count}{Environment.NewLine}";
                saveData += GrouplessFiguresToSaveData(item.Figures, tabs+1);
                if (item.Groups.Count > 0)
                    saveData += RecursiveIO(item.Groups, tabs+1);
            }
            return saveData;
        }

        private bool GroupContainsCompositeOf(Group group, int groupID)
        {
            var answer = false;

            foreach (var item in group.Groups)
            {
                if (item.ID == groupID)
                    return true;
            }

            return answer;
        }

    }
}