using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
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
        List<ConsolidadoAcrClienteModel> cosolicitante;
        double monto_max, cuota_mensual, cuota_anual;
        int nroCuotasMensual, nroCuotasAnual;

        public Consulta()
        {
            InitializeComponent();
        }

        private void Consulta_cedula(object sender, RoutedEventArgs e)
        {
            string cedu, ceduCo;
            
            // Cedula emplead
            cedu = "000000000" + cedulaCliente.Text;
            cedu = nacCliente.Text + cedu.Substring(cedu.Length - 8);
            // Cedula Co solicitante
            ceduCo = "000000000" + cedulaCosolicitante.Text;
            ceduCo = nacCosolicitante.Text + ceduCo.Substring(ceduCo.Length - 8);

            try
            {
                if ((bool)adquisicion.IsChecked)
                {
                    clientes = SQLiteDataAccess.CargaCedulaAdquisicion(cedu);
                    nroCuotasMensual = 420;
                    nroCuotasAnual = 17;
                }
                else if ((bool)remodelacion.IsChecked)
                {
                    clientes = SQLiteDataAccess.CargaCedulaRemodelacion(cedu);
                    nroCuotasMensual = 180;
                    nroCuotasAnual = 14;
                } // Consulta en base de adquisicion o remodelacion segun se marque

                //Cosolicitante
                try
                {
                    if (cedulaCosolicitante.Text.Length > 0)
                    {
                        cosolicitante = SQLiteDataAccess.CargaCosolicitante(ceduCo);                        
                        if(cosolicitante[0].CONSOLIDADO == 0){
                            calificaCosolicitante.Text = "Califica";
                            calificaCosolicitante.Background = Brushes.White;
                            calificaCosolicitante.Foreground = Brushes.Black;
                        }
                        else{
                            calificaCosolicitante.Text = cosolicitante[0].CONSOLIDADO_DESC.ToString();
                            calificaCosolicitante.Background = Brushes.DarkRed;
                            calificaCosolicitante.Foreground = Brushes.White;
                        }
                    }//Si se llena cosolicitante hace la busqueda
                }
                catch
                {
                    MessageBox.Show("Ingrese cedula valida de un Co-Solicitante");
                }

                if (clientes[0].consolidado == 0)
                {
                    observacion.Text = "Califica";
                    observacion.Background = Brushes.White;
                    observacion.Foreground = Brushes.Black;
                }
                else
                {
                    observacion.Text = clientes[0].consolidado_descr.ToString();
                    observacion.Background = Brushes.DarkRed;
                    observacion.Foreground = Brushes.White;
                }// Revisa si empleado califica o tiene marca negativa

                nombreCliente.Text = clientes[0].nombre;
                cargoCliente.Text = clientes[0].cargo;
                r1.Text = clientes[0].r1;
                r2.Text = clientes[0].r2;
                r3.Text = clientes[0].r3;
                r4.Text = clientes[0].r4;
                nivelCliente.Text = clientes[0].nivel.ToString();
                departamento.Text = clientes[0].departamento;
                fechaIngCliente.Text = clientes[0].fechadeingreso;                
                tasa.Text = clientes[0].tasa.ToString();
                distMensual.Text = clientes[0].distribucion_mensual.ToString();
                distAnual.Text = clientes[0].distribucion_anual.ToString();
                maxMensual.Text = "$" + clientes[0].maximo_mensual.ToString();
                maxAnual.Text = "$" + clientes[0].maximo_anual.ToString();
                cuotaMensual.Text = "$" + clientes[0].cuota_mensual.ToString();
                cuotaAnual.Text = "$" + clientes[0].cuota_anual.ToString();

                monto_max = Double.Parse(clientes[0].maximo_total.ToString());
            }
            catch {
                MessageBox.Show("Ingrese cedula de un empleado BdV");
            };// cedula no valida
            
        }
        private void Calcular_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double montoSolicitado = double.Parse(montoSol.Text);                
                double montoAprobado = Math.Min(montoSolicitado, monto_max);
                maxTotal.Text = "$" + montoAprobado.ToString("N2");

                double montoMensual = montoAprobado * clientes[0].distribucion_mensual;
                double montoAnual= montoAprobado * clientes[0].distribucion_anual;

                cuota_mensual = PMT(clientes[0].tasa, nroCuotasMensual, montoMensual);
                cuota_anual = PMT(((clientes[0].tasa*12)*2 ), nroCuotasAnual, montoAnual);

                maxMensual.Text = "$" + montoMensual.ToString();
                maxAnual.Text = "$" + montoAnual.ToString();
                cuotaMensual.Text = "$" + cuota_mensual.ToString("N2");
                cuotaAnual.Text = "$" + cuota_anual.ToString("N2");


            }
            catch
            {
                MessageBox.Show("Error en cálculo, revise monto ingresado");
            }

        }// Calcula monto aprobado 

        private void Consultar_Click(object sender, RoutedEventArgs e)
        {
            Consulta_cedula(sender, e);
        }//Al hacer click ejecuta la consulta

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Consultar_Click((object)sender, e);
            }
        }//Al darle enter ejecuta la consulta

        private void SoloNumero_PreviewTextInput(object sender, TextCompositionEventArgs e){
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != "." && e.Text != ","){
                    e.Handled = true;
            }
        }// Solo permite ingresa numeros en textbox

        public static double PMT(double yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
        {
            var rate = yearlyInterestRate / 12;
            var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
            return (rate + (rate / denominator)) * loanAmount;
        }
    }
}
