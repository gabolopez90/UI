using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.ViewModels
{
    public class ConsultaModel : ViewModelBase
    {

        public string[] Nacionalidad { get; set; } = {
                "V",
                "E",
                "J",
                "P",
                "M"
        };

        public string[] Estado { get; set; } =
        {
            "AMAZONAS",
            "ANZOATEGUI",
            "APURE",
            "ARAGUA",
            "BARINAS",
            "BOLIVAR",
            "CARABOBO",
            "COJEDES",
            "DELTA AMACURO",
            "FALCON",
            "DISTRITO CAPITAL",
            "GUARICO",
            "LA GUAIRA",
            "LARA",            
            "MERIDA",
            "MIRANDA",
            "MONAGAS",
            "NUEVA ESPARTA",
            "PORTUGUESA",
            "SUCRE",
            "TACHIRA",
            "YARACUY",
            "TRUJILLO",
            "ZULIA"
        };    
        

    }
}
