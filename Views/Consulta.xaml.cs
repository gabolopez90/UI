using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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
using UI.Controller;
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
        List<DepartamentosModel> encargados;
        List<AsignacionesModel> asignaciones;
        List<EvaluadosModel> evaluados;
        double monto_max, cuota_mensual, cuota_anual, tasaAnual, cuposDiferencia, cuposUsados;
        int nroCuotasMensual, nroCuotasAnual;
        string cedu, ceduCo;

        public Consulta()
        {
            InitializeComponent();
        }

        private void Consulta_cedula(object sender, RoutedEventArgs e)
        {   
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
                    tasaAnual = (clientes[0].tasa * 12) * 2;
                }
                else if ((bool)remodelacion.IsChecked)
                {
                    clientes = SQLiteDataAccess.CargaCedulaRemodelacion(cedu);
                    nroCuotasMensual = 180;
                    nroCuotasAnual = 14;
                    tasaAnual = clientes[0].tasa * 12;
                } // Consulta en base de adquisicion o remodelacion segun se marque

                //Cosolicitante
                try
                {
                    if (cedulaCosolicitante.Text.Length > 0)
                    {
                        cosolicitante = SQLiteDataAccess.CargaCosolicitante(ceduCo);                        
                        if(cosolicitante[0].CONSOLIDADO == 0){
                            detalleCosolicitante.Text = "Califica";
                            detalleCosolicitante.Background = Brushes.White;
                            detalleCosolicitante.Foreground = Brushes.Black;
                        }
                        else{
                            detalleCosolicitante.Text = cosolicitante[0].CONSOLIDADO_DESC.ToString();
                            detalleCosolicitante.Background = Brushes.DarkRed;
                            detalleCosolicitante.Foreground = Brushes.White;
                        }
                    }//Si se llena cosolicitante hace la busqueda
                }
                catch
                {
                    MessageBox.Show("Ingrese cedula valida de un Co-Solicitante");
                }

                if (clientes[0].consolidado == 0)
                {
                    detalle.Text = "Califica";
                    detalle.Background = Brushes.White;
                    detalle.Foreground = Brushes.Black;
                }
                else
                {
                    detalle.Text = clientes[0].consolidado_descr.ToString();
                    detalle.Background = Brushes.DarkRed;
                    detalle.Foreground = Brushes.White;
                }// Revisa si empleado califica o tiene marca negativa

                try
                {
                    encargados = SQLiteDataAccess.BuscaDepartamento(clientes[0].departamento);
                    reporteDir.Text = encargados[0].encargado.ToString();
                }// Busca el reporte directo con el departamento
                catch
                {
                    MessageBox.Show("Reporte Directo no Encontrado");
                }
                
                //Llena los campos con la informacion del cliente
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
               
                monto_max = Double.Parse(clientes[0].maximo_total.ToString());

                try
                {
                    evaluados = SQLiteDataAccess.Evaluados(clientes[0].departamento);
                    asignaciones = SQLiteDataAccess.BuscaAsignaciones(clientes[0].departamento);
                    // Compara cupos con asignacion

                    if (evaluados.Count > 0)
                    {
                        if (asignaciones[0].tipo_asignacion == "Semestral")
                        {                            
                            if (System.DateTime.Now.Month <= 6)
                            {
                                cuposUsados = evaluados.Count(item => int.Parse(item.fecha.Substring(3, 2)) <= 6);
                            }
                            else
                            {
                                cuposUsados = evaluados.Count(item => int.Parse(item.fecha.Substring(3, 2)) > 6);
                            }
                        }
                        else if (asignaciones[0].tipo_asignacion == "Trimestral")
                        {                            
                            switch (System.DateTime.Now.Month)
                            {
                                case <= 3:
                                    cuposUsados = evaluados.Count(item => int.Parse(item.fecha.Substring(3, 2)) <= 3);
                                    break;
                                case > 3 and <= 6 :
                                    cuposUsados = evaluados.Count(item => int.Parse(item.fecha.Substring(3, 2)) is > 3 and <= 6);
                                    break;
                                case > 6 and <= 9 :
                                    cuposUsados = evaluados.Count(item => int.Parse(item.fecha.Substring(3, 2)) is > 6 and <= 9);
                                    break;
                                case >9 and <= 12 :
                                    cuposUsados = evaluados.Count(item => int.Parse(item.fecha.Substring(3, 2)) is > 9 and <= 12);
                                    break;
                            }
                        }
                        else
                        {
                            cuposUsados = evaluados.Count(item => int.Parse(item.fecha.Substring(3, 2)) == System.DateTime.Now.Month);
                        }
                    }
                    MessageBox.Show(cuposUsados.ToString());
                    cuposDiferencia = asignaciones[0].asignacion - cuposUsados;
                    cuposAsignados.Text = asignaciones[0].asignacion.ToString();
                    cuposRestantes.Text = cuposDiferencia.ToString();
                    tipoCupo.Text = asignaciones[0].tipo_asignacion;

                    if (cuposDiferencia >= 0){
                        cuposRestantes.Background = Brushes.White;
                        cuposRestantes.Foreground = Brushes.Black;
                    }else{
                        cuposRestantes.Background = Brushes.DarkRed;
                        cuposRestantes.Foreground = Brushes.White;
                    }// Si hay un area consumio ya sus cupos levanta alerta
                }
                catch
                {
                    MessageBox.Show("Problema con Departamento");
                }
                
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
                //Si tiene co-solicitante aumenta el limite un 15%
                double montoAprobado = cedulaCosolicitante.Text.Length > 0 ? Math.Min(montoSolicitado, (monto_max * 0.15) + monto_max) : Math.Min(montoSolicitado, monto_max);                
                maxTotal.Text = "$" + montoAprobado.ToString("N2");

                double montoMensual = montoAprobado * clientes[0].distribucion_mensual;
                double montoAnual= montoAprobado * clientes[0].distribucion_anual;

                double tasaAnual = (bool)adquisicion.IsChecked ? (clientes[0].tasa * 12) * 2 : (clientes[0].tasa * 12);

                cuota_mensual = PMT(clientes[0].tasa, nroCuotasMensual, montoMensual);
                cuota_anual = PMT(tasaAnual, nroCuotasAnual, montoAnual);

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

        private void Archivar_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                string FECHA = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                string CEDULA = cedu;
                string NOMBRE = nombreCliente.Text;
                string CARGO = cargoCliente.Text;
                string NIVEL = nivelCliente.Text;
                string DEPARTAMENTO = departamento.Text;
                string REPORTE_DIRECTO = reporteDir.Text;
                string FECHA_INGRESO = fechaIngCliente.Text;
                string R1 = r1.Text;
                string R2 = r2.Text;
                string R3 = r3.Text;
                string R4 = r4.Text;
                string MODALIDAD_CREDITO = (bool)adquisicion.IsChecked ? "ADQUISICION" : "REMODELACION";
                string CI_COSOLICITANTE = cedulaCosolicitante.Text.Length > 0 ? ceduCo : "";
                string INGRESOS_COSOLICITANTE = cedulaCosolicitante.Text.Length > 0 ? ingresosCo.Text : "0.00";
                string CARGAS_COSOLICITANTE = cedulaCosolicitante.Text.Length > 0 ? deduccionesCo.Text : "0.00";
                string CALIFICA = detalle.Text;
                string CALIFICA_COSOLICITANTE = detalleCosolicitante.Text;
                string MONTO_SOLICITADO = montoSol.Text;
                string TASA = tasa.Text;
                string DIST_MENSUAL = distMensual.Text;
                string DIST_ANUAL = distAnual.Text;
                string CUOTA_MENSUAL = cuotaMensual.Text;
                string CUOTA_ANUAL = cuotaAnual.Text;
                string MONTO_APROBADO = maxTotal.Text;
                string ESPECIALISTA = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Substring(9).ToUpper();
                string OBSERVACIONES = observaciones.Text;


                SQLiteArchivar.Archivado(FECHA, CEDULA, NOMBRE, CARGO, NIVEL, DEPARTAMENTO, REPORTE_DIRECTO, 
                    FECHA_INGRESO, R1, R2, R3, R4, MODALIDAD_CREDITO, CI_COSOLICITANTE, INGRESOS_COSOLICITANTE, 
                    CARGAS_COSOLICITANTE, CALIFICA, CALIFICA_COSOLICITANTE, MONTO_SOLICITADO, TASA, DIST_MENSUAL, DIST_ANUAL,
                    CUOTA_MENSUAL, CUOTA_ANUAL, MONTO_APROBADO, ESPECIALISTA, OBSERVACIONES);

                MessageBox.Show("REGISTRO ARCHIVADO EXITOSAMENTE");

            }
            catch (Exception p)
            {
                MessageBox.Show("ERROR EN ARCHIVADO");
                MessageBox.Show(p.ToString());
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Consultar_Click((object)sender, e);                
            }
        }//Al darle enter ejecuta la consulta

        private void OnKeyDownCalcular(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Calcular_Click((object)sender, e);
            }
        }//Al darle enter ejecuta el calculo




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
