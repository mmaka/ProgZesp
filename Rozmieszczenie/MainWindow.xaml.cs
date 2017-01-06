﻿using Microsoft.Win32;
using Rozmieszczenie.Logika;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace Rozmieszczenie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Jądro J;
        public MainWindow()
        {
            J = new Jądro(this);
            InitializeComponent();
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            J.nowy_prostokat();
        }

    

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            J.nowy_matryca();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            J.zamknij();
            Close();
        }

        private void button_rozmiesc_Click(object sender, RoutedEventArgs e)
        {
            J.rozmiesc();
            
        }
       
        private void _Usuń_prostokąt_Click(object sender, RoutedEventArgs e)
        {
            if ((dataGrid.SelectedItem is Prostokat) && dataGrid.SelectedItem != null)
            {
                List<int> listaIdDoUsunięcia = new List<int>();
                foreach (Prostokat item in dataGrid.SelectedItems)
                {

                    listaIdDoUsunięcia.Add(item.ID);
                }

                foreach (var item in listaIdDoUsunięcia)
                {
                    J.usuń_prostokąt(item);
                }
            }

        }
       private void _Usuń_wszystkie_Click(object sender, RoutedEventArgs e)
        {
            J.usuń_wszystkie();

        }
        
     
        private void _Odwróć_prostokąt_Click(object sender, RoutedEventArgs e)
        {
            if ((dataGrid.SelectedItem is Prostokat) && dataGrid.SelectedItem != null)
            {
                foreach (Prostokat item in dataGrid.SelectedItems)
                {
                    int tmp = item.W;
                    item.W = item.H;
                   item.H = tmp;
                }
               
                dataGrid.DataContext = Jądro.lista_obiektow;
                dataGrid.Items.Refresh();

            }

        }
        public void ZapiszFiguryClick(object sender, RoutedEventArgs e)
        {

            if (Jądro.lista_obiektow.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML file (*.xml)|*.xml";
                if (saveFileDialog.ShowDialog() == true)
                {
                    //do sth


                    {
                        //definiowanie obiektów
                        XDocument xml = new XDocument();
                        XDeclaration deklaracja = new XDeclaration("1.0", "utf-8", "yes");
                        XComment komentarz = new XComment("Lista Figur");
                        XElement ListaFigur = new XElement("ListaFigur");
                        XElement Projekt = new XElement("Projekt");





                        //budowanie drzewa (od gałęzi)

                        foreach (var item in Jądro.lista_obiektow)
                        {
                            XElement Figura = new XElement("Figura" + item.ID);
                            XElement nazwa = new XElement("Nazwa", item.Nazwa);
                            XElement X = new XElement("W", item.W);
                            XElement Y = new XElement("H", item.H);
                            XElement ID = new XElement("ID", item.ID);

                            Figura.Add(nazwa);
                            Figura.Add(X);
                            Figura.Add(Y);
                            Figura.Add(ID);

                            ListaFigur.Add(Figura);

                        }

                        Projekt.Add(ListaFigur);
                        xml.Declaration = deklaracja;
                        xml.Add(komentarz);
                        xml.Add(Projekt);

                        //zapis do pliku
                        xml.Save(saveFileDialog.FileName);

                    }

                }
            }
            else
                MessageBox.Show("Brak Figur!", "Błąd!",
                       MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ZapiszProjektClick(object sender, RoutedEventArgs e)
        {


            //  if (Jądro.lista_obiektow.Count > 0  )
            //{
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                {
                    //definiowanie obiektów
                    XDocument xml = new XDocument();
                    XDeclaration deklaracja = new XDeclaration("1.0", "utf-8", "yes");
                    XComment komentarz = new XComment("Cały Projekt");
                    XElement ListaFigur = new XElement("ListaFigur");
                    XElement Projekt = new XElement("Projekt");
                    XElement Matryca = new XElement("Matryca");
                    XElement Rozmieszczenia = new XElement("Rozmieszczenia");



                    try////dodanie stworzonej matrycy
                    {
                        XElement X1 = new XElement("W", J.Matka.rozmiar_x);
                        XElement Y1 = new XElement("H", J.Matka.rozmiar_y);
                        Matryca.Add(X1);
                        Matryca.Add(Y1);
                    }
                    catch { }
                    try
                    {
                        XElement Liczba = new XElement("Liczba", J.NAJLEPSZE.lista_matryc.Count);
                        Matryca.Add(Liczba);
                    }
                    catch { }

                    Projekt.Add(Matryca);




                    try
                    {
                        XElement LiczbaFigur = new XElement("LiczbaFigur", J.NAJLEPSZE.lokalizacja_figur.Length);
                        Rozmieszczenia.Add(LiczbaFigur);
                      

                        Projekt.Add(Rozmieszczenia);

                        //budowanie drzewa (od gałęzi)

                        foreach (var item in J.NAJLEPSZE.lokalizacja_figur)
                        {

                            XElement NumerMatrycy = new XElement("NumerMatrycy", item.nr_matrycy);
                            XElement X = new XElement("W", item.figura.W);
                            XElement Y = new XElement("H", item.figura.H);
                            XElement Figura = new XElement("Figura" + item.figura.ID);
                            XElement nazwa = new XElement("Nazwa", item.figura.Nazwa);
                            XElement ID = new XElement("ID", item.figura.ID);

                            XElement połX = new XElement("PołożenieX", item.p.x);
                            XElement połY = new XElement("PołożenieY", item.p.y);


                            Figura.Add(ID);
                            Figura.Add(X);
                            Figura.Add(Y);
                            Figura.Add(NumerMatrycy);
                            Figura.Add(połX);
                            Figura.Add(połY);
                            Figura.Add(nazwa);

                            ListaFigur.Add(Figura);

                        }
                        XElement Info = new XElement("Info", J.InfoOkno.textBox);
                        Projekt.Add(Info);
                    }
                    catch
                    {
                        try
                        {

                            foreach (var item in Jądro.lista_obiektow)
                            {
                                XElement Figura = new XElement("Figura" + item.ID);
                                XElement nazwa = new XElement("Nazwa", item.Nazwa);
                                XElement X = new XElement("W", item.W);
                                XElement Y = new XElement("H", item.H);
                                XElement ID = new XElement("ID", item.ID);

                                Figura.Add(nazwa);
                                Figura.Add(X);
                                Figura.Add(Y);
                                Figura.Add(ID);

                                ListaFigur.Add(Figura);

                            }

                        }
                        catch { }

                    }

                    Projekt.Add(ListaFigur);

                    xml.Declaration = deklaracja;
                    xml.Add(komentarz);
                    xml.Add(Projekt);

                    //zapis do pliku
                    xml.Save(saveFileDialog.FileName);

                }
            }
            // }
            //  else
            //    MessageBox.Show("Nie można zapisać!", "Błąd!",
            //      MessageBoxButton.OK, MessageBoxImage.Warning);

        }


        private void WczytajListęFigur(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ("XML file(*.xml) | *.xml");
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    XDocument xml = XDocument.Load(openFileDialog.FileName);




                    IEnumerable<XElement> listaWczytanychElementów = xml.Root.Element("ListaFigur").Elements();
                    int maxID=0;
                    if (Jądro.lista_obiektow.Count > 0)
                        maxID = Jądro.lista_obiektow[Jądro.lista_obiektow.Count - 1].ID;
                       
                

                    foreach (var item in listaWczytanychElementów)
                    {

                        Jądro.lista_obiektow.Add(new Prostokat(int.Parse(item.Element("W").Value), int.Parse(item.Element("H").Value), int.Parse(item.Element("ID").Value)+maxID, item.Element("Nazwa").Value));
                        Prostokat.licznik = int.Parse(item.Element("ID").Value) + maxID;
                    }
                    dataGrid.DataContext = Jądro.lista_obiektow;
                    dataGrid.Items.Refresh();



                }
            }
            catch
            {
                MessageBox.Show("Błąd podczas wczytywania listy Figur!", "Błąd",
                       MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void WczytajProjektClick(object sender, RoutedEventArgs e)
        {


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ("XML file(*.xml) | *.xml");
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    XDocument xml = XDocument.Load(openFileDialog.FileName);

                    try
                    {
                        IEnumerable<XElement> listaWczytanychElementów = xml.Root.Element("ListaFigur").Elements();
                        Jądro.lista_obiektow.Clear();

                        foreach (var item in listaWczytanychElementów)
                        {

                            Jądro.lista_obiektow.Add(new Prostokat(int.Parse(item.Element("W").Value), int.Parse(item.Element("H").Value), int.Parse(item.Element("ID").Value), item.Element("Nazwa").Value));
                        }
                        dataGrid.DataContext = Jądro.lista_obiektow;
                        dataGrid.Items.Refresh();
                    }
                    catch { }
                    try
                    {
                        J.wm = new Widoki.widok_matryca(J, int.Parse(xml.Root.Element("Matryca").Element("W").Value), int.Parse(xml.Root.Element("Matryca").Element("H").Value));
                        J.wm.Show();
                        J.R = new Rysowanie(J.wm);
                        J.Matka = new Matryca(int.Parse(xml.Root.Element("Matryca").Element("W").Value), int.Parse(xml.Root.Element("Matryca").Element("H").Value));
                    }
                    catch { }
                    try
                    {
                        Rozmieszczenia roz = new Rozmieszczenia(int.Parse(xml.Root.Element("Rozmieszczenia").Element("LiczbaFigur").Value), new Matryca(int.Parse(xml.Root.Element("Matryca").Element("W").Value), int.Parse(xml.Root.Element("Matryca").Element("H").Value)), int.Parse(xml.Root.Element("Odstep").Element("odst").Value));

                        var zapytanie = xml.Root.Element("ListaFigur").Elements();
                        int iterator = 0;
                        foreach (var item in zapytanie)
                        {
                            roz.lokalizacja_figur[iterator] = new MatrycaFiguraPunkt(int.Parse(item.Element("NumerMatrycy").Value), new Prostokat(int.Parse(item.Element("W").Value), int.Parse(item.Element("H").Value), int.Parse(item.Element("ID").Value), item.Element("Nazwa").Value), new Punkt(int.Parse(item.Element("PołożenieX").Value), int.Parse(item.Element("PołożenieY").Value)));
                            iterator++;
                        }

                        J.R.Rysuj(J.wm, roz, int.Parse(xml.Root.Element("Matryca").Element("Liczba").Value) - 1);

                        J.InfoOkno = new Widoki.informacyjne();
                        J.InfoOkno.textBox.Text = xml.Root.Element("Info").Value;
                        J.InfoOkno.Show();
                    }
                    catch { }

                }
            }
            catch
            {
                MessageBox.Show("Błąd podczas wczytywania Projektu!", "Błąd",
                        MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }
        private void dataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            J.test();
        }
        private void DataGridPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if ((dataGrid.SelectedItem is Prostokat) && dataGrid.SelectedItem != null)
                {
                    List<int> listaIdDoUsunięcia = new List<int>();
                    foreach (Prostokat item in dataGrid.SelectedItems)
                    {

                        listaIdDoUsunięcia.Add(item.ID);
                    }

                    foreach (var item in listaIdDoUsunięcia)
                    {
                        J.usuń_prostokąt(item);
                    }
                }
            }

        }
    }
}
