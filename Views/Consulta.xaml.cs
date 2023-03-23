using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Models;

namespace UI.Views
{
    /// <summary>
    /// Lógica de interacción para Consulta.xaml
    /// </summary>
    public partial class Consulta : UserControl
    {
        List<ClientesModel> clientes;

        public Consulta()
        {
            InitializeComponent();            
        }

        private async void Consulta_cedula(object sender, RoutedEventArgs e)
        {
            string cedu;
            cedu = "000000000" + cedulaCliente.Text;
            cedu = nacCliente.Text + cedu.Substring(cedu.Length - 8);
            try
            {
                clientes = SQLiteDataAccess.CargaCedula(cedu);
                nombreCliente.Content = clientes[0].nombre;
                cargoCliente.Content = clientes[0].cargo;
                nivelCliente.Content = clientes[0].nivel;
            }
            catch {
                MessageBox.Show("Ingrese cedula de un empleado BdV");
            };
            
        }

        private void consultar_Click(object sender, RoutedEventArgs e)
        {
            Consulta_cedula(sender, e);
        }//Al hacer click ejecuta la consulta

        private async void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Consulta_cedula(sender, e);
            }
        }//Al darle enter ejecuta la consulta
    }
}
