using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Models
{
    public class ClientesModel
    {        
        public string? nombre { get; set; }
        public int nivel { get; set; }
        public string? cargo { get; set; }
        public string? r1 { get; set; }
        public string? r2 { get; set; }
        public string? r3 { get; set; }
        public string? r4 { get; set; }
        public string? departamento { get; set; }
        public string? fechadeingreso { get; set; }
        public int consolidado { get; set; }
        public string? consolidado_descr { get; set; }
        public double tasa { get; set; }
        public double distribucion_mensual { get; set; }
        public double distribucion_anual { get; set; }
        public double maximo_mensual { get; set; }
        public double maximo_anual { get; set; }
        public double cuota_mensual { get; set; }
        public double cuota_anual { get; set; }
        public double maximo_total { get; set; }
    }
}
