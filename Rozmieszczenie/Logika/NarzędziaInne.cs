using System;
using System.ComponentModel;
using Rozmieszczenie.Widoki;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Rozmieszczenie.Logika
{    
    static class Sprawdz
    {

        public static bool CzyNull(params object[] o)
        {
            for (int i = 0; i < o.Length; i++)
            {
                TextBox T = (TextBox)o[i];
                if (T.Text == "")
                {
                    MessageBox.Show("Żadna z wartości nie może być pusta!", "Błąd",
                            MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return false;
                }                
            }
            return true;
        }



    }
}
