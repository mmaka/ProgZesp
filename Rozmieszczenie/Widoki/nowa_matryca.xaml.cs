﻿using Rozmieszczenie.Logika;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for nowa_matryca.xaml
    /// </summary>
    public partial class nowa_matryca : Window
    {
        Jądro J;
        int nm = 0;
        public nowa_matryca(Jądro j, int nm = 0)
        {
            J = j;            InitializeComponent();  this.nm = nm;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Sprawdz.CzyNull(textBox_matryca_szerokość, textBox_matryca_wysokość, textBox_matryca_odstep))
            {
                if (nm != 0)
                {
                    J.zmień_matryce(int.Parse(textBox_matryca_szerokość.Text), int.Parse(textBox_matryca_wysokość.Text));
                    Close();
                }
                else
                {
                    J.odstep = Convert.ToInt32(textBox_matryca_odstep.Text);
                    J.dodaj_matryce();
                }
            }
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                button_Click(null, null);
            }
            else if (e.Key == Key.Escape) Close();
        }


        private void Zaznacz(object sender, RoutedEventArgs e)
        {

            var textBox = sender as System.Windows.Controls.TextBox;
            textBox.SelectAll();

        }

        private void Zaznacz(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            textBox.SelectAll();
        }

        private void Zaznacz(object sender, System.EventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            textBox.SelectAll();
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
