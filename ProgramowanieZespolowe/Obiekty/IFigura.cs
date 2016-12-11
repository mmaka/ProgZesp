using System.Windows.Forms;

namespace dodawanie_figur1
{
    interface IFigura
    {
       void rysuj(object sender, PaintEventArgs e);
       string Name { get;}
    }
}
