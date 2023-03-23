using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
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

    }
}
