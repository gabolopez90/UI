using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Models
{
    

    public class SQLiteDataAccess
    {
        public static List<ClientesModel> CargaCedulaAdquisicion(string CEDULA)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<ClientesModel>("Select * from HIPOTECARIO_ADQUISICION where CEDULA = '" + CEDULA + "'", new DynamicParameters());
            return output.ToList();
        }

        public static List<ClientesModel> CargaCedulaRemodelacion(string CEDULA)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<ClientesModel>("Select * from HIPOTECARIO_REMODELACION where CEDULA = '" + CEDULA + "'", new DynamicParameters());
            return output.ToList();
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}

    
