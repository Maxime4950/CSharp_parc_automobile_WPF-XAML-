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

namespace ISET2018_WPFBD.View
{
    /// <summary>
    /// Logique d'interaction pour FraisAchat.xaml
    /// </summary>
    public partial class FraisAchat : Window
    {
        private ViewModel.VM_Frais LocalFrais;
        public FraisAchat()
        {
            InitializeComponent();
            LocalFrais = new ViewModel.VM_Frais();
            DataContext = LocalFrais;
        }

        private void dgFrais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgFrais.SelectedIndex >= 0) LocalFrais.FraisSelectionnee2UnFrais();
        }

        private void bQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
