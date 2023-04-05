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
        
        public static List<EvaluadosModel> Evaluados(string departamento)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<EvaluadosModel>("Select * from ARCHIVO_HIPOTECARIO where DEPARTAMENTO = '" + departamento + "'", new DynamicParameters());
            return output.ToList();
        }

        public static List<MunicipiosModel> BuscaMunicipio(string estado)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<MunicipiosModel>("Select * from ESTADOS where ESTADO = '" + estado + "'", new DynamicParameters());
            return output.ToList();
        }

        public static List<ParroquiasModel> BuscaParroquia(string municipio)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<ParroquiasModel>("Select * from ESTADOS where MUNICIPIO = '" + municipio + "'", new DynamicParameters());
            return output.ToList();
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}

    
