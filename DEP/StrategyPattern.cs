using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEP
{
    public interface IFigure
    {
        void Draw(PaintEventArgs e, Figure figure);
        string ToString(Figure figure);
    }
       
}
