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
        public string? fechadeingreso { get; set; }
        public string? consolidado { get; set; }
        public string? consolidado_descr { get; set; }
        public float tasa { get; set; }
        public float distribucion_mensual { get; set; }
        public float distribucion_anual { get; set; }
        public float maximo_mensual { get; set; }
        public float maximo_anual { get; set; }
        public float cuota_mensual { get; set; }
        public float cuota_anual { get; set; }
        public float maximo_total { get; set; }
    }
}
