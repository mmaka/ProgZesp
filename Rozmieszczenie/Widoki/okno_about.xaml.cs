﻿using System.Windows;
using System.Windows.Input;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for okno_about.xaml
    /// </summary>
    public partial class okno_about : Window
    {
        public okno_about()
        {
            InitializeComponent();
            text.Text = "Program wspomagający optymalny rozkład figur prostokątnych na matrycy określonej wielkosci. \n\n";
            text.Text += "Wykorzystując algorytm genetyczny szukana jest najlepsza możliwa konfiguracja rozłożenia prostokątów. \n\n\n\n";
            text.Text += "Zastosowania: zarówno do wycinania jak i układania prostokątnych obiektów w ramach zadanej prostokątnej powierzchni. \n\n Autorzy:\n -Dawid Gruszczyński \n -Michał Makaś \n -Jakub Reszka \n\n UMK WFAiIS Rok 2016/2017";
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
        private void Zamknij(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimalizuj(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
