using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
                        var x = stream.ReadToEnd();
                        test = DecryptIO(x);
                        Console.WriteLine(x);
                    }
                }
            }
            SaveData.Instance.Figures = test;
            SaveData.Instance.HistoryList.Add(new List<Figure>(test));
        }

        private List<Figure> DecryptIO(string x)
        {
            var tempList = x.Split(
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
            string saveData = "";
            foreach (var item in figures)
            {
                int width = Math.Abs(item.start.X - item.end.X);
                int height = Math.Abs(item.start.Y - item.end.Y);
                int startX = item.start.X < item.end.X ? item.start.X : item.end.X;
                int startY = item.start.Y < item.end.Y ? item.start.Y : item.end.Y;

                if(item is xRectangle)
                {
                    saveData += $"rectangle {startX} {startY} {width} {height}{Environment.NewLine}";
                }
                else if( item is Ellipse)
                {
                    saveData += $"ellipse {startX} {startY} {width} {height}{Environment.NewLine}";                    
                }
            }
            return saveData;
        }


        private string GroupIO(List<Group> Groups)
        {
            string saveGroupData = "";
            for (int i = 0; i < Groups.Count; i++)
            {
                var ExistsAsComposite = false;
                if (Groups[i].Groups.Count > 0)
                {
                    for (int j = 0; j < Groups.Count; j++)
                    {
                        ExistsAsComposite = GroupContainsCompositeOf(Groups[j], Groups[i].ID);

                    }
                }
                if (ExistsAsComposite)
                    Groups.Remove(Groups[i]);
            }
            return saveGroupData;
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