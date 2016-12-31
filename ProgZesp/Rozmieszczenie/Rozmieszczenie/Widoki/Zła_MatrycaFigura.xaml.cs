using Rozmieszczenie.Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for Zła_MatrycaFigura.xaml
    /// </summary>
    public partial class Zła_MatrycaFigura : Window
    {
        int ID;
        Jądro jd;
        nowa_prostokat np;
        MainWindow mw;

        public Zła_MatrycaFigura(int ID, Jądro jd, string Komunikat, MainWindow mw)
        {
            InitializeComponent();
            //this.Closing += new System.ComponentModel.CancelEventHandler(MyWindow_Closing);// zablokowanie zamykania okna
            label.Content = Komunikat;
            this.ID = ID;
            this.jd = jd;
            this.mw = mw;
        }

        private void Usun_figure_button1_Click(object sender, RoutedEventArgs e)
        {
            Prostokat tmp = (Prostokat)Jądro.lista_obiektow.Where(p => p.ID == ID).First<Prostokat>(); //szukamy figury do usuniecia
            jd.usuń_prostokąt(tmp);
            this.Close();
        }



        private void Edytuj_Matrycebutton_Click(object sender, RoutedEventArgs e)
        {
            //  Jądro.listaStworzonychMatryc.Clear();
            Jądro.listaStworzonychMatryc.First<Matryca>().EdytujMatrycę(500, 500);
            //  jd.nowy_matryca();
            this.Close();
        }


        private void EdytujFigureButton_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (Prostokat)Jądro.lista_obiektow.Where(p => p.ID == ID).First<Prostokat>();
            np = new nowa_prostokat(jd, (Prostokat)tmp);

            np.ShowDialog();
            mw.dataGrid.DataContext = Jądro.lista_obiektow;
            mw.dataGrid.Items.Refresh();
            this.Close();


        }
        void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
