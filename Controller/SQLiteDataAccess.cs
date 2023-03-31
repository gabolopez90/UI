using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.Models
{
    

    public class SQLiteDataAccess
    {
        // Carga base de credito adquisicion
        public static List<ClientesModel> CargaCedulaAdquisicion(string CEDULA)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<ClientesModel>("Select * from HIPOTECARIO_ADQUISICION where CEDULA = '" + CEDULA + "'", new DynamicParameters());
            return output.ToList();
        }

        // Carga base de credito remodelacion
        public static List<ClientesModel> CargaCedulaRemodelacion(string CEDULA)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<ClientesModel>("Select * from HIPOTECARIO_REMODELACION where CEDULA = '" + CEDULA + "'", new DynamicParameters());            
            return output.ToList();
        }

        // Carga informacion para co solicitante
        public static List<ConsolidadoAcrClienteModel> CargaCosolicitante(string CEDULA)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<ConsolidadoAcrClienteModel>("Select * from CONSOLIDADO_ACR_CLIENTE where CEDULA = '" + CEDULA + "'", new DynamicParameters());
            return output.ToList();
        }

        // Carga reporte directo por departamento
        public static List<DepartamentosModel> BuscaDepartamento(string departamento)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<DepartamentosModel>("Select * from DEPARTAMENTOS where DEPARTAMENTO = '" + departamento + "'", new DynamicParameters());            
            return output.ToList();
        }

        public static List<AsignacionesModel> BuscaAsignaciones(string departamento)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<AsignacionesModel>("Select * from ASIGNACIONES where DEPARTAMENTO = '" + departamento + "'", new DynamicParameters());
            return output.ToList();
        }

        // Archivar
        public static void Archivado(string FECHA, string CEDULA, string NOMBRE, string CARGO, string NIVEL, string DEPARTAMENTO, 
            string REPORTE_DIRECTO, string FECHA_INGRESO, string R1, string R2, string R3, string R4, string MODALIDAD_CREDITO, 
            string CI_COSOLICITANTE, string INGRESOS_COSOLICITANTE, string CARGAS_COSOLICITANTE, string CALIFICA, 
            string CALIFICA_COSOLICITANTE, string MONTO_SOLICITADO, string TASA, string DIST_MENSUAL, string DIST_ANUAL, 
            string CUOTA_MENSUAL,string CUOTA_ANUAL, string MONTO_APROBADO, string ESPECIALISTA, string OBSERVACIONES)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Execute("insert into ARCHIVO_HIPOTECARIO (FECHA, CEDULA, NOMBRE, CARGO, NIVEL, DEPARTAMENTO, REPORTE_DIRECTO, " +
                "FECHA_INGRESO, R1, R2, R3, R4, MODALIDAD_CREDITO, CI_COSOLICITANTE, INGRESOS_COSOLICITANTE, CARGAS_COSOLICITANTE, " +
                "CALIFICA, CALIFICA_COSOLICITANTE, MONTO_SOLICITADO, TASA, DIST_MENSUAL, DIST_ANUAL, CUOTA_MENSUAL, CUOTA_ANUAL, " +
                "MONTO_APROBADO, ESPECIALISTA, OBSERVACIONES) " +
                "values ('" + FECHA + "', '" + CEDULA + "', '" + NOMBRE + "', '" + CARGO + "', '" + NIVEL + "', '" + DEPARTAMENTO + "', '" + 
                REPORTE_DIRECTO + "', '" + FECHA_INGRESO + "', '" + R1 + "', '" + R2 + "', '" + R3 + "', '" + R4 + "', '" + 
                MODALIDAD_CREDITO + "', '" + CI_COSOLICITANTE + "', '" + INGRESOS_COSOLICITANTE + "', '" + CARGAS_COSOLICITANTE + "', '" + 
                CALIFICA + "', '" + CALIFICA_COSOLICITANTE + "', '" + MONTO_SOLICITADO + "', '" + TASA + "', '" + DIST_MENSUAL + "', '" + 
                DIST_ANUAL + "', '" + CUOTA_MENSUAL + "', '" + CUOTA_ANUAL + "', '" + MONTO_APROBADO + "', '" + ESPECIALISTA + "', '" + 
                OBSERVACIONES + "')");
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}

    
