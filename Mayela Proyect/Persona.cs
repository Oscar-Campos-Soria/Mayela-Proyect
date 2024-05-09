using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayela_Proyect
{
    class Persona
    {
        public String Id { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public DateTime FechaNcimiento { get; set; }
        public DateTime FechaRegistro { get; set; } // Nueva propiedad para la fecha de registro

    }
}
