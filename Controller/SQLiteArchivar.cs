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
using UI.Views;

namespace UI.Controller
{
    public class SQLiteArchivar
    {

        // Archivar
        public static void Archivado(string FECHA, string CEDULA, string NOMBRE, string CARGO, string NIVEL, string DEPARTAMENTO,
            string REPORTE_DIRECTO, string FECHA_INGRESO, string R1, string R2, string R3, string R4, string MODALIDAD_CREDITO,
            string CI_COSOLICITANTE, string INGRESOS_COSOLICITANTE, string CARGAS_COSOLICITANTE, string CALIFICA,
            string CALIFICA_COSOLICITANTE, string MONTO_SOLICITADO, string TASA, string DIST_MENSUAL, string DIST_ANUAL,
            string CUOTA_MENSUAL, string CUOTA_ANUAL, string MONTO_APROBADO, string ESPECIALISTA, string OBSERVACIONES, string ESTATUS, 
            string ULTIMA_MODIFICACION, string ESTADO, string MUNICIPIO, string PARROQUIA, string COD_PARROQUIA)
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Execute("insert into ARCHIVO_HIPOTECARIO (FECHA, CEDULA, NOMBRE, CARGO, NIVEL, DEPARTAMENTO, REPORTE_DIRECTO, " +
                "FECHA_INGRESO, R1, R2, R3, R4, MODALIDAD_CREDITO, CI_COSOLICITANTE, INGRESOS_COSOLICITANTE, CARGAS_COSOLICITANTE, " +
                "CALIFICA, CALIFICA_COSOLICITANTE, MONTO_SOLICITADO, TASA, DIST_MENSUAL, DIST_ANUAL, CUOTA_MENSUAL, CUOTA_ANUAL, " +
                "MONTO_APROBADO, ESPECIALISTA, OBSERVACIONES, ESTATUS, ULTIMA_MODIFICACION, ESTADO, MUNICIPIO, PARROQUIA, COD_PARROQUIA ) " +
                "values ('" + FECHA + "', '" + CEDULA + "', '" + NOMBRE + "', '" + CARGO + "', '" + NIVEL + "', '" + DEPARTAMENTO + "', '" +
                REPORTE_DIRECTO + "', '" + FECHA_INGRESO + "', '" + R1 + "', '" + R2 + "', '" + R3 + "', '" + R4 + "', '" +
                MODALIDAD_CREDITO + "', '" + CI_COSOLICITANTE + "', '" + INGRESOS_COSOLICITANTE + "', '" + CARGAS_COSOLICITANTE + "', '" +
                CALIFICA + "', '" + CALIFICA_COSOLICITANTE + "', '" + MONTO_SOLICITADO + "', '" + TASA + "', '" + DIST_MENSUAL + "', '" +
                DIST_ANUAL + "', '" + CUOTA_MENSUAL + "', '" + CUOTA_ANUAL + "', '" + MONTO_APROBADO + "', '" + ESPECIALISTA + "', '" +
                OBSERVACIONES + "', '" + ESTATUS + "', '" + ULTIMA_MODIFICACION + "', '" + ESTADO + "', '" + MUNICIPIO + "', '" + PARROQUIA + "', '" + COD_PARROQUIA + "')");
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
