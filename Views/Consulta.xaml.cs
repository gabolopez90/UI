using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
                nombreCliente.Text = clientes[0].nombre;
                cargoCliente.Text = clientes[0].cargo;
                nivelCliente.Text = clientes[0].nivel.ToString();
                fechaIngCliente.Text = clientes[0].fechadeingreso;
                observacion.Text = clientes[0].consolidado_descr;
                tasa.Text = clientes[0].tasa.ToString();
                distMensual.Text = clientes[0].distribucion_mensual.ToString();
                distAnual.Text = clientes[0].distribucion_anual.ToString();
                maxMensual.Text = clientes[0].maximo_mensual.ToString();
                maxAnual.Text = clientes[0].maximo_anual.ToString();
                cuotaMensual.Text = clientes[0].cuota_mensual.ToString();
                cuotaAnual.Text = clientes[0].cuota_anual.ToString();
                maxTotal.Text = clientes[0].maximo_total.ToString();

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
