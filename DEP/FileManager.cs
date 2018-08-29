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
            var figures = SaveData.Instance.CurrentDrawState.Figures;
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
                        DecryptIO(SaveFileStream);
                        Console.WriteLine(SaveFileStream);
                    }
                }
            }

        }

        List<Group> Groups = new List<Group>();
        List<Figure> Figures = new List<Figure>();

        private void DecryptIO(string SaveFile)
        {
            GroupID = 0;
            var tempList = SaveFile.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
            ).ToList();

            var returnList = new List<Figure>();
            Group newGroup;

            for (int i = 0; i < tempList.Count(); i++)
            {
                if(!tempList[i].Contains("\t") && !tempList[i].Contains("group") && !String.IsNullOrEmpty(tempList[i]))
                {
                    Figures.Add(decryptFigure(tempList[i]));
                }
                else if(!tempList[i].Contains("\t") && tempList[i].Contains("group"))
                {
                    newGroup = new Group(GroupID);
                    Group currentGroup = getGroup(tempList, i, false, 1);
                    newGroup.Figures = currentGroup.Figures;
                    newGroup.Groups = currentGroup.Groups;
                    Groups.Add(newGroup);
                }
            }

            SaveData.Instance.CurrentDrawState = new DrawState(Figures, Groups);
            SaveData.Instance.HistoryList.Add(new DrawState(Figures, Groups));
        }

        private List<Figure> GetFiguresFromGroups(List<Group> groups)
        {
            var figures = new List<Figure>();
            foreach (var item in groups)
            {
                figures.AddRange(item.Figures);
                if (item.Groups.Count > 0)
                    figures.AddRange(GetFiguresFromGroups(item.Groups));
            }
            return figures;
        }

        private int GroupID { get; set; }

        private Group getGroup(List<string> tempList, int groupIndex, bool InnerGroup, int tabs)
        {
            var itemsInGroup = new List<string>();
            var index = groupIndex + 1;
            for (int i = index; i < tempList.Count(); i++)
            {
                if (tempList[i].Contains("\t"))
                    itemsInGroup.Add(tempList[i]);
                else
                    break;
            }

            List<Group> groups = new List<Group>();
            var figures = new List<Figure>();
            Group newGroup = new Group(GroupID);
            if(InnerGroup)
                newGroup.IsInGroup = GroupID - 1;

            int FiguresCount = 0;

            for (int i = 0; i < itemsInGroup.Count(); i++)
            {
                itemsInGroup[i] = itemsInGroup[i].Remove(itemsInGroup[i].IndexOf("\t"), tabs);
                if (!itemsInGroup[i].Contains("\t") && !itemsInGroup[i].Contains("group"))
                {
                    Figure figure = decryptFigure(itemsInGroup[i]);
                    figure.group = newGroup;
                    Figures.Add(figure);
                    FiguresCount++;
                }
                else if (!itemsInGroup[i].Contains("\t") && itemsInGroup[i].Contains("group"))
                {
                    GroupID++;
                    Group currentGroup = getGroup(itemsInGroup, i, true, tabs+1);
                    newGroup.AddToGroup(currentGroup);
                    i += newGroup.Figures.Count + newGroup.Groups.Count - 1;
                }
            }
            Groups.Add(newGroup);
            return newGroup;
        }

        private Figure decryptFigure(string item)
        {
            Figure figure;
            var itemList = item.Split(' ');
            var startX = int.Parse(itemList[1]);
            var startY = int.Parse(itemList[2]);
            var width = int.Parse(itemList[3]);
            var height = int.Parse(itemList[4]);


            if (item.Contains("rectangle"))
            {
                figure = new xRectangle();
            }
            else if (item.Contains("ellipse"))
            {
                figure = new Ellipse();
            }
            else
            {
                figure = new Ellipse();
            }
            figure.start = new Point(startX, startY);
            figure.end = new Point(startX + width, startY + height);
            figure.direction = DetermineDirection(figure);
            return figure;
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
            var groups = new List<Group>();
            foreach (var item in SaveData.Instance.CurrentDrawState.Groups)
            {
                groups.Add(new Group(item));
            }

            foreach (var item in SaveData.Instance.CurrentDrawState.Figures)
            {
                if (item.group != null)
                    groups[item.group.ID].AddToGroup(item);
            }


            string saveData = "";
            int tabs = 1;
            foreach (var item in groups)
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