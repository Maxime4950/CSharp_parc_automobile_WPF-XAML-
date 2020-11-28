using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ISET2018_WPFBD.Model;
namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour Stock.xaml
    /// </summary>
    public partial class Stock : Window
    {
        private ViewModel.VM_Stock LocalStock;

        private string sConnex = @"Data Source=DESKTOP-5KJPBES;Initial Catalog=C:\USERS\MAESM\DOCUMENTS\COMPLEMENT_P\ISET2018_WPFBD_MVVM_CONCEPT\ISET2018_WPFBD\BD_VOITURE_MVVM.MDF;Integrated Security=True";
        public Stock()
        {
            InitializeComponent();
            LocalStock = new ViewModel.VM_Stock();
            DataContext = LocalStock;
        }

        private void bQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgStock.SelectedIndex >= 0) LocalStock.StockSelectionnee2UnStock();
        }

        private void bConsulter_Click(object sender, RoutedEventArgs e)
        {
            FicheInfoStock.IsEnabled = true;
        }

        private void bConsulterStockHtml_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
