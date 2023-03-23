using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Models
{
    public class ClientesModel
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public int nivel { get; set; }
        public string? cargo { get; set; }
        public string? fechaIng { get; set; }
        public string? consolidado { get; set; }
        public string? negadoDetalle { get; set; }
        public float tasa { get; set; }
        public float dist_mensual { get; set; }
        public float dist_anual { get; set; }
        public float max_mensual { get; set; }
        public float max_anual { get; set; }
        public float cuota_mes { get; set; }
        public float cuota_anual { get; set; }
        public float max_total { get; set; }
    }
}
